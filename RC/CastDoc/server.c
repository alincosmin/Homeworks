#include <sys/types.h>
#include <sys/socket.h>
#include <sys/time.h>
#include <netinet/in.h>
#include <unistd.h>
#include <errno.h>
#include <stdio.h>
#include <arpa/inet.h>
#include <string.h>
#include <fcntl.h>
#include <stdlib.h>

#define PORT 13012
#define CLIENTS 20

extern int errno;

int main()
{
	struct sockaddr_in serverStruct;
	struct sockaddr_in fromStruct;
	fd_set readDescriptors;
	fd_set activeDescriptors;
	struct timeval timeValue;
	int serverSocket, clientSocket;
	int optionValue = 1;
	int descriptor;
	int maxDescriptors;
	int sockaddrLength;
	
	if((serverSocket = socket(AF_INET, SOCK_STREAM, 0)) == -1)
	{
		perror("[Server] socket() error!\n");
		return errno;
	}
	
	setsockopt(serverSocket, SOL_SOCKET, SO_REUSEADDR, &optionValue, sizeof(optionValue));
	
	bzero(&serverStruct, sizeof(serverStruct));
	
	serverStruct.sin_family = AF_INET;
	serverStruct.sin_addr.s_addr = htonl(INADDR_ANY);
	serverStruct.sin_port = htons(PORT);
	
	if(bind(serverSocket, (struct sockaddr *) &serverStruct, sizeof(struct sockaddr)) == -1)
	{
		perror("[Server] bind() error!\n");
		return errno;
	}
	
	if(listen(serverSocket, CLIENTS) == -1)
	{
		perror("[Server] listen() error!\n");
		return errno;
	}
	
	FD_ZERO(&activeDescriptors);
	FD_SET(serverSocket, &activeDescriptors);
	
	timeValue.tv_sec = 1;
	timeValue.tv_usec = 0;
	
	maxDescriptors = serverSocket;
	
	printf("[Server] Listening on port %d...\n", PORT);
	fflush(stdout);
	
	while(1)
	{
		bcopy((char *) &activeDescriptors, (char *) &readDescriptors, sizeof(readDescriptors));
		
		if(select(maxDescriptors + 1, &readDescriptors, NULL, NULL, &timeValue) < 0)
		{
			perror("[Server] select() error!\n");
			return errno;
		}
		
		if(FD_ISSET(serverSocket, &readDescriptors))
		{
			sockaddrLength = sizeof(fromStruct);
			bzero(&fromStruct, sizeof(fromStruct));
			
			clientSocket = accept(serverSocket, (struct sockaddr *) &fromStruct, &sockaddrLength);
			
			if(clientSocket < 0)
			{
				perror("[Server] accept() error!\n");
				return errno;
			}
			
			if(maxDescriptors < clientSocket)
				maxDescriptors = clientSocket;
				
			FD_SET(clientSocket, &activeDescriptors);
			
			printf("[Server] Client-%d connected!\n", clientSocket);
			fflush(stdout);
		}
		
		for(descriptor = 0; descriptor <= maxDescriptors; descriptor++)
		{
			if(descriptor != serverSocket && FD_ISSET(descriptor, &readDescriptors))
			{
				if(DoJob(descriptor))
				{
					printf("[Server] Client-%d disconnected!\n", descriptor);
					fflush(stdout);
					close(descriptor);
					FD_CLR(descriptor, &activeDescriptors);
				}
			}
		}
	}
}

int ConvertDocument(char * type, char * filename)
{
	char newName[10];
	strcpy(newName,"numenou");
	strcpy(filename,newName);
	return 1;
}

int DoJob(int descriptor)
{
	char buffer[1024];
	unsigned long fileSize;
	int bytes;
	char conversionType[128];
	char fileName[10];
	int file;
	
	if(read(descriptor, buffer, sizeof(buffer)) < 0)
	{
		perror ("[Server] read() through socket error!\n");
		return 0;
	}
	
	sscanf(buffer,"%s %lu",conversionType,&fileSize);
	
	printf("Conversion: %s\nSize: %lu\n",conversionType,fileSize);

	strcpy(buffer,"OK");
	
	if(write(descriptor, buffer, sizeof(buffer)) <= 0)
	{
		perror ("[Server] write() through socket error!\n");
		return 0;
	}
	
	if((file = open("test123",O_WRONLY|O_CREAT|O_TRUNC,S_IWUSR|S_IRUSR)) == -1)
	{
		printf("[Server] Can't create file!\n");
		return 1;
	}
	
	bzero(buffer,1024);
	do
	{
		if((bytes = read(descriptor,buffer,1024)) < 0)
			break;
		if(bytes != write(file,buffer,bytes))
			break;
		fileSize -= bytes;
	} while(fileSize > 0);
	close(file);
	
	if(fileSize > 0)
		printf("[Server] File transfer from Client-%d failed!\n",descriptor);
	else 
		printf("[Server] File transfer from Client-%d is complete!\n", descriptor);
	
	return 1;
}

