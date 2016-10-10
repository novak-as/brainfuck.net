# brainfuck.net

##Usage:

  `compiler.exe somebrainfuckcode.bf`

##Example:

#####Input:
   
* test.bf
   
   `,.[-.]`
   
#####Output:
  
* test.exe
* test.il
  
  ```
.assembly extern mscorlib {}
.assembly Test
{
.ver 1:0:1:0
}
.module test.exe
.method static void main() cil managed
{
.maxstack 8
.entrypoint
.locals init ([0] int32[] memory,
[1] int32 currentPosition,
[2] int32[] loopStack,
[3] int32 loopPosition)
ldc.i4.s 100
newarr [mscorlib]System.Int32
stloc.0
ldc.i4.0
stloc.1
ldc.i4.s 50
newarr [mscorlib]System.Int32
stloc.2
ldc.i4.0
stloc.3
ldloc.0
ldloc.1
call int32 [mscorlib]System.Console::Read()
stelem.i4
ldloc.0
ldloc.1
ldelem.i4
call void [mscorlib] System.Console::Write(int32)
ldloc.3
ldc.i4.1
add
stloc.3
ldloc.2
ldloc.3
ldloc.1
stelem.i4
LOOP_1:
ldloc.0
ldloc.1
ldloc.0
ldloc.1
ldelem.i4
ldc.i4.1
sub
stelem.i4
ldloc.0
ldloc.1
ldelem.i4
call void [mscorlib] System.Console::Write(int32)
ldloc.0
ldloc.2
ldloc.3
ldelem.i4
ldelem.i4
brtrue LOOP_1
ldloc.3
ldc.i4.1
sub
stloc.3

ret
}
  ```
   
