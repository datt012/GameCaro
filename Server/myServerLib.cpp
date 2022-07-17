#include "server.h"
#include "database.h"
#include "Room.h"

vector<Room> listRooms;
vector<UserLogin> listUserLogin;

/* function Receive: receive data from player

@param s: a truct player have socket use to receive data
@param dataIn: A pointer to a string to save data
@param mess: A pointer to a struct package to a complete message

Returns
0 on error
1 if message not process out of
2 if the message has been processed
*/

int Receive(Player s, char *dataIn, package *mess) {

	char dataRecv[BUFF_SIZE], buff[BUFF_MAX], length[5];
	long ret = 0;
	long lenData = 0;

	strcpy(buff, dataIn);
	size_t idx = strlen(buff);
	//return 0;
	if (idx > 5) { //da nhan duoc du lieu
		strncpy_s(length, dataIn + 2, 4);
		lenData = atoi(length);
		if (idx - 6 >= lenData) { //nhan du du lieu
			mess->opcode[0] = buff[0];
			mess->opcode[1] = buff[1];
			strcat_s(mess->length, length);
			strncpy_s(mess->payload, buff + 6, lenData);
			if (idx - 6 == lenData) { //du du lieu
				dataIn[0] = 0;
				return 2;
			}
			else //size data != length
			{
				dataIn[0] = 0;
				long location = lenData + 6;
				for (long j = 0; j < idx - location; j++) {
					dataIn[j] = buff[j + location];      //phan du lieu bi thua cho vao dataIn
				}
				return 1;
			}
		}
	}

	while (1) { //chua nhan duoc du lieu
				//receive data
		ret = recv(s.s, dataRecv, BUFF_SIZE, 0);
		if (ret == SOCKET_ERROR)
		{
			printf("Error: %d! Cannot receive message.", WSAGetLastError());
			return 0;
		}
		else if (ret == 0) {
			printf("Client disconnects.\n");
			return 0;
		}
		else {
			dataRecv[ret] = 0;
			strcat_s(buff, dataRecv);
		}
		idx = strlen(buff);
		strncpy_s(length, buff + 2, 4);
		lenData = atoi(length);
		if (idx - 6 >= lenData) {
			mess->opcode[0] = buff[0];
			mess->opcode[1] = buff[1];
			strcat_s(mess->length, length);
			strncpy_s(mess->payload, buff + 6, lenData);
			if (idx - 6 == lenData) {
				dataIn[0] = 0;
				return 2;
			}
			else
			{
				dataIn[0] = 0;
				long location = lenData + 6;
				for (long j = 0; j < idx - location; j++) {
					dataIn[j] = buff[j + location];
				}
				return 1;
			}
		}
	}
	return 0;
}

/* function Send: send data

@param s: socket use to send data
@param opcode: A pointer to a string opcode
@param dataIn: A pointer to a string data to send

Returns 0 on error and 1 on success
*/

int Send(SOCKET s, char *opcode, char *dataIn) {
	long ret = 0, idx;
	char dataSend[BUFF_SIZE], lengthPay[5];

	size_t lenData = strlen(dataIn);
	convertIntToChar(lenData, lengthPay);

	strcpy(dataSend, opcode);
	strcat(dataSend, lengthPay);
	strcat(dataSend, dataIn);

	lenData = strlen(dataSend);
	int leftBytes = (int)lenData;
	idx = 0;
	while (leftBytes > 0) {
		ret = send(s, &dataSend[idx], leftBytes, 0);
		if (ret == SOCKET_ERROR)
		{
			printf("Error: %d! Cannot send message.\n", WSAGetLastError());
			return 0;
		}
		idx += ret;
		leftBytes -= ret;
	}
	return 1;
}

/* function handleDataReceive: handle data receive

@param player: A pointer to a struct player
@param mess: A pointer to a struct package constain data
*/

void handleDataReceive(Player *player, package mess) {
	char opcode[3];
	opcode[0] = mess.opcode[0];
	opcode[1] = mess.opcode[1];
	opcode[2] = 0;
	int t = atoi(mess.opcode);
	switch (t)
	{
	case 10:
		userSignup(player, mess);
		break;
	case 20:
		userSignin(player, mess);
		break;
	case 30:
		if (userSignout(player) == 31) Send(player->s, "31", "");
		else if (userSignout(player) == 32) Send(player->s, "32", "");
		break;
	case 40:
		Send(player->s, "41", (char*)listPlayerIsFree(player->username).c_str());
		break;
	case 50:
		handleChallenge(player, mess.payload, opcode);
		break;
	case 51:
		acceptChallenge(player, mess.payload, opcode);
		break;
	case 52:
		refuseChallenge(player, mess.payload, opcode);
		break;
	case 60:
		sendResult(player);
		break;
	case 70:
		sendCoordinates(player, mess.payload);
		break;
	case 80:
		userSurrender(player);
		break;
	default:
		break;
	}
}

void userSignup(Player *player, package mess) {
	string s(mess.payload);
	if (count(s.begin(), s.end(), ' ') == 1) {
		char *username, *password;
		username = strtok(mess.payload, " ");
		password = strtok(NULL, "\n");
		int ret = signUp(username, password);
		if (ret == 11) {
			Send(player->s, "11", "");
		}
		else if (ret == 12)
		{
			Send(player->s, "12", "");
		}
		else 
		{
			Send(player->s, "14", "");
		}
	}
	else Send(player->s, "13", "");

}


/* function login: handle login request of user

@param player: A pointer to a struct player want to login
@param mess: A pointer to a struct package constain data
*/

void userSignin(Player *player, package mess) {
	char *username, *password;
	username = strtok(mess.payload, " ");
	password = strtok(NULL, "\n");
	int ret = signIn(username, password);
	if (ret == 21) {
		player->isLogin = 1;
		strcpy_s(player->username, username);
		UserLogin user = UserLogin(player->username, player->s);
		listUserLogin.push_back(user);
		Send(player->s, "21", "");
		Send(player->s, "41", (char*)listPlayerIsFree(player->username).c_str());
	}
	else if (ret == 22) Send(player->s, "22", "");
	else if (ret == 23) Send(player->s, "23", "");
	else if (ret == 24) Send(player->s, "24", "");
	else Send(player->s, "25", "");
}

/* function logout: handle logout request of user

@param player: A pointer to a struct player want to login
*/

int userSignout(Player *player) {
	if (player->isLogin != 0) {
		//remove in listUserLogin
		player->isLogin = 0;
		removeUser(player->s);
		updateUserIsFree(player->username, 1);
		updateUserStatus(player->username, 0);
		return 31;
	}
	else return 32;
}

void sendResult(Player *player) {
	int score = getScore(player->username);
	int rank = getRank(player->username);
	string s = to_string(score) + " " + to_string(rank);
	Send(player->s, "61", (char*)s.c_str());
}

/* function getCoordinates: get coordinates from data

@param data: A pointer to a string data

Returns a struct Coordinates
*/

Coordinates getCoordinates(char *data) {
	char x[3], y[3];
	strncpy(y, data + 0, 2);
	strncpy(x, data + 2, 2);
	Coordinates coordinates(atoi(x), atoi(y));
	return coordinates;
}

/* function sendChallenge: send challenge from player to challenged person

@param player: A pointer to a struct player
@param usernameRecv: A pointer to username receive
@param opcode: A pointer to a message code
*/

void handleChallenge(Player *player, char *usernameRecv, char *opcode) {
	int index = -1;
	for (int i = 0; i < listUserLogin.size(); i++) {
		if (strcmp(listUserLogin[i].username, usernameRecv) == 0) {
			index = i;
			break;
		}
	}
	if (index > -1) {
		if (getStatusFree(usernameRecv) == 1) {
			if (abs(getRank(player->username) - getRank(usernameRecv)) <= 10) {
				updateUserChallenge(player->username, usernameRecv);
				Send(listUserLogin[index].s, opcode, player->username);
			}
			else
			{
				Send(player->s, "53", "");
			}
		}
		else
		{
			Send(player->s, "54", "");
		}
	}
	else
	{
		Send(player->s, "55", "");
	}

}

/* function receiveChallenge: receive challenge

@param player: A pointer to a struct player
@param usernameRecv: A pointer to username receive
@param opcode: A pointer to a message code
*/

void acceptChallenge(Player *player, char *usernameRecv, char *opcode) {
	SOCKET s;
	for (int i = 0; i < listUserLogin.size(); i++) {
		if (strcmp(listUserLogin[i].username, usernameRecv) == 0) {
			s = listUserLogin[i].s;
			break;
		}
	}
	if (strcmp((char*)getUserChallenge(usernameRecv).c_str(), player->username) == 0) {
		updateUserChallenge(usernameRecv, "");
		Send(s, opcode, player->username);
		Send(player->s, opcode, "");
		//change isFree
		updateUserIsFree(player->username, 0);
		updateUserIsFree(usernameRecv, 0);
		//add room
		Room room = Room(s, player->s);
		listRooms.push_back(room);
	}
}

/* function refuseChallenge: refuse challenge

@param usernameSend: A pointer to username send
@param usernameRecv: A pointer to username receive
@param opcode: A pointer to a message code
*/

void refuseChallenge(Player *player, char *usernameRecv, char *opcode) {
	if (strcmp((char*)getUserChallenge(usernameRecv).c_str(), player->username) == 0) {
		updateUserChallenge(usernameRecv, "");
		SOCKET s;
		for (int i = 0; i < listUserLogin.size(); i++) {
			if (strcmp(listUserLogin[i].username, usernameRecv) == 0) {
				s = listUserLogin[i].s;
				break;
			}
		}
		Send(s, opcode, player->username);
	}
}

/* function sendCoordinates: send coordinates

@param player: A pointer to a struct player
@param opcode: A pointer to a message code
@param payload: A pointer to string data constain coordinates
*/

void sendCoordinates(Player *player, char *payload) {
	SOCKET clientRecv;
	auto findRoom = [](Player *player)->Room {
		for (int i = 0; i < listRooms.size(); i++)
		{
			if (listRooms[i].client1 == player->s || listRooms[i].client2 == player->s)
				return listRooms[i];
		}
		return Room(0, 0);
	};
	Room room = findRoom(player);

	Coordinates coordinate = getCoordinates(payload);
	if (room.client1 == player->s) {
		clientRecv = room.client2;
		room.updateMatrix(coordinate, 1);
	}
	else
	{
		clientRecv = room.client1;
		room.updateMatrix(coordinate, 2);
	}
	Send(clientRecv, "71", payload);
	auto findUserLogin = [clientRecv]()->UserLogin {
		for (int i = 0; i < listUserLogin.size(); i++) {
			if (listUserLogin[i].s == clientRecv) {
				return listUserLogin[i];
			}
		}
		return UserLogin("", 0);
	};
	UserLogin userRecv = findUserLogin();
	switch (room.isEndGame(coordinate))
	{
	case 0:
		break;
	case 1:
		Send(clientRecv, "90", player->username);
		Send(player->s, "90", player->username);
		updateScoreOfPlayer(player->username, 1);
		updateScoreOfPlayer(userRecv.username, 0);
		updateUserIsFree(player->username, 1);
		updateUserIsFree(userRecv.username, 1);
		removeRoom(player->s);
		updateRank();
		break;
	case 2:
		Send(player->s, "90", "");
		Send(clientRecv, "90", "");
		break;
	default:
		break;
	}

}

void userSurrender(Player *player) {
	Room room(0, 0);
	SOCKET clientRecv;
	for (int i = 0; i < listRooms.size(); i++)
	{
		if (listRooms[i].client1 == player->s || listRooms[i].client2 == player->s) {
			room = listRooms[i];
		}
	}
	if (room.client1 == player->s) {
		clientRecv = room.client2;
	}
	else
	{
		clientRecv = room.client1;
	}
	auto findUserLogin = [clientRecv]()->UserLogin {
		for (int i = 0; i < listUserLogin.size(); i++) {
			if (listUserLogin[i].s == clientRecv) {
				return listUserLogin[i];
			}
		}
		return UserLogin("", 0);
	};
	UserLogin playerWin = findUserLogin();
	Send(playerWin.s, "90", playerWin.username);
	updateScoreOfPlayer(playerWin.username, 1);
	updateScoreOfPlayer(player->username, 0);
	updateUserIsFree(player->username, 1);
	updateUserIsFree(playerWin.username, 1);
	removeRoom(player->s);
	updateRank();

}

/* function removeRoom: remove room from list room

@param s: socket in the room want to delete
*/

void removeRoom(SOCKET s) {
	int index = -1;
	for (int i = 0; i < listRooms.size(); i++) {
		if (listRooms[i].client1 == s || listRooms[i].client2 == s) {
			index = i;
			break;
		}
	}
	//remove in listRooms
	if (index > -1) {
		listRooms.erase(listRooms.begin() + index);
	}
}

/* function removeUser: remove user from list user login when user logout

@param s: socket of the user want to delete
*/

void removeUser(SOCKET s) {
	int index = -1;
	for (int i = 0; i < listUserLogin.size(); i++) {
		if (listUserLogin[i].s == s) {
			index = i;
			break;
		}
	}
	//remove in userLogin
	if (index > -1) {
		listUserLogin.erase(listUserLogin.begin() + index);
	}
}

/* function convertIntToChar: convert int to array char

@param value: number want to convert
@param des: array char contains results
*/

void convertIntToChar(size_t value, char des[]) {
	for (int i = 3; i > -1; i--) {
		des[i] = value % 10 + 48;
		value = value / 10;
	}
	des[4] = 0;
}


