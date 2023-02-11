#pragma once
#include "constants.h"
#pragma comment (lib,"WS2_32.lib")
#pragma warning (disable:6385)

class RoomWithServer {
private:
	SOCKET playerSocket;
	std::vector<PlayerMove> movesList;
    int board[BOARD_HEIGHT][BOARD_WIDTH];
	std::string startTime;
public:
	RoomWithServer(SOCKET playerSocket);
	bool isPlayerTurn(SOCKET socket);
	bool isBoardCellEmpty(int x, int y);
	bool isMoveInBoard(int x, int y);
	bool isMatchEndByDraw();
	bool isMatchEndByWin();
	bool isPlayerInRoom(SOCKET socket);
	int addPlayerMove(PlayerMove aMove);
	int getMatchResult();
	int getPlayerMoveType(SOCKET socket);
	std::vector<PlayerMove> getMovesList();
	void setStartTime(std::string start);
	std::string getStartTime();
    void caculateChess(int *coordinateServer);
    long serverChess(int x, int y);
    long clientChess(int x, int y); 
};

/*
@function: RoomWithServer: Create room object.

@param playerSocket: the player socket.
*/
RoomWithServer::RoomWithServer(SOCKET playerSocket) {
	this->playerSocket = playerSocket;
	this->movesList.clear();
	for (int i = 0; i < BOARD_HEIGHT; i++) {
		for (int j = 0; j < BOARD_WIDTH; j++) {
			this->board[i][j] = 0;
		}
	}
}
/*
@function getMovesList: Get the moves 2 players made.

@return Return a list of moves of the match.
*/
std::vector<PlayerMove> RoomWithServer::getMovesList() {
	return this->movesList;
}
/*
@function isPlayerInRoom: Check if the connection currently in a room.

@param socket: The socket needed to be checked.

@return true if socket is in a room.
false if not

*/
bool RoomWithServer::isPlayerInRoom(SOCKET socket) {
	if (this->playerSocket == socket)
		return true;
	return false;
}
/*
@function isPlayerTurn: Check if it is a scoket's turn.

@param socket: The socket needed to be checked.

@return true if it is socket's turn.
false if not
*/
bool RoomWithServer::isPlayerTurn(SOCKET socket) {
	int nextMoveType;
	if (this->movesList.size() == 0) {
		nextMoveType = TYPE_O;
	}
	else {
		nextMoveType = this->movesList.back().type == TYPE_O ? TYPE_X : TYPE_O;
	}
	if (socket == this->playerSocket) {
		return nextMoveType == TYPE_O;
	}
	else {
		return nextMoveType == TYPE_X;
	}
}
/*
@function isMoveInBoard: Check if the move coordinates is inside the board.

@param x: The x-axis coordinate
@param y: The y-axis coordinate

@return true if it is indside.
false if not
*/
bool RoomWithServer::isMoveInBoard(int x, int y) {
	return x < BOARD_WIDTH&& x >= 0 && y < BOARD_HEIGHT&& y >= 0;
}
/*
@function isBoardCellEmpty: check if the cell at move coordinates is empty.

@param x: The x-axis coordinate
@param y: The y-axis coordinate

@return true if it is indside.
false if not
*/
bool RoomWithServer::isBoardCellEmpty(int x, int y) {
	return this->board[y][x] == 0;
}
/*
@function isMatchEndByWin: Check if current tur player win

@return true if it is indside.
false if not
*/
bool RoomWithServer::isMatchEndByWin() {
	int dx[] = { 0, 1, 1, 1 };
	int dy[] = { 1, 0, 1, -1 };
	int startX, startY, currentX, currentY;
	PlayerMove curMove = this->movesList.back();
	int type = curMove.type;
	int x = curMove.x;
	int y = curMove.y;
	//Print current board
	for (int i = 0; i < BOARD_HEIGHT; i++) {
		for (int j = 0; j < BOARD_WIDTH; j++) {
			std::cout << this->board[i][j] << " ";
		}
		std::cout << std::endl;
	}

	// Check row, column, diagonal and back diagonal for winning line
	int score;
	for (int lineType = 0; lineType < 4; lineType++) {
		score = 0;
		startX = x - (BOARD_WIN_SCORE - 1) * dx[lineType];
		startY = y - (BOARD_WIN_SCORE - 1) * dy[lineType];
		for (int i = 0; i < 2 * BOARD_WIN_SCORE - 1; i++) {
			currentX = startX + i * dx[lineType];
			currentY = startY + i * dy[lineType];
			if (!RoomWithServer::isMoveInBoard(currentX, currentY)) continue;
			if (this->board[currentY][currentX] != type) score = 0;
			else {
				score++;
				if (score == BOARD_WIN_SCORE) return true;
			}
		}
	}
	return false;
}
/*
@function isMatchEndByDraw: Check if a match is draw (when there is no empty cells left)

@return true if it is indside.
false if not
*/
bool RoomWithServer::isMatchEndByDraw() {
	return this->movesList.size() >= BOARD_WIDTH * BOARD_HEIGHT;
}
/*
@function getMatchResult: Return type of match result based on current moves list

@return	MATCH_END_BY_WIN if a player win
MATCH_END_BY_DRAW if match is a draw
MATCH_CONTINUE if match shoud be continued

*/
int RoomWithServer::getMatchResult() {
	if (this->isMatchEndByWin()) return MATCH_END_BY_WIN;
	if (this->isMatchEndByDraw()) return MATCH_END_BY_DRAW;
	return MATCH_CONTINUE;
}
/*
@function addPlayerMove: Add a player's move to the match current moves list

@param aMove: Player's move

@return OPCODE_PLAY_INVALID_CORDINATE if the move is invalid
OPCODE_PLAY if the move is added

*/
int RoomWithServer::addPlayerMove(PlayerMove aMove) {
	if (!RoomWithServer::isMoveInBoard(aMove.x, aMove.y) || !RoomWithServer::isBoardCellEmpty(aMove.x, aMove.y)) {
		return OPCODE_PLAY_INVALID_CORDINATE_SERVER;
	}
	this->board[aMove.y][aMove.x] = aMove.type;
	this->movesList.push_back(aMove);
	return OPCODE_PLAY_WITH_SERVER;
}
/*
@function getPlayerMoveType: Get a player's move type based on their socket

@param socket: Player's socket

@return	TYPE_O if player move type O
TYPE_X if player move type X

*/
int RoomWithServer::getPlayerMoveType(SOCKET socket) {
	if (socket == this->playerSocket) {
		return TYPE_O;
	}
	else {
		return TYPE_X;
	}
}

/*
@function setStartTime: Set match start time

@param start: The string represent start time
*/
void RoomWithServer::setStartTime(std::string start) {
	this->startTime = start;
}

/*
@function getStartTime: Get match start time
*/
std::string RoomWithServer::getStartTime() {
	return this->startTime;
}

/* Region AI */
int Attack[] = { 0, 9, 54, 162, 1458, 13112, 118008 };
int Defense[] = { 0, 3, 27, 99, 729, 6561, 59049 };

void RoomWithServer::caculateChess(int* coordinateServer) {
    long max = 0;
    int imax = 1, jmax = 1;
    for (int i = 0; i < BOARD_HEIGHT; i++) {
        for (int j = 0; j < BOARD_WIDTH; j++) {
            if (this->board[i][j] == 0) {
                long temp = serverChess(i, j) + clientChess(i, j);
                if (temp > max) {
                    max = temp;
                    imax = i;
                    jmax = j;
                }
            }
        }  
    }
    coordinateServer[0] = jmax;
    coordinateServer[1] = imax;
}

long RoomWithServer::serverChess(int x, int y) {
    int i = x - 1, j = y;
    int column = 0, row = 0, mdiagonal = 0, ediagonal = 0;
    int sc_ = 0, sc = 0, sr_ = 0, sr = 0, sm_ = 0, sm = 0, se_ = 0, se = 0;
    while (this->board[i][j] == 2 && i >= 0) {
        column++;
        i--;
    }
    if (this->board[i][j] == 0) sc_ = 1;
    i = x + 1;
    while (this->board[i][j] == 2 && i <= BOARD_HEIGHT) {
        column++;
        i++;
    }
    if (this->board[i][j] == 0) sc = 1;
    i = x; j = y - 1;
    while (this->board[i][j] == 2 && j >= 0) {
        row++;
        j--;
    }
    if (this->board[i][j] == 0) sr_ = 1;
    j = y + 1;
    while (this->board[i][j] == 2 && j <= BOARD_WIDTH) {
        row++;
        j++;
    }
    if (this->board[i][j] == 0) sr = 1;
    i = x - 1; j = y - 1;
    while (this->board[i][j] == 2 && i >= 0 && j >= 0) {
        mdiagonal++;
        i--;
        j--;
    }
    if (this->board[i][j] == 0) sm_ = 1;
    i = x + 1; j = y + 1;
    while (this->board[i][j] == 2 && i <= BOARD_HEIGHT && j <= BOARD_WIDTH) {
        mdiagonal++;
        i++;
        j++;
    }
    if (this->board[i][j] == 0) sm = 1;
    i = x - 1; j = y + 1;
    while (this->board[i][j] == 2 && i >= 0 && j <= BOARD_WIDTH) {
        ediagonal++;
        i--;
        j++;
    }
    if (this->board[i][j] == 0) se_ = 1;
    i = x + 1; j = y - 1;
    while (this->board[i][j] == 2 && i <= BOARD_HEIGHT && j >= 0) {
        ediagonal++;
        i++;
        j--;
    }
    if (this->board[i][j] == 0) se = 1;
    if (column == 4) column = BOARD_WIN_SCORE;
    if (row == 4) row = BOARD_WIN_SCORE;
    if (mdiagonal == 4) mdiagonal = BOARD_WIN_SCORE;
    if (ediagonal == 4) ediagonal = BOARD_WIN_SCORE;
    if (column == 3 && sc == 1 && sc_ == 1) column = 4;
    if (row == 3 && sr == 1 && sr_ == 1) row = 4;
    if (mdiagonal == 3 && sm == 1 && sm_ == 1) mdiagonal = 4;
    if (ediagonal == 3 && se == 1 && se_ == 1) ediagonal = 4;
    if (column == 2 && row == 2 && sc == 1 && sc_ == 1 && sr == 1 && sr_ == 1) column = 3;
    if (column == 2 && mdiagonal == 2 && sc == 1 && sc_ == 1 && sm == 1 && sm_ == 1) column = 3;
    if (column == 2 && ediagonal == 2 && sc == 1 && sc_ == 1 && se == 1 && se_ == 1) column = 3;
    if (row == 2 && mdiagonal == 2 && sm == 1 && sm_ == 1 && sr == 1 && sr_ == 1) column = 3;
    if (row == 2 && ediagonal == 2 && se == 1 && se_ == 1 && sr == 1 && sr_ == 1) column = 3;
    if (ediagonal == 2 && mdiagonal == 2 && sm == 1 && sm_ == 1 && se == 1 && se_ == 1) column = 3;
    long Sum = Attack[row] + Attack[column] + Attack[mdiagonal] + Attack[ediagonal];
    return Sum;
}

long RoomWithServer::clientChess(int x, int y) {
    int i = x - 1, j = y;
    int sc_ = 0, sc = 0, sr_ = 0, sr = 0, sm_ = 0, sm = 0, se_ = 0, se = 0;
    int column = 0, row = 0, mdiagonal = 0, ediagonal = 0;
    while (this->board[i][j] == 1 && i >= 0) {
        column++;
        i--;
    }
    if (this->board[i][j] == 0) sc_ = 1;
    i = x + 1;
    while (this->board[i][j] == 1 && i <= BOARD_HEIGHT) {
        column++;
        i++;
    }
    if (this->board[i][j] == 0) sc = 1;
    i = x; j = y - 1;
    while (this->board[i][j] == 1 && j >= 0) {
        row++;
        j--;
    }
    if (this->board[i][j] == 0) sr_ = 1;
    j = y + 1;
    while (this->board[i][j] == 1 && j <= BOARD_WIDTH) {
        row++;
        j++;
    }
    if (this->board[i][j] == 0) sr = 1;
    i = x - 1; j = y - 1;
    while (this->board[i][j] == 1 && i >= 0 && j >= 0) {
        mdiagonal++;
        i--;
        j--;
    }
    if (this->board[i][j] == 0) sm_ = 1;
    i = x + 1; j = y + 1;
    while (this->board[i][j] == 1 && i <= BOARD_HEIGHT && j <= BOARD_WIDTH) {
        mdiagonal++;
        i++;
        j++;
    }
    if (this->board[i][j] == 0) sm = 1;
    i = x - 1; j = y + 1;
    while (this->board[i][j] == 1 && i >= 0 && j <= BOARD_WIDTH) {
        ediagonal++;
        i--;
        j++;
    }
    if (this->board[i][j] == 0) se_ = 1;
    i = x + 1; j = y - 1;
    while (this->board[i][j] == 1 && i <= BOARD_HEIGHT && j >= 0) {
        ediagonal++;
        i++;
        j--;
    }
    if (this->board[i][j] == 0) se = 1;
    if (column == 4) column = BOARD_WIN_SCORE;
    if (row == 4) row = BOARD_WIN_SCORE;
    if (mdiagonal == 4) mdiagonal = BOARD_WIN_SCORE;
    if (ediagonal == 4) ediagonal = BOARD_WIN_SCORE;
    if (column == 3 && sc == 1 && sc_ == 1) column = 4;
    if (row == 3 && sr == 1 && sr_ == 1) row = 4;
    if (mdiagonal == 3 && sm == 1 && sm_ == 1) mdiagonal = 4;
    if (ediagonal == 3 && se == 1 && se_ == 1) ediagonal = 4;
    if (column == 2 && row == 2 && sc == 1 && sc_ == 1 && sr == 1 && sr_ == 1) column = 3;
    if (column == 2 && mdiagonal == 2 && sc == 1 && sc_ == 1 && sm == 1 && sm_ == 1) column = 3;
    if (column == 2 && ediagonal == 2 && sc == 1 && sc_ == 1 && se == 1 && se_ == 1) column = 3;
    if (row == 2 && mdiagonal == 2 && sm == 1 && sm_ == 1 && sr == 1 && sr_ == 1) column = 3;
    if (row == 2 && ediagonal == 2 && se == 1 && se_ == 1 && sr == 1 && sr_ == 1) column = 3;
    if (ediagonal == 2 && mdiagonal == 2 && sm == 1 && sm_ == 1 && se == 1 && se_ == 1) column = 3;
    long Sum = Defense[row] + Defense[column] + Defense[mdiagonal] + Defense[ediagonal];
    return Sum;
}
/* End region AI */