grammar BrainfuckOptimized;

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

expr: (next|prev|seq_inc|seq_dec|print|read|reset_value|loop);

next: NEXT;
prev: PREV;
add: ADD;
sub: SUB;
print: PRINT;
read: READ;
sloop: LOOP;
eloop: ELOOP;


reset_value: sloop sub eloop;
loop: sloop expr* eloop;

seq_inc: add+;
seq_dec: sub+;

