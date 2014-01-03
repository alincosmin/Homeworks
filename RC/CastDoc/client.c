#include <sys/types.h>
#include <sys/socket.h>
#include <sys/stat.h>
#include <netinet/in.h>
#include <errno.h>
#include <unistd.h>
#include <stdio.h>
#include <stdlib.h>
#include <netdb.h>
#include <string.h>
#include <fcntl.h>

extern int errno;

int main(int argc, char * argv[])
{
	int port;
	int socketDescriptor;
	struct sockaddr_in serverStruct;
	int bytes;
	unsigned long byteTotal;
	char message[128];
	char buffer[1024];
	char filename[128];
	int file;
	struct stat fileStat;
	
	if (argc != 3)
	{
		printf("[Client] Syntax: %s <server_address> <port_number>\n",argv[0]);
		return -1;
	}
	
	port = atoi(argv[2]);
	
	if((socketDescriptor = socket(AF_INET, SOCK_STREAM, 0)) == -1)
	{
		perror("[Client] socket() error!\n");
		return errno;
	}
	
	serverStruct.sin_family = AF_INET;
	serverStruct.sin_addr.s_addr = inet_addr(argv[1]);
	serverStruct.sin_port = htons(port);
	
	if(connect(socketDescriptor, (struct sockaddr *) &serverStruct, sizeof(struct sockaddr)) == -1)
	{
		perror("[Client] connect() error!\n");
		return errno;
	}
	
	bzero(buffer, 1024);
	bzero(message, 128);
	bzero(filename, 128);
	printf("[Client] Please type in conversion type and file path.\n");
	fflush(stdout);
	scanf("%s %s",message, filename);
	printf("[Client] File: %s\n",filename);
	fflush(stdout);
	
	if((file = open(filename, O_RDONLY)) == -1)
	{
		printf("\n[Client] Can't open file: %s\n", filename);
		return 1;
	}
	fstat(file, &fileStat);
	sprintf(buffer,"%s %lu",message,(unsigned long)fileStat.st_size);
	
	if(write(socketDescriptor, buffer, 1024) <= 0)
	{
		perror("[Client] write() through socket error!\n");
		return errno;
	}
	
	
	if(read(socketDescriptor, buffer, 1024) < 0)
	{
		perror("[Client] read() through socket error!\n");
		return errno;
	}
	
	if(strcmp("OK",buffer))
	{
		printf("[Client] Something went wrong!\n");
		return 2;
	}
	
	printf("[Client] Sending file... (%lu bytes)",(unsigned long)fileStat.st_size);
	bzero(buffer,1024);
	byteTotal = 0;
	do
	{
		if((bytes = read(file,buffer,1024)) < 1)
			break;
		if(bytes != write(socketDescriptor,buffer,bytes))
			break;
		byteTotal += bytes;
	} while(byteTotal < fileStat.st_size);
	
	if(byteTotal != fileStat.st_size)
		printf("\tError! %lu %lu\n",byteTotal,fileStat.st_size);
	else
		printf("\tComplete!\n");
	close(file);
	close(socketDescriptor);
}
