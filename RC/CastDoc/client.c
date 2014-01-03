#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <errno.h>
#include <unistd.h>
#include <stdio.h>
#include <stdlib.h>
#include <netdb.h>
#include <string.h>

extern int errno;

int main(int argc, char * argv[])
{
	int port;
	int socketDescriptor;
	struct sockaddr_in serverStruct;
	char buffer[1024];
	char filename[128];
	int file;
	
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
	bzero(filename, 128);
	printf("[Client] Please type in conversion type and file path.\n");
	fflush(stdout);
	scanf("%s %s",buffer, filename);
	printf("[Client] File: %s\n",filename);
	fflush(stdout);
	
	if((file = open(filename, O_RDONLY)) == -1)
	{
		perror("[Client] Can't open file: %s\n", filename);
		return errno;
	}
	
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
	
	printf("[Client] Response: %s\n", buffer);
	
	
	
	close(socketDescriptor);
}
