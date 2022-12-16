#ifndef _SERVER_H_
#define _SERVER_H_

#pragma once
#define _CRT_SECURE_NO_WARNINGS
#include <WinSock2.h>
#include <WS2tcpip.h>
#pragma comment (lib,"ws2_32.lib")
#include <stdio.h>
#include <math.h>
#include <time.h>
#include <string>
#include <process.h>
#include <windows.h>
#include <conio.h>
#include <vector>
#include <iostream>
#include <chrono>
#include <ctime>    
#include "constants.h"
#include "connection.h"
#include "database.h"
#include "file.h"
#include "room.h"

/* START DEFINE CONSTANTS */
#define NO_CLIENT -1
/* END DEFINE CONSTANTS */

/* START DEFINE VARIABLES */
CLIENT clients[WSA_MAXIMUM_WAIT_EVENTS];
vector<Room> rooms;
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
@function getCurrentTime: Get the server current date and time

@return The string represent server's current time
*/
string getCurrentTime();

/*
@function getEndReason: Generate a string decribe end match reseaon

@param endReasonType: Type of match end
@param winner: The winner's username

@return	The end match reseaon string
*/
string getEndReason(int endReasonType, string winner = "");

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
@function updateMatchPlayers: Update players info after a match to the database

@param winner: The winner of the match
@param loser: The loser of the match
*/
void updateMatchPlayers(CLIENT* winner, CLIENT* loser);

/*
@function updateMatchLog: Create log file and prepare it to send to players

@param aRoom: The room the match was played
@param client1: The winner of the match
@param client2: The loser of the match
@param client1: The winner of the match
@param endReasonType: The type of the match ending
@param winner: The winner of the match
*/
void updateMatchLog(Room* aRoom, CLIENT* client1, CLIENT* client2, int endType, string winner);

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
@function handleRecvPlay: Handle a request that has opcode equals OPCODE_PLAY

@param aClient: The client that requested
*/
void handleRecvPlay(CLIENT* aClient);

/*
@function handleRecvSurrender: Handle a request that has opcode equals OPCODE_SURRENDER

@param aClient: The client that requested
*/
void handleRecvSurrender(CLIENT* aClient);

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
	// Update user in database if user is signed in
	if (aClient->isLoggedIn) {
		updateFreeStatus(aClient->username, UPDATE_USER_NOT_BUSY);
		updateOnlineStatus(aClient->username, UPDATE_USER_STATUS_OFFLINE);
		updateUserCurrentChallenge(aClient->username, "");
		string payload = getFreePlayerList(aClient->username);
		if (!payload.empty()) {
			string s = payload;
			string delimiter = " ";
			size_t pos = 0;
			string token;
			while ((pos = s.find(delimiter)) != std::string::npos) {
				token = s.substr(0, pos);
				char* nameUser = (char*)token.c_str();
				string childPayload = getFreePlayerList(nameUser);
				Send(findClientByUsername(nameUser), OPCODE_LIST_REPLY, (unsigned short)childPayload.size(), (char*)childPayload.c_str());
				s.erase(0, pos + delimiter.length());
			}
		}
	}
	closesocket(aClient->socket);
	// Move the last client to the client at current index
	*aClient = clients[nEvents - 1];
	initClient(&clients[nEvents - 1]);
	// Move the last event to the event at current index
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
	// Check if there is any ongoing file transfer
	if (aClient->fPointer == NULL) {
		Send(aClient, OPCODE_FILE_NO_FILE, 0, NULL);
		return;
	}
	handleSendFile(aClient);
	return;
}

void handleRecvSignUp(CLIENT* aClient) {
	string payload(aClient->buff);
	string username, password;
	// Validate username and password
	if (payload[20] != ' ' || payload.size() == 0) {
		Send(aClient, OPCODE_SIGN_UP_INVALID_USERNAME, 0, NULL);
		return;
	}
	else if (payload.size() > 41 || payload.size() < 22) {
		Send(aClient, OPCODE_SIGN_UP_INVALID_PASSWORD, 0, NULL);
		return;
	}
	else {
		string un = payload.substr(0, 20);
		password = payload.substr(21);
		if (un[19] == ' ') {
			long long index = 19;
			for (long long i = index; i >= 0; i--) {
				if (un[i] != ' ') {
					index = i;
					break;
				}
			}
			username = un.substr(0, index + 1);
		}
		else username = un;

		//Check if username or password contain character " "
		if (username.find(' ') != string::npos) {
			Send(aClient, OPCODE_SIGN_UP_INVALID_USERNAME, 0, NULL);
			return;
		}

		else if (password.find(' ') != string::npos) {
			Send(aClient, OPCODE_SIGN_UP_INVALID_PASSWORD, 0, NULL);
			return;
		}

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
	string payload(aClient->buff);
	string username, password;
	// Validate username and password
	if (payload[20] != ' ' || payload.size() == 0) {
		Send(aClient, OPCODE_SIGN_IN_INVALID_USERNAME, 0, NULL);
		return;
	}
	else if (payload.size() > 41 || payload.size() < 22) {
		Send(aClient, OPCODE_SIGN_IN_INVALID_PASSWORD, 0, NULL);
		return;
	}
	else {
		string un = payload.substr(0, 20);
		password = payload.substr(21);
		if (un[19] == ' ') {
			long long index = 19;
			for (long long i = index; i >= 0; i--) {
				if (un[i] != ' ') {
					index = i;
					break;
				}
			}
			username = un.substr(0, index + 1);
		}
		else username = un;

		//Check if username or password contain character " "
		if (username.find(' ') != string::npos) {
			Send(aClient, OPCODE_SIGN_IN_INVALID_USERNAME, 0, NULL);
			return;
		}

		else if (password.find(' ') != string::npos) {
			Send(aClient, OPCODE_SIGN_IN_INVALID_PASSWORD, 0, NULL);
			return;
		}

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
	string payload = getFreePlayerList(aClient->username);
	Send(aClient, OPCODE_LIST_REPLY, (unsigned short)payload.size(), (char*)payload.c_str());
	if (!payload.empty()) {
		string s = payload;
		string delimiter = " ";
		size_t pos = 0;
		string token;
		while ((pos = s.find(delimiter)) != std::string::npos) {
			token = s.substr(0, pos);
			char* nameUser = (char*)token.c_str();
			string childPayload = getFreePlayerList(nameUser);
			Send(findClientByUsername(nameUser), OPCODE_LIST_REPLY, (unsigned short)childPayload.size(), (char*)childPayload.c_str());
			s.erase(0, pos + delimiter.length());
		}
	}
	return;
}

void handleRecvChallenge(CLIENT* challengeSender) {
	char* challengeReceiverUsername = challengeSender->buff;
	CLIENT* challengeReceiver = findClientByUsername(challengeReceiverUsername);
	// Check if challengeReceiver is online
	if (challengeReceiver == NULL) {
		Send(challengeSender, OPCODE_CHALLENGE_NOT_FOUND, 0, NULL);
		return;
	}
	//Check if challengeReceiver is free
	else if (getFreeStatus(challengeReceiver->username) == UPDATE_USER_BUSY) {
		Send(challengeSender, OPCODE_CHALLENGE_BUSY, 0, NULL);
		return;
	}
	//Check if challengeReceiver is challengeSender
	else if (strcmp(challengeSender->username, challengeReceiverUsername) == 0) {
		Send(challengeSender, OPCODE_CHALLENGE_DUPLICATED_USERNAME, 0, NULL);
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
	//Check if challengeSender is online
	if (challengeSender == NULL) {
		Send(challengeReceiver, OPCODE_CHALLENGE_NOT_FOUND, 0, NULL);
		return;
	}
	//Check if challengeSender did indeed challenged this receiver
	string senderChallenge = getUserCurrentChallenge(challengeSenderUsername);
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
	//Add room
	Room room = Room(challengeSender->socket, challengeReceiver->socket);
	room.setStartTime(getCurrentTime());
	rooms.push_back(room);
}

void handleRecvChallengeRefuse(CLIENT* challengeReceiver) {
	char* challengeSenderUsername = challengeReceiver->buff;
	CLIENT* challengeSender = findClientByUsername(challengeSenderUsername);
	// Check if challengeSender is online
	if (challengeSender == NULL) {
		Send(challengeReceiver, OPCODE_CHALLENGE_NOT_FOUND, 0, NULL);
		return;
	}
	//Check if challengeSender did indeed challenged this receiver
	string senderChallenge = getUserCurrentChallenge(challengeSenderUsername);
	if (strcmp((char*)senderChallenge.c_str(), challengeReceiver->username) != 0) {
		Send(challengeReceiver, OPCODE_CHALLENGE_NOT_FOUND, 0, NULL);
		return;
	}
	updateUserCurrentChallenge(challengeSender->username, "");
	Send(challengeSender, OPCODE_CHALLENGE_REFUSE, (unsigned short) strlen(challengeReceiver->username) + 1, challengeReceiver->username);
	return;
}

void handleRecvInfo(CLIENT* aClient) {
	if (!aClient->isLoggedIn) {
		Send(aClient, OPCODE_INFO_NOT_FOUND, 0, NULL);
		return;
	}
	//Get data from database
	int score = getScore(aClient->username);
	int rank = getRank(aClient->username);
	string msg = to_string(score) + " " + to_string(rank);
	Send(aClient, OPCODE_INFO_FOUND, (unsigned short) msg.size(), (char*)msg.c_str());
	if (!aClient->recvBytes) {
		string payload = getFreePlayerList(aClient->username);
		string s = payload;
		string delimiter = " ";
		size_t pos = 0;
		string token;
		while ((pos = s.find(delimiter)) != std::string::npos) {
			token = s.substr(0, pos);
			char* nameUser = (char*)token.c_str();
			score = getScore(nameUser);
			rank = getRank(nameUser);
			msg = to_string(score) + " " + to_string(rank);
			Send(findClientByUsername(nameUser), OPCODE_INFO_FOUND, (unsigned short)msg.size(), (char*)msg.c_str());
			s.erase(0, pos + delimiter.length());
		}
	}
}

void handleRecvPlay(CLIENT* aClient) {
	Room* aRoom = findRoomBySocket(aClient->socket);
	if (aRoom == NULL || !aRoom->isPlayerTurn(aClient->socket)) {
		Send(aClient, OPCODE_PLAY_INVALID_TURN, 0, NULL);
		return;
	}
	char x, y;
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
	char buff[2], *winnerUsername;
	//Send the received player's move to the opponent
	buff[0] = x;
	buff[1] = y;
	std::cout << "Player " << aClient->username << " move [" << aMove.x << ", " << aMove.y << "]" << std::endl;
	Send(opponentClient, OPCODE_PLAY_OPPONENT, 2, buff);

	//Check for match result then send the result if the match ends
	int matchResult = aRoom->getMatchResult();
	if (matchResult == MATCH_CONTINUE) { return; }
	switch (matchResult) {
	case MATCH_END_BY_DRAW:
		Send(aClient, OPCODE_RESULT, 0, NULL);
		Send(opponentClient, OPCODE_RESULT, 0, NULL);
		updateMatchLog(aRoom, aClient, opponentClient, MATCH_END_BY_DRAW, "");
		break;
	case MATCH_END_BY_WIN:
		winnerUsername = aClient->username;
		Send(aClient, OPCODE_RESULT, (unsigned short)strlen(winnerUsername), winnerUsername);
		Send(opponentClient, OPCODE_RESULT, (unsigned short)strlen(winnerUsername), winnerUsername);
		updateMatchPlayers(aClient, opponentClient);
		updateMatchLog(aRoom, aClient, opponentClient, MATCH_END_BY_WIN, winnerUsername);
		break;
	default:
		break;
	}
	removeRoom(aClient->socket);
}

void handleRecvSurrender(CLIENT* aClient) {
	Room* aRoom = findRoomBySocket(aClient->socket);
	if (aRoom == NULL) {
		Send(aClient, OPCODE_SURRENDER_NO_ROOM, 0, NULL);
		return;
	}

	// Send the opponent match result;
	SOCKET opponentSocket = aRoom->getPlayerOpponent(aClient->socket);
	CLIENT* opponentClient = findClientBySocket(opponentSocket);
	Send(opponentClient, OPCODE_RESULT, (unsigned short)strlen(opponentClient->username), opponentClient->username);
	updateMatchPlayers(opponentClient, aClient);
	updateMatchLog(aRoom, aClient, opponentClient, MATCH_END_BY_SURRENDER, string(opponentClient->username));
	removeRoom(aClient->socket);
}

void updateMatchPlayers(CLIENT* winner, CLIENT* loser) {
	//Update database
	updateScore(winner->username, UPDATE_MATCH_WINNER);
	updateScore(loser->username, UPDATE_MATCH_LOSER);
	updateFreeStatus(winner->username, UPDATE_USER_NOT_BUSY);
	updateFreeStatus(loser->username, UPDATE_USER_NOT_BUSY);
	updateRank();
}

void updateMatchLog(Room* aRoom, CLIENT* client1, CLIENT* client2, int endReasonType, string winner) {
	std::vector<PlayerMove> movesList = aRoom->getMovesList();
	// Create log string
	string logString = getEndReason(endReasonType, winner) + "\n\n"
		+ "Match start at " + aRoom->getStartTime()
		+ "Match end at " + getCurrentTime() + "\n"
		+ "IP Address: " + string(client1->address) + "\tPlayer 1: " + string(client1->username) + "\n"
		+ "IP Address: " + string(client2->address) + "\tPlayer 2: " + string(client2->username) + "\n\n"
		+ "Move Log\n";

	size_t movesCount = movesList.size();
	for (unsigned int i = 0; i < movesCount; i++) {
		string move = "{x: " + to_string(movesList[i].x)
			+ ", y: " + to_string(movesList[i].y)
			+ ", type: " + to_string(movesList[i].type) + "}\n";
		logString.append(move);
	}

	// Create temp file
	char filename[BUFF_SIZE];
	createTempFileName(client1->username, client2->username, filename);
	FILE* logFile = fopen(filename, "w+");
	if (logFile == NULL) {
		Send(client1, OPCODE_FILE_ERROR, 0, NULL);
		Send(client2, OPCODE_FILE_ERROR, 0, NULL);
		return;
	}
	// Write log string to temp file
	fwrite(logString.c_str(), sizeof(char), logString.size(), logFile);
	fclose(logFile);

	// Prepare file to send to players
	prepareClientToSendFile(client1, filename, (int)logString.size());
	prepareClientToSendFile(client2, filename, (int)logString.size());
};

string getCurrentTime() {
	auto end = std::chrono::system_clock::now();
	std::time_t end_time = std::chrono::system_clock::to_time_t(end);
	return string(std::ctime(&end_time));
}

string getEndReason(int endReasonType, string winner) {
	string reason = "";
	switch (endReasonType)
	{
	case MATCH_END_BY_DRAW:
		reason = "Match end by draw. No winner.";
		break;
	case MATCH_END_BY_WIN:
		reason = "Match end by win. Winner: " + winner + ".";
		break;
	case MATCH_END_BY_SURRENDER:
		reason = "Match end by surrender. Winner: " + winner + ".";
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