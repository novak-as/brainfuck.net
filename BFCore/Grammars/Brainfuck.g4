grammar Brainfuck;

NEXT: '>';
PREV: '<';
ADD: '+';
SUB: '-';
PRINT: '.';
READ: ',';
LOOP: '[';
ELOOP: ']';
WS:[\t\r\n]+ -> skip;

analyze: expr* EOF;

expr: (next|prev|add|sub|print|read|loop);

next: NEXT;
prev: PREV;
add: ADD;
sub: SUB;
print: PRINT;
read: READ;

loop: LOOP expr* ELOOP;
