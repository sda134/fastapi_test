#include <stdio.h>		// FILE, printf
#include <stdlib.h>		// exit
#include <time.h>		// tm
#include <sys/time.h>		// timeval
#include <unistd.h>		// open, close, write, read, usleep
#include <fcntl.h>		// O_RDWR
#include <signal.h>		//

char isWorking =1;
int fd =0;

void sigIntHandler(){
     isWorking=0;
     printf("SIGINT interruption!\n");
}


int main(int argc, char *argv[]){
    
	FILE *fp;
	char fname[] = "test.csv";
	signal(SIGINT, sigIntHandler);

	if((fp = fopen( fname, "w" )) == NULL ) {
		printf( "ファイルがオープンできませんでした\n" ); exit(1);
	}
		    
    while(isWorking){	
	struct timeval tv;
	struct tm *tm_st;
	gettimeofday(&tv, NULL); 
	tm_st = localtime(&tv.tv_sec);
				
	fprintf( fp, "%02d:%02d:%02d.%06d\n",
		tm_st->tm_hour,          	// 時
		tm_st->tm_min,           	// 分
		tm_st->tm_sec,			// 秒
		tv.tv_usec			// マイクロ秒
		);
		
		
		usleep(1 * 1000);		// 1mSec	
	}
	fclose(fp);
}
