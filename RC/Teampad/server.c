#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <fcntl.h>

char users[1000];

char * GenerateCode()
{
	char * code = malloc(sizeof(char)*8);
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
	for(i=1;i<8;i++)
		code[i] = symbols[rand()%36];
	return code;
}

int FindFile(char * name)
{
   return open(name,O_RDONLY);
}

void Check(int fd)
	{
		switch(users[fd])
		{
			case 0:
				printf("Nimeni.."); break;
			case 1:
				printf("Calaretu' singuratic..."); break;
			case 2:
				printf("Gasca-i toata!"); break;
			default:
				printf("Ceva nu-i bun...");
		}
	}


int main()
{
	memset(users,0,1000);
	printf("Hello from server!\n");
	
	Check(FindFile("/etc/passwd"));
	Check(7);
	Check(FindFile("sdasdas"));
	
	return 0;
}
