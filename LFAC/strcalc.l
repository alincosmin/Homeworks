%{
#include <stdio.h>
#include "y.tab.h"
%}
%%
"==" {return EQ;}
[0-9]+ {yylval.intval=atoi(yytext); return NR;}
[a-z]+ {yylval.strval=yytext; return STR;}
[ \t] ;
\n {return 0;}
. {return yytext[0];}
