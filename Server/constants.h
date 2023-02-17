#pragma once
/* START DEFINE CONSTANTS */
/*
Define sever variables
*/
#define SERVER_ADDR "127.0.0.1"
#define SERVER_PORT 5500
#define MAX_CLIENT 1024
#define BUFF_SIZE 10240

#define NO_CLIENT -1
#define NO_ROOM -1

#define BOARD_HEIGHT 16
#define BOARD_WIDTH 16
#define BOARD_WIN_SCORE 5

#define TYPE_O 1
#define TYPE_X 2

/*
Define message variables
*/
#define OPCODE_SIZE 1
#define LENGTH_SIZE 2
#define USERNAME_SIZE 20
#define PASSWORD_SIZE 50
#define PLAYER_MOVE_SIZE 1

/*
Define match variables
*/
#define MATCH_CONTINUE 0
#define MATCH_END_BY_WIN 1
#define MATCH_END_BY_DRAW 2
#define MATCH_END_BY_SURRENDER 3

/* END DEFINE CONSTANTS */

/* START DEFINE OPCODE */

/*
Opcode for sending and receiving file
*/
#define OPCODE_FILE 0
#define OPCODE_FILE_DATA 1
#define OPCODE_FILE_ERROR 2
#define OPCODE_FILE_NO_FILE 3

/*
Opcode for sending and receiving sign up request and reply
*/
#define OPCODE_SIGN_UP 10
#define OPCODE_SIGN_UP_SUCESS 11
#define OPCODE_SIGN_UP_DUPLICATED_USERNAME 12
#define OPCODE_SIGN_UP_DIFFERENT_REPASSWORD 13
#define OPCODE_SIGN_UP_INVALID_USERNAME 14
#define OPCODE_SIGN_UP_INVALID_PASSWORD 15
#define OPCODE_SIGN_UP_UNKNOWN_ERROR 19

/*
Opcode for sending and receiving sign in request and reply
*/
#define OPCODE_SIGN_IN 20
#define OPCODE_SIGN_IN_SUCESS 21
#define OPCODE_SIGN_IN_ALREADY_LOGGED_IN 22
#define OPCODE_SIGN_IN_USERNAME_NOT_FOUND 23
#define OPCODE_SIGN_IN_INVALID_USERNAME 24
#define OPCODE_SIGN_IN_INVALID_PASSWORD 25
#define OPCODE_SIGN_IN_WRONG_PASSWORD 26
#define OPCODE_SIGN_IN_UNKNOWN_ERROR 29

/*
Opcode for sending and receiving sign out request and reply
*/
#define OPCODE_SIGN_OUT 30
#define OPCODE_SIGN_OUT_SUCCESS 31
#define OPCODE_SIGN_OUT_NOT_LOGGED_IN 32

/*
Opcode for sending and receiving querying player's info request and reply
*/
#define OPCODE_INFO 40
#define OPCODE_INFO_REPLY 41

/*
Opcode for sending and receiving list of online player request and reply
*/
#define OPCODE_LIST 50
#define OPCODE_LIST_REPLY 51

/*
Opcode for sending and receiving challenge player request and reply
*/
#define OPCODE_CHALLENGE 60
#define OPCODE_CHALLENGE_ACCEPT 61
#define OPCODE_CHALLENGE_REFUSE 62
#define OPCODE_CHALLENGE_INVALID_RANK 63
#define OPCODE_CHALLENGE_BUSY 64
#define OPCODE_CHALLENGE_NOT_FOUND 65

/*
Opcode for sending and receiving challenge player request and reply to server
*/
#define OPCODE_CHALLENGE_WITH_SERVER 70
#define OPCODE_CHALLENGE_WITH_SERVER_PLAY 71
#define OPCODE_CHALLENGE_WITH_SERVER_OVERLOAD 72

/*
Opcode for receiving request and sending reply of players' move in a match
*/
#define OPCODE_PLAY 80
#define OPCODE_PLAY_OPPONENT 81
#define OPCODE_PLAY_INVALID_CORDINATE 82
#define OPCODE_PLAY_INVALID_TURN 83

/*
Opcode for receiving request and sending reply of players' move in a match to server
*/
#define OPCODE_PLAY_WITH_SERVER 90
#define OPCODE_PLAY_REPLY_SERVER 91
#define OPCODE_PLAY_INVALID_CORDINATE_SERVER 92
#define OPCODE_PLAY_INVALID_TURN_SERVER 93

/*
Opcode for sending and receiving game result request and reply
*/
#define OPCODE_RESULT 100
#define OPCODE_SURRENDER 101
#define OPCODE_SURRENDER_WITH_SERVER 102
#define OPCODE_TIMER_DRAW 103
#define OPCODE_TIMER_DRAW_WITH_SERVER 104

/*
Opcode for sending and receiving querying history match request and reply
*/
#define OPCODE_HISTORY 110
#define OPCODE_HISTORY_REPLY 111

/*
Opcode for sending and receiving query change password request and reply
*/
#define OPCODE_CHANGE_PASSWORD 120
#define OPCODE_CHANGE_PASSWORD_SUCCESS 121
#define OPCODE_CHANGE_PASSWORD_WRONG 122
#define OPCODE_CHANGE_DIFFERENT_NEWPASSWORD 123
#define OPCODE_CHANGE_PASSWORD_INVALID 124
#define OPCODE_CHANGE_PASSWORD_OLDNEW 125
/* END OPCODE */

