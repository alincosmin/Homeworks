#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <fcntl.h>

typedef struct {
	char name[5];
	char clients;
} Document;

Document docs[1000];

char * GenerateCode()
{
	char * code = malloc(sizeof(char)*5);
	char symbols[36];
	int i = 0;
	char c = '0';
	while (c<='9')
		symbols[i++] = c++;
	c = 'a';
	while (c<='z')
		symbols[i++] = c++;
	srand(time(NULL));
	code[0] = symbols[rand()%26+10];
	for(i=1;i<5;i++)
		code[i] = symbols[rand()%36];
	return code;
}

void DownloadFile(int client, char * filename)
{
	printf("DownloadFile - neimplementat!\n"); 
}

char * AvailableName()
{
	char * filename = GenerateCode();
	int fd = open(filename,O_RDONLY);
	while (fd > 2)
		{
			close(fd);
			filename = GenerateCode();
			fd = open(filename,O_RDONLY);
		}
	return filename;
}

int CreateFile()
{
	char * name = AvailableName();
	int rfd = open(name,O_CREAT,O_RDWR);
	strcpy(docs[rfd].name,name);
	docs[rfd].clients = 1;
	return rfd;
}

int EditFile(char * filename)
{
	int i;
	for(i=2;i<1000;i++)
		if(!strcmp(filename,docs[i].name))
		{
			if(docs[i].clients < 2)
				docs[i].clients++;
			else return -1;
			return i;
		}
	i = open(filename,O_RDWR);
	if (i>0)
		return i;
	else return -2;
}

int main()
{
	memset(docs,0,1000);
	char * x = AvailableName();
	printf("Hello from server!\n");
	printf("Editing %s: %d\n",x,EditFile(x));
	return 0;
}
