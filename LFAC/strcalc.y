%{
#include <stdio.h>
#include <string.h>
%}
%union {
int intval;
char* strval;
}
%token <strval>STR <intval>NR EQ
%type <intval>nr
%type <strval>str
%left '+' '-'
%left '*' '`' '#' '?'
%nonassoc EQ '|'

%start s
%%
s	:	nr {printf("=> %d\n",$<intval>$);}
	|	str {printf("=> %s\n", $<strval>$);}
	;

nr	:	NR	{ $$ = $1;}
	|	str '?' str	{
						int x = 0,i,l = strlen($3);
						for (i = 0; i <= strlen($1)-l; i++)
							x += !(strncmp($1+i,$3,l));
						$$ = x;
					}
	|	str EQ str	{ $$ = !(strcmp((const char *)$1,(const char *)$3)); }
	|	'|' str '|' { $$ = strlen($2); }
	;
 
str	:	STR	{
           		char * s=malloc(sizeof(char)*strlen($1));
               	strcpy(s,$1);
               	$$ = s;
	     	}
	| '(' str ')'	{
           				char * s=malloc(sizeof(char)*strlen($2));
              		 	strcpy(s,$2);
              		 	$$ = s;
					}
	|	str '+' str {
						char * s = malloc(sizeof(char) * (strlen($1)+strlen($3)));
						strcpy(s,$1);
						strcat(s,$3);
						$$ = s;
					}
	|	str '-' str	{
						char * s = malloc(sizeof(char) * strlen($1));
						if (strstr((const char *)$1,(const char *)$3) == NULL)
							strcpy(s,$1);
						else {
								int i, l = strlen($3);
								for (i = 0; i < strlen($1);i++)
									if((strncmp($1+i,$3,l)))
										strncat(s,$1+i,1);
									else i+=l-1;
							}
						$$ = s;
					}
	|	str '*' nr {
						char * s = malloc(sizeof(char)*strlen($1)*$3);
						int i;
						for(i=0;i<$3;i++)
							strcat(s,$1);
						$$ = s;
				}
	| nr '`' str {
					char * s = malloc(sizeof(char)*$1);
					strncpy(s,$3,$1);
					$$ = s;						
				}
	| str '#' nr {
					char * s = malloc(sizeof(char)*$3);
					if ($3 > strlen($1))
						strcpy(s,$1);
					else strcpy(s,$1+strlen($1)-$3);
					$$ = s;					
				}
    ; 			 
%%
int main(){
 yyparse();
}    
