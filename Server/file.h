#pragma once
#include <time.h>
#include "constants.h"

/*
@function createTempFileName: Create a temporary file name

@param player1: Name of player 1
@param player2: Name of player 2

@return
*/
void createTempFileName(char* player1, char* player2, char* filename);

/*
@function countBytesLeftInFile: Count number of bytes left in file, starting from the current position

@param file: Pointer to current position in file

@return: Number of nytes left in file
*/
int countBytesLeftInFile(FILE* file);

void createTempFileName(char* player1, char* player2, char* filename) {
	time_t rawtime;
	struct tm * timeinfo;
	time(&rawtime);
	timeinfo = localtime(&rawtime);
	snprintf(filename, BUFF_SIZE, "temp_%s_%s_%d%02d%02d_%02d%02d%02d.txt",
		player1,
		player2,
		timeinfo->tm_year + 1900, timeinfo->tm_mon, timeinfo->tm_mday,
		timeinfo->tm_hour, timeinfo->tm_min, timeinfo->tm_sec);
}

int countBytesLeftInFile(FILE* file) {
	if (file == NULL) return 0;
	int bytesInContent, bytesRead;
	bytesRead = ftell(file);
	fseek(file, 0, SEEK_END); // move file cursor to end of file to get file len
	bytesInContent = ftell(file);
	fseek(file, bytesRead, SEEK_SET); // move file cursor back to the previous position
	return bytesInContent - bytesRead;
}