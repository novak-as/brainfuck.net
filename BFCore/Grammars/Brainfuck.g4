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

expr: (next|prev|seq_inc|seq_dec|print|read|loop);

next: NEXT;
prev: PREV;
add: ADD;
sub: SUB;
print: PRINT;
read: READ;
sloop: LOOP;
eloop: ELOOP;

loop: sloop expr* eloop;

seq_inc: add+;
seq_dec: sub+;
