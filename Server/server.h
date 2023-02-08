#ifndef _SERVER_H_
#define _SERVER_H_
#pragma once
#define _CRT_SECURE_NO_WARNINGS
#include <WinSock2.h>
#include <WS2tcpip.h>
#pragma comment (lib,"WS2_32.lib")
#include "chrono"
#include "constants.h"
#include "connection.h"
#include "database.h"
#include "file.h"
#include "room.h"
#include "roomWithServer.h"
#include "regex"

/* START DEFINE CONSTANTS */
#define NO_CLIENT -1
/* END DEFINE CONSTANTS */

/* START DEFINE VARIABLES */
CLIENT clients[WSA_MAXIMUM_WAIT_EVENTS];
std::vector<Room> rooms;
std::vector<RoomWithServer> roomWithServers;
WSAEVENT events[WSA_MAXIMUM_WAIT_EVENTS];
DWORD nEvents = 0;
DWORD index;
WSANETWORKEVENTS sockEvent;
/* END DEFINE VARIABLES */

/* START DEFINE FUNCITON PROTOTYPES */

/*
@function findClientByUsername: Find a client connection by username

@param username: The client's username

@return	The client that match the username
*/
CLIENT* findClientByUsername(char *username);

/*
@function findClientBySocket: Find a client connection by socket

@param socket: The client's socket

@return	The client that match the socket
*/
CLIENT* findClientBySocket(SOCKET socket);

/*
@function findRoomBySocket: Find a room that the socket is playing in

@param socket: The client's socket

@return	The room that match the socket
*/
Room* findRoomBySocket(SOCKET socket);

/*
@function findRoomWithServerBySocket: Find a room that the socket is playing in with server
 
@param socket: The client's socket

@return	The room with server that match the socket
*/
RoomWithServer* findRoomWithServerBySocket(SOCKET socket);

/*
@function getCurrentTime: Get the server current date and time

@return The std::string represent server's current time
*/
std::string getCurrentTime();

/*
@function getEndReason: Generate a std::string decribe end match reseaon

@param endReasonType: Type of match end
@param winner: The winner's username

@return	The end match reseaon std::string
*/
std::string getEndReason(int endReasonType, std::string winner = "");

/*
@function findClientIndexBySocket: Find a client index based on socket

@param socket: The client's socket

@return: The client index that matches the socket
*/
int findClientIndexBySocket(SOCKET socket);

/*
@function prepareClientToSendFile: Open file and save file size in client

@param aClient: The client needs prepared
@param filename: The file name
@param size: The size of the file
*/
void prepareClientToSendFile(CLIENT* aClient, char* filename, int size);

/*
@function updateMatchPlayers: Update players rank,score after a match to the database

@param winner: The winner of the match
@param loser: The loser of the match
*/
void updateMatchPlayers(CLIENT* winner, CLIENT* loser);

/*
@function updateStatusPlayers: Update players status after a match to the database

@param aClient: Player 1
@param opponentClient: Player 2
*/
void updateStatusPlayers(CLIENT* aClient, CLIENT* opponentClient);

/*
@function updateMatchLog: Create log file and prepare it to send to players

@param aRoom: The room the match was played
@param client1: The winner of the match
@param client2: The loser of the match
@param endReasonType: The type of the match ending
@param winner: The winner of the match
*/
void updateMatchLog(Room* aRoom, CLIENT* client1, CLIENT* client2, int endType, std::string winner);

/*
@function updateMatchLogWithServer: Create log file and prepare it to send to player when play with server

@param aRoom: The room the match was played
@param aClient: The player of the match
@param endReasonType: The type of the match ending
@param winner: The winner of the match
*/
void updateMatchLogWithServer(RoomWithServer* aRoom, CLIENT* aClient, int endReasonType, std::string winner);

/*
@function removeClient: Remove a client from the active clients array in server

@params index: The client's index in the clients[] array
*/
void removeClient(int index);

/*
@function removeRoom: Remove the room that the player is currently in

@param socket: The player's socket
*/
void removeRoom(SOCKET socket);

/*
@function removeRoom: Remove the room that the player is currently in with server

@param socket: The player's socket
*/
void removeRoomWithServer(SOCKET socket);

/*
@function handleRecv: Assign client's request to a suitable handle

@param aClient: The client to reply

*/
void handleRecv(CLIENT* aClient);

/*
@function handleRecvFile: Handle a request that has opcode equals OPCODE_FILE

@param aClient: The client that requested
*/
void handleRecvFile(CLIENT* aClient);

/*
@function handleRecvSignup: Handle a request that has opcode equals OPCODE_SIGN_UP

@param aClient: The client that requested
*/
void handleRecvSignUp(CLIENT* aClient);

/*
@function handleRecvSignin: Handle a request that has opcode equals OPCODE_SIGN_IN

@param aClient: The client that requested
*/
void handleRecvSignIn(CLIENT* aClient);

/*
@function handleRecvSignout: Handle a request that has opcode equals OPCODE_SIGN_OUT

@param aClient: The client that requested
*/
void handleRecvSignOut(CLIENT* aClient);

/*
@function handleRecvList: Handle a request that has opcode equals OPCODE_LIST

@param aClient: The client that requested
*/
void handleRecvList(CLIENT* aClient);

/*
@function handleRecvChanllenge: Handle a request that has opcode equals OPCODE_CHALLENGE

@param challengeSender: The client that requested
*/
void handleRecvChallenge(CLIENT* aClient);

/*
@function handleRecvChanllenge: Handle a request that has opcode equals OPCODE_CHALLENGE_WITH_SERVER

@param challengeSender: The client that requested
*/
void handleRecvChallengeWithServer(CLIENT* aClient);

/*
@function handleRecvChallengeAccept: Handle a request that has opcode equals OPCODE_CHALLENGE_ACCEPT

@param challengeReceiver: The client that requested
*/
void handleRecvChallengeAccept(CLIENT* aClient);

/*
@function handleRecvChallengeRefuse: Handle a request that has opcode equals OPCODE_CHALLENGE_REFUSE

@param challengeReceiver: The client that requested
*/
void handleRecvChallengeRefuse(CLIENT* aClient);

/*
@function handleRecvInfo: Handle a request that has opcode equals OPCODE_INFO

@param aClient: The client that requested
*/
void handleRecvInfo(CLIENT* aClient);

/*
@function handleRecvHistory: Handle a request that has opcode equals OPCODE_HISTORY

@param aClient: The client that requested
*/
void handleRecvHistory(CLIENT* aClient);

/*
@function handleRecvPlay: Handle a request that has opcode equals OPCODE_PLAY

@param aClient: The client that requested
*/
void handleRecvPlay(CLIENT* aClient);

/*
@function handleRecvPlayWithServer: Handle a request that has opcode equals OPCODE_PLAY to server

@param aClient: The client that requested
*/
void handleRecvPlayWithServer(CLIENT* aClient);

/*
@function handleRecvSurrender: Handle a request that has opcode equals OPCODE_SURRENDER

@param aClient: The client that requested
*/
void handleRecvSurrender(CLIENT* aClient);

/*
@function handleRecvSurrenderWithServer: Handle a request that has opcode equals OPCODE_SURRENDER ......

@param aClient: The client that requested
*/
void handleRecvSurrenderWithServer(CLIENT* aClient);

/*
@function handleRecvTimerDraw: Handle a request that has opcode equals OPCODE_TIMER_DRAW

@param aClient: The client that requested
*/
void handleRecvTimerDraw(CLIENT* aClient);

/*
@function handleRecvTimerDrawWithServer: Handle a request that has opcode equals OPCODE_TIMER_DRAW_WITH_SERVER

@param aClient: The client that requested
*/
void handleRecvTimerDrawWithServer(CLIENT* aClient);

/*
@function handleSend: Assign client's reply to a suitable handle

@param aClient: The client to reply
@param index: The client's index

@return The returned code from recv()

*/
int handleSend(CLIENT* aClient, int index);

/*
@function handleSendFile: Handle sending file to client

@param aClient: The client to reply

@return The returned code from recv()

*/
int handleSendFile(CLIENT* aClient);

/* END DEFINE FUNCITON PROTOTYPES */


/* START FUNCITON DEFINITION */

void removeClient(int index) {
	CLIENT* aClient = &clients[index];
	//Update user in database if user is signed in
	if (aClient->isLoggedIn) {
		updateFreeStatus(aClient->username, UPDATE_USER_NOT_BUSY);
		updateOnlineStatus(aClient->username, UPDATE_USER_STATUS_OFFLINE);
		updateUserCurrentChallenge(aClient->username, "");
		std::string payload = getFreePlayerList(aClient->username);
		if (!payload.empty()) {
			std::string s = payload;
			std::string delimiter = " ";
			size_t pos = 0;
			std::string token;
			while ((pos = s.find(delimiter)) != std::string::npos) {
				token = s.substr(0, pos);
				char* nameUser = (char*)token.c_str();
				std::string childPayload = getFreePlayerListInfo(nameUser);
				Send(findClientByUsername(nameUser), OPCODE_LIST_REPLY, (unsigned short)childPayload.size(), (char*)childPayload.c_str());
				s.erase(0, pos + delimiter.length());
			}
		}
	}
	closesocket(aClient->socket);
	//Move the last client to the client at current index
	*aClient = clients[nEvents - 1];
	initClient(&clients[nEvents - 1]);
	//Move the last event to the event at current index
	WSACloseEvent(events[index]);
	events[index] = events[nEvents - 1];
	events[nEvents - 1] = 0;
	nEvents--;
}

void removeRoom(SOCKET socket) {
	unsigned int i;
	for (i = 0; i < rooms.size(); i++)
		if (rooms[i].isPlayerInRoom(socket)) break;
	if (i != rooms.size())
		rooms.erase(rooms.begin() + i);
}

void removeRoomWithServer(SOCKET socket) {
	unsigned int i;
	for (i = 0; i < roomWithServers.size(); i++)
		if (roomWithServers[i].isPlayerInRoom(socket)) break;
	if (i != roomWithServers.size())
		roomWithServers.erase(roomWithServers.begin() + i);
}

CLIENT* findClientByUsername(char *username) {
	for (int i = 0; i < (int)nEvents; i++)
		if (clients[i].isLoggedIn && strcmp(clients[i].username, username) == 0) return &clients[i];
	return NULL;
}

CLIENT* findClientBySocket(SOCKET socket) {
	for (int i = 0; i < (int)nEvents; i++)
		if (clients[i].socket == socket) return &clients[i];
	return NULL;
}

int findClientIndexBySocket(SOCKET socket) {
	int i;
	for (i = 0; i < (int)nEvents; i++)
		if (clients[i].socket == socket) return i;
	return NO_CLIENT;
}

Room* findRoomBySocket(SOCKET socket) {
	for (unsigned int i = 0; i < rooms.size(); i++) {
		if (rooms[i].isPlayerInRoom(socket)) {
			return &rooms[i];
		}
	}
	return NULL;
}

RoomWithServer* findRoomWithServerBySocket(SOCKET socket) {
	for (unsigned int i = 0; i < roomWithServers.size(); i++) {
		if (roomWithServers[i].isPlayerInRoom(socket)) {
			return &roomWithServers[i];
		}
	}
	return NULL;
}

void handleRecv(CLIENT* aClient) {
	switch (aClient->opcode) {
	case OPCODE_PLAY:
		handleRecvPlay(aClient);
		break;
	case OPCODE_LIST:
		handleRecvList(aClient);
		break;
	case OPCODE_INFO:
		handleRecvInfo(aClient);
		break;
	case OPCODE_CHALLENGE:
		handleRecvChallenge(aClient);
		break;
	case OPCODE_CHALLENGE_ACCEPT:
		handleRecvChallengeAccept(aClient);
		break;
	case OPCODE_CHALLENGE_REFUSE:
		handleRecvChallengeRefuse(aClient);
		break;
	case OPCODE_SURRENDER:
		handleRecvSurrender(aClient);
		break;
	case OPCODE_SURRENDER_WITH_SERVER:
		handleRecvSurrenderWithServer(aClient);
		break;
	case OPCODE_FILE:
		handleRecvFile(aClient);
		break;
	case OPCODE_SIGN_IN:
		handleRecvSignIn(aClient);
		break;
	case OPCODE_SIGN_OUT:
		handleRecvSignOut(aClient);
		break;
	case OPCODE_SIGN_UP:
		handleRecvSignUp(aClient);
		break;
	case OPCODE_HISTORY:
		handleRecvHistory(aClient);
		break;
	case OPCODE_TIMER_DRAW:
		handleRecvTimerDraw(aClient);
		break;
	case OPCODE_TIMER_DRAW_WITH_SERVER:
		handleRecvTimerDrawWithServer(aClient);
		break;
	case OPCODE_PLAY_WITH_SERVER:
		handleRecvPlayWithServer(aClient);
		break;
	case OPCODE_CHALLENGE_WITH_SERVER:
		handleRecvChallengeWithServer(aClient);
		break;
	default:
		break;
	}
}

int handleSend(CLIENT* aClient, int index) {
	int ret = 0;
	switch (aClient->opcode) {
	case OPCODE_FILE:
		ret = handleSendFile(aClient);
		if (aClient->bytesInFile == aClient->bytesRead) {
			WSAEventSelect(aClient->socket, events[index], FD_READ | FD_CLOSE);
		}
		break;
	default:
		break;
	}
	return ret;
}

void handleRecvFile(CLIENT* aClient) {
	//Check if there is any ongoing file transfer
	if (aClient->fPointer == NULL) {
		Send(aClient, OPCODE_FILE_NO_FILE, 0, NULL);
		return;
	}
	handleSendFile(aClient);
	return;
}

void handleRecvSignUp(CLIENT* aClient) {
	std::regex reg("^[a-zA-Z0-9]+$");
	std::string payload(aClient->buff);
	//Split username and password and repassword
	std::string username = payload.substr(0, payload.find("|"));
	std::string password = payload.substr(payload.find("|") + 1, payload.find(" ") - size(username) - 1);
	std::string repassword = payload.substr(payload.find(" ") + 1);
	//Check if username is invalid
	if (!std::regex_search(username, reg) || username.size() > 20) {
		Send(aClient, OPCODE_SIGN_UP_INVALID_USERNAME, 0, NULL);
		return;
	}
	//Check if password and repassword are different
	else if (password != repassword) {
		Send(aClient, OPCODE_SIGN_UP_DIFFERENT_REPASSWORD, 0, NULL);
		return;
	}
	//Check for signed up user
	int ret = updateSignUp((char*)username.c_str(), (char*)password.c_str());
	switch (ret) {
	case OPCODE_SIGN_UP_SUCESS:
		Send(aClient, OPCODE_SIGN_UP_SUCESS, 0, NULL);
		break;
	case OPCODE_SIGN_UP_DUPLICATED_USERNAME:
		Send(aClient, OPCODE_SIGN_UP_DUPLICATED_USERNAME, 0, NULL);
		break;
	default:
		Send(aClient, OPCODE_SIGN_UP_UNKNOWN_ERROR, 0, NULL);
		break;
	}
}

void handleRecvSignIn(CLIENT* aClient) {
	std::regex reg("^[a-zA-Z0-9]+$");
	std::string payload(aClient->buff);
	//Split username and password
	std::string username = payload.substr(0, payload.find("|"));
	std::string password = payload.substr(payload.find("|") + 1);
	//Check if username is invalid
	if (!std::regex_search(username, reg) || username.size() > 20) {
		Send(aClient, OPCODE_SIGN_IN_INVALID_USERNAME, 0, NULL);
		return;
	}
	//Check for signed in user
	int ret = updateSignIn((char*)username.c_str(), (char*)password.c_str());
	switch (ret) {
	case OPCODE_SIGN_IN_SUCESS:
		aClient->isLoggedIn = true;
		strcpy_s(aClient->username, (char*)username.c_str());
		Send(aClient, OPCODE_SIGN_IN_SUCESS, 0, NULL);
		break;
	case OPCODE_SIGN_IN_ALREADY_LOGGED_IN:
		Send(aClient, OPCODE_SIGN_IN_ALREADY_LOGGED_IN, 0, NULL);
		break;
	case OPCODE_SIGN_IN_USERNAME_NOT_FOUND:
		Send(aClient, OPCODE_SIGN_IN_USERNAME_NOT_FOUND, 0, NULL);
		break;
	case OPCODE_SIGN_IN_WRONG_PASSWORD:
		Send(aClient, OPCODE_SIGN_IN_WRONG_PASSWORD, 0, NULL);
		break;
	default:
		Send(aClient, OPCODE_SIGN_IN_UNKNOWN_ERROR, 0, NULL);
		break;
	};
}

void handleRecvSignOut(CLIENT* aClient) {
	if (aClient->isLoggedIn) {
		aClient->isLoggedIn = false;
		updateFreeStatus(aClient->username, UPDATE_USER_NOT_BUSY);
		updateOnlineStatus(aClient->username, UPDATE_USER_STATUS_OFFLINE);
		Send(aClient, OPCODE_SIGN_OUT_SUCCESS, 0, NULL);
		return;
	}
	else {
		Send(aClient, OPCODE_SIGN_OUT_NOT_LOGGED_IN, 0, NULL);
		return;
	}
}

void handleRecvList(CLIENT* aClient) {
	std::string payload = getFreePlayerListInfo(aClient->username);
	Send(aClient, OPCODE_LIST_REPLY, (unsigned short)payload.size(), (char*)payload.c_str());
	payload = getFreePlayerList(aClient->username);
	if (!payload.empty()) {
		std::string s = payload;
		std::string delimiter = " ";
		size_t pos = 0;
		std::string token;
		while ((pos = s.find(delimiter)) != std::string::npos) {
			token = s.substr(0, pos);
			char* nameUser = (char*)token.c_str();
			std::string childPayload = getFreePlayerListInfo(nameUser);
			Send(findClientByUsername(nameUser), OPCODE_LIST_REPLY, (unsigned short)childPayload.size(), (char*)childPayload.c_str());
			s.erase(0, pos + delimiter.length());
		}
	}
}

void handleRecvInfo(CLIENT* aClient) {
	//Get data from database
	int score = getScore(aClient->username);
	int rank = getRank(aClient->username);
	std::string msg = std::to_string(score) + " " + std::to_string(rank);
	Send(aClient, OPCODE_INFO_REPLY, (unsigned short)msg.size(), (char*)msg.c_str());
	if (!aClient->recvBytes) {
		std::string payload = getFreePlayerList(aClient->username);
		std::string s = payload;
		std::string delimiter = " ";
		size_t pos = 0;
		std::string token;
		while ((pos = s.find(delimiter)) != std::string::npos) {
			token = s.substr(0, pos);
			char* nameUser = (char*)token.c_str();
			score = getScore(nameUser);
			rank = getRank(nameUser);
			msg = std::to_string(score) + " " + std::to_string(rank);
			Send(findClientByUsername(nameUser), OPCODE_INFO_REPLY, (unsigned short)msg.size(), (char*)msg.c_str());
			s.erase(0, pos + delimiter.length());
		}
	}
}

void handleRecvChallenge(CLIENT* challengeSender) {
	char* challengeReceiverUsername = challengeSender->buff;
	CLIENT* challengeReceiver = findClientByUsername(challengeReceiverUsername);
	//Check if challenge receiver is online
	if (challengeReceiver == NULL) {
		Send(challengeSender, OPCODE_CHALLENGE_NOT_FOUND, 0, NULL);
		return;
	}
	//Check if challenge receiver is free
	else if (getFreeStatus(challengeReceiver->username) == UPDATE_USER_BUSY) {
		Send(challengeSender, OPCODE_CHALLENGE_BUSY, 0, NULL);
		return;
	}
	//Check if 2 players' rank are valid
	int rankDifference = abs(getRank(challengeSender->username) - getRank(challengeReceiverUsername));
	if (rankDifference > 10) {
		Send(challengeSender, OPCODE_CHALLENGE_INVALID_RANK, 0, NULL);
		return;
	}
	else {
		updateUserCurrentChallenge(challengeSender->username, challengeReceiverUsername);
		Send(challengeReceiver, OPCODE_CHALLENGE, (unsigned short)strlen(challengeSender->username) + 1, challengeSender->username);
		return;
	}
}

void handleRecvChallengeAccept(CLIENT* challengeReceiver) {
	char* challengeSenderUsername = challengeReceiver->buff;
	CLIENT* challengeSender = findClientByUsername(challengeSenderUsername);
	//Check if challenge sender is online
	if (challengeSender == NULL) {
		Send(challengeReceiver, OPCODE_CHALLENGE_NOT_FOUND, 0, NULL);
		return;
	}
	//Check if challenge sender did indeed challenged this receiver
	std::string senderChallenge = getUserCurrentChallenge(challengeSenderUsername);
	if (strcmp((char*)senderChallenge.c_str(), challengeReceiver->username) != 0) {
		Send(challengeReceiver, OPCODE_CHALLENGE_NOT_FOUND, 0, NULL);
		return;
	}
	//Update database
	updateFreeStatus(challengeReceiver->username, UPDATE_USER_BUSY);
	updateFreeStatus(challengeSenderUsername, UPDATE_USER_BUSY);
	updateUserCurrentChallenge(challengeSender->username, "");
	Send(challengeSender, OPCODE_CHALLENGE_ACCEPT, (unsigned short) size(senderChallenge), (char*)senderChallenge.c_str());
	Send(challengeReceiver, OPCODE_CHALLENGE_ACCEPT, 0, NULL);
	std::cout << "[" << challengeSender->username << ", " << challengeReceiver->username << "] created a room" << std::endl;
	//Add room
	Room room = Room(challengeSender->socket, challengeReceiver->socket);
	room.setStartTime(getCurrentTime());
	rooms.push_back(room);
}

void handleRecvChallengeWithServer(CLIENT *aClient) {
	updateFreeStatus(aClient->username, UPDATE_USER_BUSY);
	Send(aClient, OPCODE_CHALLENGE_WITH_SERVER_PLAY, 0, NULL);
	std::cout << "[" << aClient->username << ", Server] created a room " << std::endl;
	//Add room
	RoomWithServer roomWithServer = RoomWithServer(aClient->socket);
	roomWithServer.setStartTime(getCurrentTime());
	roomWithServers.push_back(roomWithServer);
}

void handleRecvChallengeRefuse(CLIENT* challengeReceiver) {
	char* challengeSenderUsername = challengeReceiver->buff;
	CLIENT* challengeSender = findClientByUsername(challengeSenderUsername);
	//Check if challenge sender is online
	if (challengeSender == NULL) {
		Send(challengeReceiver, OPCODE_CHALLENGE_NOT_FOUND, 0, NULL);
		return;
	}
	//Check if challenge sender did indeed challenged this receiver
	std::string senderChallenge = getUserCurrentChallenge(challengeSenderUsername);
	if (strcmp((char*)senderChallenge.c_str(), challengeReceiver->username) != 0) {
		Send(challengeReceiver, OPCODE_CHALLENGE_NOT_FOUND, 0, NULL);
		return;
	}
	updateUserCurrentChallenge(challengeSender->username, "");
	Send(challengeSender, OPCODE_CHALLENGE_REFUSE, (unsigned short) strlen(challengeReceiver->username) + 1, challengeReceiver->username);
	return;
}

void handleRecvHistory(CLIENT* aClient) {
	//Get data from database
	std::string payload = getHistory(aClient->username);
	Send(aClient, OPCODE_HISTORY_REPLY, (unsigned short)payload.size(), (char*)payload.c_str());
}

void handleRecvPlay(CLIENT* aClient) {
	Room* aRoom = findRoomBySocket(aClient->socket);
	if (aRoom == NULL || !aRoom->isPlayerTurn(aClient->socket)) {
		Send(aClient, OPCODE_PLAY_INVALID_TURN, 0, NULL);
		return;
	}
	char x, y, * winnerUsername;
	int moveType = aRoom->getPlayerMoveType(aClient->socket);
	getPlayerMoveCoordinate(aClient, &x, &y);
	PlayerMove aMove = { x, y, moveType };
	int ret = aRoom->addPlayerMove(aMove);
	if (ret != OPCODE_PLAY) {
		Send(aClient, (char)ret, 0, NULL);
		return;
	}
	SOCKET opponentSocket = aRoom->getPlayerOpponent(aClient->socket);
	CLIENT* opponentClient = findClientBySocket(opponentSocket);
	char coordinateReply[2] = {0};
	//Send the received player's move to the opponent
	coordinateReply[0] = x;
	coordinateReply[1] = y;
	std::cout << aClient->username << " move [" << aMove.x << ", " << aMove.y << "]" << std::endl;
	Send(opponentClient, OPCODE_PLAY_OPPONENT, 2, coordinateReply);
	//Check for match result then send the result if the match ends
	int matchResult = aRoom->getMatchResult();
	if (matchResult == MATCH_CONTINUE) { return; }
	switch (matchResult) {
	case MATCH_END_BY_DRAW:
		Send(aClient, OPCODE_RESULT, 0, NULL);
		Send(opponentClient, OPCODE_RESULT, 0, NULL);
		updateStatusPlayers(aClient, opponentClient);
		updateMatchLog(aRoom, aClient, opponentClient, MATCH_END_BY_DRAW, "No one");
		break;
	case MATCH_END_BY_WIN:
		winnerUsername = aClient->username;
		Send(aClient, OPCODE_RESULT, (unsigned short)strlen(winnerUsername), winnerUsername);
		Send(opponentClient, OPCODE_RESULT, (unsigned short)strlen(winnerUsername), winnerUsername);
		updateMatchPlayers(aClient, opponentClient);
		updateStatusPlayers(aClient, opponentClient);
		updateMatchLog(aRoom, aClient, opponentClient, MATCH_END_BY_WIN, winnerUsername);
		break;
	default:
		break;
	}
	std::cout << "[" << aClient->username << ", " << opponentClient->username << "] removed a room" << std::endl;
	removeRoom(aClient->socket);
}

void handleRecvPlayWithServer(CLIENT* aClient) {
	RoomWithServer* aRoomWithServer = findRoomWithServerBySocket(aClient->socket);
	if (aRoomWithServer == NULL || !aRoomWithServer->isPlayerTurn(aClient->socket)) {
		Send(aClient, OPCODE_PLAY_INVALID_TURN_SERVER, 0, NULL);
		return;
	}
	char x, y, *winnerName;
	getPlayerMoveCoordinate(aClient, &x, &y);
	PlayerMove aMoveClient = { x, y, TYPE_O };
	int ret = aRoomWithServer->addPlayerMove(aMoveClient);
	if (ret != OPCODE_PLAY_WITH_SERVER) {
		Send(aClient, (char)ret, 0, NULL);
		return;
	}
	std::cout << aClient->username << " move [" << aMoveClient.x << ", " << aMoveClient.y << "]" << std::endl;
	//Check for match result then send the result if the match ends
	int matchResult = aRoomWithServer->getMatchResult();
	if (matchResult != MATCH_CONTINUE) 
	{
		switch (matchResult) {
		case MATCH_END_BY_DRAW:
			Send(aClient, OPCODE_RESULT, 0, NULL);
			updateMatchLogWithServer(aRoomWithServer, aClient, MATCH_END_BY_DRAW, "No one");
			updateFreeStatus(aClient->username, UPDATE_USER_NOT_BUSY);
			break;
		case MATCH_END_BY_WIN:
			winnerName = aClient->username;
			Send(aClient, OPCODE_RESULT, (unsigned short)strlen(winnerName), winnerName);
			updateMatchLogWithServer(aRoomWithServer, aClient, MATCH_END_BY_WIN, winnerName);
			updateFreeStatus(aClient->username, UPDATE_USER_NOT_BUSY);
			break;
		default:
			break;
		}
		std::cout << "[" << aClient->username << ", Server] removed a room" << std::endl;
		removeRoomWithServer(aClient->socket);
		return;
	}
	char coordinateReply[2];
	int coordinateServer[2];
	aRoomWithServer->caculateChess(coordinateServer);
	memcpy(coordinateReply, coordinateServer, 1);
	memcpy(coordinateReply + 1, coordinateServer + 1, 1);
	PlayerMove aMoveServer = { coordinateServer[0], coordinateServer[1], TYPE_X};
	ret = aRoomWithServer->addPlayerMove(aMoveServer);
	if (ret != OPCODE_PLAY_WITH_SERVER) {
		Send(aClient, (char)ret, 0, NULL);
		return;
	}
	std::cout << "Server move [" << coordinateServer[0] << ", " << coordinateServer[1] << "]" << std::endl;
	Send(aClient, OPCODE_PLAY_REPLY_SERVER, 2, coordinateReply);
	//Check for match result then send the result if the match ends
	matchResult = aRoomWithServer->getMatchResult();
	if (matchResult == MATCH_CONTINUE) { return; }
	switch (matchResult) {
	case MATCH_END_BY_DRAW:
		Send(aClient, OPCODE_RESULT, 0, NULL);
		updateMatchLogWithServer(aRoomWithServer, aClient, MATCH_END_BY_DRAW, "No one");
		updateFreeStatus(aClient->username, UPDATE_USER_NOT_BUSY);
		break;
	case MATCH_END_BY_WIN:
		winnerName = "Server";
		Send(aClient, OPCODE_RESULT, (unsigned short)strlen(winnerName), winnerName);
		updateMatchLogWithServer(aRoomWithServer, aClient, MATCH_END_BY_WIN, winnerName);
		updateFreeStatus(aClient->username, UPDATE_USER_NOT_BUSY);
		break;
	default:
		break;
	}
	std::cout << "[" << aClient->username << ", Server] removed a room" << std::endl;
	removeRoomWithServer(aClient->socket);
}

void handleRecvSurrender(CLIENT* aClient) {
	Room* aRoom = findRoomBySocket(aClient->socket);
	//Send the opponent match result;
	SOCKET opponentSocket = aRoom->getPlayerOpponent(aClient->socket);
	CLIENT* opponentClient = findClientBySocket(opponentSocket);
	Send(opponentClient, OPCODE_RESULT, (unsigned short)strlen(opponentClient->username), opponentClient->username);
	updateMatchPlayers(opponentClient, aClient);
	updateStatusPlayers(aClient, opponentClient);
	updateMatchLog(aRoom, aClient, opponentClient, MATCH_END_BY_SURRENDER, std::string(opponentClient->username));
	std::cout << "[" << aClient->username << ", " << opponentClient->username << "] removed a room" << std::endl;
	removeRoom(aClient->socket);
}

void handleRecvSurrenderWithServer(CLIENT* aClient) {
	RoomWithServer* aRoomWithServer = findRoomWithServerBySocket(aClient->socket);
	updateFreeStatus(aClient->username, UPDATE_USER_NOT_BUSY);
	updateMatchLogWithServer(aRoomWithServer, aClient, MATCH_END_BY_SURRENDER, "Server");
	std::cout << "[" << aClient->username << ", Server] removed a room" << std::endl;
	removeRoomWithServer(aClient->socket);
}

void handleRecvTimerDraw(CLIENT* aClient) {
	Room* aRoom = findRoomBySocket(aClient->socket);
	//Send the opponent match result;
	SOCKET opponentSocket = aRoom->getPlayerOpponent(aClient->socket);
	CLIENT* opponentClient = findClientBySocket(opponentSocket);
	Send(aClient, OPCODE_RESULT, 0, NULL);
	Send(opponentClient, OPCODE_RESULT, 0, NULL);
	updateStatusPlayers(aClient, opponentClient);
	updateMatchLog(aRoom, aClient, opponentClient, MATCH_END_BY_DRAW, "No one");
	removeRoom(aClient->socket);
}

void handleRecvTimerDrawWithServer(CLIENT* aClient) {
	RoomWithServer* aRoomWithServer = findRoomWithServerBySocket(aClient->socket);
	Send(aClient, OPCODE_RESULT, 0, NULL);
	updateFreeStatus(aClient->username, UPDATE_USER_NOT_BUSY);
	updateMatchLogWithServer(aRoomWithServer, aClient, MATCH_END_BY_DRAW, "No one");
	removeRoomWithServer(aClient->socket);
}

void updateMatchPlayers(CLIENT* winner, CLIENT* loser) {
	//Update database
	updateScore(winner->username, UPDATE_MATCH_WINNER);
	updateScore(loser->username, UPDATE_MATCH_LOSER);
	updateRank();
}

void updateStatusPlayers(CLIENT* aClient, CLIENT* opponentClient) {
	//Update database
	updateFreeStatus(aClient->username, UPDATE_USER_NOT_BUSY);
	updateFreeStatus(opponentClient->username, UPDATE_USER_NOT_BUSY);
}

void updateMatchLog(Room* aRoom, CLIENT* client1, CLIENT* client2, int endReasonType, std::string winner) {
	std::vector<PlayerMove> movesList = aRoom->getMovesList();
	//Create log string
	std::string logstring = getEndReason(endReasonType, winner) + "\n\n"
		+ "Match start at " + aRoom->getStartTime()
		+ "Match end at " + getCurrentTime() + "\n"
		+ "IP Address: " + std::string(client1->address) + "\tPlayer 1: " + std::string(client1->username) + "\n"
		+ "IP Address: " + std::string(client2->address) + "\tPlayer 2: " + std::string(client2->username) + "\n\n"
		+ "Move Log\n";
	switch (endReasonType) {
	case MATCH_END_BY_DRAW:
		updateHistory(std::string(client1->username), std::string(client2->username), "Draw", winner, aRoom->getStartTime(), getCurrentTime());
		break;
	case MATCH_END_BY_WIN:
		updateHistory(std::string(client1->username), std::string(client2->username), "Win", winner, aRoom->getStartTime(), getCurrentTime());
		break;
	case MATCH_END_BY_SURRENDER:
		updateHistory(std::string(client1->username), std::string(client2->username), "Surrender", winner, aRoom->getStartTime(), getCurrentTime());
		break;
	default:
		break;
	}
	size_t movesCount = movesList.size();
	for (unsigned int i = 0; i < movesCount; i++) {
		std::string move = "{x: " + std::to_string(movesList[i].x)
			+ ", y: " + std::to_string(movesList[i].y)
			+ ", type: " + std::to_string(movesList[i].type) + "}\n";
		logstring.append(move);
	}
	//Create temp file
	char filename[BUFF_SIZE];
	createTempFileName(client1->username, client2->username, filename);
	FILE* logFile = fopen(filename, "w+");
	if (logFile == NULL) {
		Send(client1, OPCODE_FILE_ERROR, 0, NULL);
		Send(client2, OPCODE_FILE_ERROR, 0, NULL);
		return;
	}
	//Write log string to temp file
	fwrite(logstring.c_str(), sizeof(char), logstring.size(), logFile);
	fclose(logFile);
	//Prepare file to send to players
	prepareClientToSendFile(client1, filename, (int)logstring.size());
	prepareClientToSendFile(client2, filename, (int)logstring.size());
};

void updateMatchLogWithServer(RoomWithServer* aRoomWithServer, CLIENT* aClient, int endReasonType, std::string winner) {
	std::vector<PlayerMove> movesList = aRoomWithServer->getMovesList();
	//Create log string
	std::string logstring = getEndReason(endReasonType, winner) + "\n\n"
		+ "Match start at " + aRoomWithServer->getStartTime()
		+ "Match end at " + getCurrentTime() + "\n"
		+ "IP Address: " + std::string(aClient->address) + "\tPlayer 1: " + std::string(aClient->username) + "\n"
		+ "IP Address: " + "IP of Server" + "\tPlayer 2: " + "Server" + "\n\n"
		+ "Move Log\n";
	switch (endReasonType) {
	case MATCH_END_BY_DRAW:
		updateHistory(std::string(aClient->username), "Server", "Draw", winner, aRoomWithServer->getStartTime(), getCurrentTime());
		break;
	case MATCH_END_BY_WIN:
		updateHistory(std::string(aClient->username), "Server", "Win", winner, aRoomWithServer->getStartTime(), getCurrentTime());
		break;
	case MATCH_END_BY_SURRENDER:
		updateHistory(std::string(aClient->username), "Server", "Surrender", winner, aRoomWithServer->getStartTime(), getCurrentTime());
		break;
	default:
		break;
	}
	size_t movesCount = movesList.size();
	for (unsigned int i = 0; i < movesCount; i++) {
		std::string move = "{x: " + std::to_string(movesList[i].x)
			+ ", y: " + std::to_string(movesList[i].y)
			+ ", type: " + std::to_string(movesList[i].type) + "}\n";
		logstring.append(move);
	}
	//Create temp file
	char filename[BUFF_SIZE];
	createTempFileName(aClient->username, "Server", filename);
	FILE* logFile = fopen(filename, "w+");
	if (logFile == NULL) {
		Send(aClient, OPCODE_FILE_ERROR, 0, NULL);
		return;
	}
	//Write log string to temp file
	fwrite(logstring.c_str(), sizeof(char), logstring.size(), logFile);
	fclose(logFile);
	//Prepare file to send to player
	prepareClientToSendFile(aClient, filename, (int)logstring.size());
};

std::string getCurrentTime() {
	auto end = std::chrono::system_clock::now();
	std::time_t end_time = std::chrono::system_clock::to_time_t(end);
	return std::string(std::ctime(&end_time));
}

std::string getEndReason(int endReasonType, std::string winner) {
	std::string reason = "";
	switch (endReasonType) {
	case MATCH_END_BY_DRAW:
		reason = "Match end by draw. No winner";
		break;
	case MATCH_END_BY_WIN:
		reason = "Match end by win. Winner: " + winner;
		break;
	case MATCH_END_BY_SURRENDER:
		reason = "Match end by surrender. Winner: " + winner;
		break;
	default:
		break;
	}
	return reason;
}

void prepareClientToSendFile(CLIENT* aClient, char* filename, int size) {
	aClient->fPointer = fopen(filename, "r");
	aClient->bytesInFile = size;
}

int handleSendFile(CLIENT* aClient) {
	int ret;
	if (aClient->fPointer == NULL) {
		ret = Send(aClient, OPCODE_FILE_ERROR, 0, NULL);
		closeOpenendFile(aClient);
		return ret;
	}
	size_t bytesToSend, bytesLeft, bytesCanFitInBuff;
	char buff[BUFF_SIZE];
	bytesCanFitInBuff = BUFF_SIZE - OPCODE_SIZE - LENGTH_SIZE;
	bytesLeft = aClient->bytesInFile - aClient->bytesRead;
	bytesToSend = bytesLeft > bytesCanFitInBuff ? bytesCanFitInBuff : bytesLeft;
	bytesCanFitInBuff = fread(buff, sizeof(char), bytesToSend, aClient->fPointer);
	ret = Send(aClient, OPCODE_FILE_DATA, (unsigned short) bytesCanFitInBuff, buff);
	if (ret <= 0) return ret;
	aClient->bytesRead += ret - OPCODE_SIZE - LENGTH_SIZE;
	if (aClient->bytesInFile == aClient->bytesRead) {
		fclose(aClient->fPointer);
		Send(aClient, OPCODE_FILE_DATA, 0, NULL);
	}
	return ret;
}
/* END FUNCITON DEFINITION */
#endif