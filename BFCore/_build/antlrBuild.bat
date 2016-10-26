cd ..

java -jar _external/antlr-4.5.3-complete.jar -Dlanguage=CSharp -o antlr/Brainfuck grammars/Brainfuck.g4
java -jar _external/antlr-4.5.3-complete.jar -Dlanguage=CSharp -o antlr/BrainfuckOptimized grammars/BrainfuckOptimized.g4

cd _build
