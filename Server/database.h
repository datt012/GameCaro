#ifndef _DATABASE_
#define _DATABASE_

#pragma once
#include <iostream>
#include <string>
#include <vector>
#include <windows.h>
#include <sqlext.h>
#include <sqltypes.h>
#include <sql.h>
#include "constants.h"


#define UPDATE_MATCH_LOSER 0
#define UPDATE_MATCH_WINNER 1

/* freeStatus */
#define UPDATE_USER_BUSY 0
#define UPDATE_USER_NOT_BUSY 1

/* onlineStatus */
#define UPDATE_USER_STATUS_OFFLINE 0
#define UPDATE_USER_STATUS_ONLINE 1

using namespace std;
#define SQL_RESULT_LEN 240
#define SQL_RETURN_CODE_LEN 1024
#define SCORE 3


/*
@Function connectDatabase: Connect to database
@Return: True if connect successful
         False if connect fail
*/
bool connectDatabase();

/*
@Function disconnectDatabase: Disconnect to database
@Return: None
*/
void disconnectDatabase();

/*
@Function showSQLError: Show SQL error
@Return: None
*/
void showSQLError(unsigned int handleType, const SQLHANDLE& handle);

/*
@Function getUserCurrentChallenge: Get challenged person of user
@Param username: Username of user
@Return : Username of challenged person
*/
string getUserCurrentChallenge(char *username);

/*
@Function getFreePlayerList: Get all user signed in and is free state
@Param username: Username of user
@Return: String list username of user signed in and is free state
*/
string getFreePlayerList(char *username);

/*
@Function getRank: Get rank of user
@Param username: Username of user
@Return: Rank of user
*/
int getRank(char *username);

/*
@Function getFreeStatus: Get status free of user
@Param username: Username of user
@Return: Status free of user
*/
int getFreeStatus(char *username);

/*
@Function getScore: Get score of user
@Param username: Username of user
@Return: Score of user
*/
int getScore(char *username);

/*
@Function updateSignIn: Update sign in status in database
@Param username: Username of user
@Param password: Password of user
@Return: Opcode
*/
int updateSignIn(char *username, char *password);

/*
@Function updateSignUp: Update sign up status in database
@Param username: Username of user
@Param password: Password of user
@Return: Opcode
*/
int updateSignUp(char *username, char *password);

/*
@Function updateOnlineStatus: update status of user
@Param username: username of user
@Param status: status of user
*/
void updateOnlineStatus(char *username, int onlineStatus);

/*
@Function updateRank: update rank of all user
@Return: None
*/
void updateRank();

/*
@Function updateFreeStatus: Update free status of user in database
@Param username: Username of user
@Param freeStatus: Free status of user
@Return: None
*/
void updateFreeStatus(char *username, int freeStatus);

/*
@Function updateScore: Update score of user
@Param username: Username of user
@Param win: Chess game results of user
@Return: None
*/
void updateScore(char *username, int win);

/*
@Function updateUserCurrentChallenge: Update challenged person of user
@Param username: Username of user
@Param usernameChallenge: Username of challenged person
@Return: None
*/
void updateUserCurrentChallenge(char *username, char *usernameChallenge);


void updateHistory(string player1, string player2, string matchEndBy, string winner, string timeStart, string timeEnd);

string getHistory(char* username);



SQLHENV  SQLEnvHandle = NULL;
SQLHDBC  SQLConnectionHandle = NULL;
SQLHSTMT SQLStatementHandle = NULL;

bool connectDatabase() {
	SQLRETURN retCode = 0;
	bool val = false;
	do {
		//Allocates the environment
		if (SQL_SUCCESS != SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &SQLEnvHandle))
			break;

		//Sets attributes that govern aspects of environments
		if (SQL_SUCCESS != SQLSetEnvAttr(SQLEnvHandle, SQL_ATTR_ODBC_VERSION, (SQLPOINTER)SQL_OV_ODBC3, 0))
			break;

		//Allocates the connection
		if (SQL_SUCCESS != SQLAllocHandle(SQL_HANDLE_DBC, SQLEnvHandle, &SQLConnectionHandle))
			break;

		//Sets attributes that govern aspects of connections
		if (SQL_SUCCESS != SQLSetConnectAttr(SQLConnectionHandle, SQL_LOGIN_TIMEOUT, (SQLPOINTER)5, 0))
			break;

		SQLCHAR retConString[SQL_RETURN_CODE_LEN];
		switch (SQLDriverConnect(SQLConnectionHandle, NULL,
			(SQLCHAR*)"DRIVER={SQL Server}; SERVER=DATTT, 1433; DATABASE=GameCaro; UID=sa; PWD=tiendat2101;",
			SQL_NTS, retConString, 1024, NULL, SQL_DRIVER_NOPROMPT)) {
			//Connect to a data source using a connection string
		case SQL_SUCCESS:
			val = true;
			break;
		case SQL_SUCCESS_WITH_INFO:
			val = true;
			break;
		case SQL_NO_DATA_FOUND:
			showSQLError(SQL_HANDLE_DBC, SQLConnectionHandle);
			retCode = -1;
			break;
		case SQL_INVALID_HANDLE:
			showSQLError(SQL_HANDLE_DBC, SQLConnectionHandle);
			retCode = -1;
			break;
		case SQL_ERROR:
			showSQLError(SQL_HANDLE_DBC, SQLConnectionHandle);
			retCode = -1;
			break;
		default:
			break;
		}

		if (retCode == -1)
			break;

		//Allocates the statement
		if (SQL_SUCCESS != SQLAllocHandle(SQL_HANDLE_STMT, SQLConnectionHandle, &SQLStatementHandle))
			break;

	} while (FALSE);
	return val;
}


void disconnectDatabase() {
	SQLFreeHandle(SQL_HANDLE_STMT, SQLStatementHandle);
	SQLDisconnect(SQLConnectionHandle);
	SQLFreeHandle(SQL_HANDLE_DBC, SQLConnectionHandle);
	SQLFreeHandle(SQL_HANDLE_ENV, SQLEnvHandle);
	SQLEnvHandle = NULL;
	SQLConnectionHandle = NULL;
	SQLStatementHandle = NULL;
}


void showSQLError(unsigned int handleType, const SQLHANDLE& handle)
{
	SQLCHAR SQLState[1024];
	SQLCHAR message[1024];
	if (SQL_SUCCESS == SQLGetDiagRec(handleType, handle, 1, SQLState, NULL, message, 1024, NULL))
		cout << "SQL driver message: " << message << "\nSQL state: " << SQLState << "." << endl;
}


int updateSignUp(char *username, char *password) {
	string SQLQuery = "INSERT INTO information VALUES('" + string(username) + "', '" + string(password) + "', 0, 0, 1, 0, NULL);";
	if (connectDatabase()) {
		//Executes a preparable statement
		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
			disconnectDatabase();
			return OPCODE_SIGN_UP_DUPLICATED_USERNAME;
		}
		else {
			disconnectDatabase();
			return OPCODE_SIGN_UP_SUCESS;
		}
	}
	else return OPCODE_SIGN_UP_UNKNOWN_ERROR;
}


int updateSignIn(char *username, char *password) {
	string SQLQuery = "SELECT password, onlineStatus FROM information WHERE username='" + string(username) + "'";
	if (connectDatabase()) {
		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
			disconnectDatabase();
			return OPCODE_SIGN_IN_UNKNOWN_ERROR;
		}
		else
		{
			char pass[PASSWORD_SIZE] = "";
			int onlineStatus = 0;
			while (SQLFetch(SQLStatementHandle) == SQL_SUCCESS) {
				SQLGetData(SQLStatementHandle, 1, SQL_C_DEFAULT, &pass, sizeof(pass), NULL);
				SQLGetData(SQLStatementHandle, 2, SQL_C_ULONG, &onlineStatus, 0, NULL);
			}
			//Not found player
			if (strlen(pass) == 0) {
				disconnectDatabase();
				return OPCODE_SIGN_IN_USERNAME_NOT_FOUND;
			}
			//Incorrect password
			else if (strcmp(pass, password) != 0) {
				disconnectDatabase();
				return OPCODE_SIGN_IN_WRONG_PASSWORD;
			}
			else {
				//User signed in
				if (onlineStatus == UPDATE_USER_STATUS_ONLINE) {
					disconnectDatabase();
					return OPCODE_SIGN_IN_ALREADY_LOGGED_IN;
				}
				else {
					updateOnlineStatus(username, UPDATE_USER_STATUS_ONLINE);
					disconnectDatabase();
					return OPCODE_SIGN_IN_SUCESS;
				}
			}
		}
	}
	else return OPCODE_SIGN_IN_UNKNOWN_ERROR;
}


void updateFreeStatus(char *username, int freeStatus) {
	string SQLQuery = "UPDATE information SET freeStatus=" + to_string(freeStatus) + " WHERE username='" + string(username) + "'";
	if (connectDatabase()) {
		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
			disconnectDatabase();
		}
	}
}



void updateUserCurrentChallenge(char *username, char *usernameChallenge) {
	string SQLQuery;
	if (strlen(usernameChallenge) > 0) {
		SQLQuery = "UPDATE information SET currentChallenge='" + string(usernameChallenge) + "' WHERE username='" + string(username) + "'";
	}
	else
	{
		SQLQuery = "UPDATE information SET currentChallenge=NULL WHERE username='" + string(username) + "'";
	}
	if (connectDatabase()) {
		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
			disconnectDatabase();
		}
	}
}


string getUserCurrentChallenge(char *username) {
	char userChallenge[USERNAME_SIZE] = { 0 };
	string SQLQuery = "SELECT currentChallenge FROM information WHERE username='" + string(username) + "'";
	if (connectDatabase()) {
		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
			disconnectDatabase();
		}
		else
		{
			while (SQLFetch(SQLStatementHandle) == SQL_SUCCESS) {
				SQLGetData(SQLStatementHandle, 1, SQL_C_DEFAULT, &userChallenge, sizeof(userChallenge), NULL);
			}
			disconnectDatabase();
		}
	}
	return string(userChallenge);
}


int getRank(char *username) {
	int rank = 0;
	string SQLQuery = "SELECT rank FROM information WHERE username='" + string(username) + "'";
	if (connectDatabase()) {
		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
			disconnectDatabase();
			return 0;
		}
		else
		{
			while (SQLFetch(SQLStatementHandle) == SQL_SUCCESS) {
				SQLGetData(SQLStatementHandle, 1, SQL_C_ULONG, &rank, 0, NULL);
			}
			disconnectDatabase();
		}
	}
	return rank;
}


int getFreeStatus(char *username) {
	unsigned int freeStatus = 0, onlineStatus = 0;
	string SQLQuery = "SELECT freeStatus, onlineStatus FROM information WHERE username='" + string(username) + "'";
	if (connectDatabase()) {
		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
			disconnectDatabase();
			return 0;
		}
		else
		{
			while (SQLFetch(SQLStatementHandle) == SQL_SUCCESS) {
				SQLGetData(SQLStatementHandle, 1, SQL_C_ULONG, &freeStatus, 0, NULL);
				SQLGetData(SQLStatementHandle, 2, SQL_C_ULONG, &onlineStatus, 0, NULL);
			}
			disconnectDatabase();
		}
	}
	return (freeStatus && onlineStatus);
}

int getScore(char *username) {
	int score = 0;
	string SQLQuery = "SELECT score FROM information WHERE username='" + string(username) + "'";
	if (connectDatabase()) {
		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
			disconnectDatabase();
			return 0;
		}
		else
		{
			while (SQLFetch(SQLStatementHandle) == SQL_SUCCESS) {
				SQLGetData(SQLStatementHandle, 1, SQL_C_ULONG, &score, 0, NULL);
			}
			disconnectDatabase();
		}
	}
	return score;
}


void updateOnlineStatus(char *username, int onlineStatus) {
	string SQLQuery = "UPDATE information SET onlineStatus=" + to_string(onlineStatus) + " WHERE username='" + string(username) + "'";
	if (connectDatabase()) {
		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
		}
		disconnectDatabase();
	}
}


void updateScore(char *username, int win) {
	int score = getScore(username);
	if (win < 2) { //Check if win is 0 or 1
		if (win) {
			score += SCORE;
		}
		else
		{
			score -= SCORE;
			if (score < 0) score = 0;
		}

		string SQLQuery = "UPDATE information SET score=" + to_string(score) + " WHERE username='" + username + "'";
		if (connectDatabase()) {
			if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery.c_str(), SQL_NTS))
			{
				showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
			}
			disconnectDatabase();
		}
	}
}

void updateRank() {
	struct userScore {
		char username[USERNAME_SIZE];
		int score;

		userScore(char name[USERNAME_SIZE], int scoreUser) {
			strcpy_s(username, name);
			score = scoreUser;
		}
	};
	int rankUser = 1, scoreUser = -1;
	char username[USERNAME_SIZE];
	int score;
	vector<userScore> listScore;
	string SQLQuery1 = "SELECT username, score FROM information ORDER BY score DESC";

	if (connectDatabase()) {
		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery1.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
		}
		else
		{
			while (SQLFetch(SQLStatementHandle) == SQL_SUCCESS) {
				SQLGetData(SQLStatementHandle, 1, SQL_C_DEFAULT, &username, sizeof(username), NULL);
				SQLGetData(SQLStatementHandle, 2, SQL_C_ULONG, &score, 0, NULL);
				listScore.push_back(userScore(username, score)); //List username vs score sorted with descending order
			}
		}
		disconnectDatabase();
	}
	if (connectDatabase()) {
		scoreUser = listScore[0].score;
		string SQLQuery2 = "UPDATE information SET rank=" + to_string(rankUser) + " WHERE username='" + string(listScore[0].username) + "'";

		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery2.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
		}
		else {
			for (unsigned int i = 1; i < listScore.size(); i++) {
				if (scoreUser != listScore[i].score) {
					rankUser++;
					scoreUser = listScore[i].score;
				}
				SQLQuery2 = "UPDATE information SET rank=" + to_string(rankUser) + " WHERE username='" + string(listScore[i].username) + "'";

				if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery2.c_str(), SQL_NTS))
				{
					showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
				}
			}
		}
		disconnectDatabase();
	}
}

string getFreePlayerList(char *username) {
	string listFreePlayer = "";
	string SQLQuery = "SELECT username FROM information WHERE onlineStatus=1 AND freeStatus=1 AND username != '" + string(username) + "'";
	if (connectDatabase()) {
		char userFree[USERNAME_SIZE] = { 0 };
		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
		}
		else
		{
			while (SQLFetch(SQLStatementHandle) == SQL_SUCCESS) {
				SQLGetData(SQLStatementHandle, 1, SQL_C_DEFAULT, &userFree, sizeof(userFree), NULL);
				listFreePlayer = listFreePlayer + userFree + " "; //Each username space delimiter " "
			}
		}
		disconnectDatabase();
	}
	return listFreePlayer;
}

void updateHistory(string player1, string player2, string matchEndBy, string winner, string timeStart, string timeEnd) {
	string SQLQuery = "INSERT INTO history VALUES ('" + player1 + "','" + player2 + "','" + matchEndBy + "','" + winner + "','" + timeStart + "','" + timeEnd + "');";
	if (connectDatabase()) {
		//Executes a preparable statement
		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
		}
		disconnectDatabase();
	}
}

string getHistory(char* username) {
	string listHistory = "";
	string SQLQuery = "SELECT * FROM history WHERE player1 = '" + string(username) + "'OR player2 = '" + string(username) + "';";
	if (connectDatabase()) {
		char player1[USERNAME_SIZE] = { 0 };
		char player2[USERNAME_SIZE] = { 0 };
		char matchEndBy[12] = { 0 };
		char winner[USERNAME_SIZE] = { 0 };
		char timeStart[27] = { 0 };
		char timeEnd[27] = { 0 };
		if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery.c_str(), SQL_NTS))
		{
			showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
		}
		else
		{
			while (SQLFetch(SQLStatementHandle) == SQL_SUCCESS) {
				SQLGetData(SQLStatementHandle, 1, SQL_C_DEFAULT, &player1, sizeof(player1), NULL);
				SQLGetData(SQLStatementHandle, 2, SQL_C_DEFAULT, &player2, sizeof(player2), NULL);
				SQLGetData(SQLStatementHandle, 3, SQL_C_DEFAULT, &matchEndBy, sizeof(matchEndBy), NULL);
				SQLGetData(SQLStatementHandle, 4, SQL_C_DEFAULT, &winner, sizeof(winner), NULL);
				SQLGetData(SQLStatementHandle, 5, SQL_C_DEFAULT, &timeStart, sizeof(timeStart), NULL);
				SQLGetData(SQLStatementHandle, 6, SQL_C_DEFAULT, &timeEnd, sizeof(timeEnd), NULL);
				listHistory = listHistory + player1 + " " + player2 + " " + matchEndBy + " " + winner + " " + timeStart + " " + timeEnd + " "; //Each space delimiter " "
			}
		}
		disconnectDatabase();
	}
	return listHistory;
}

#endif