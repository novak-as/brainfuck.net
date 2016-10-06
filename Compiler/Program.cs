using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = ".++.>.++.-.";

            Console.WriteLine(input);

            AntlrInputStream inputStream = new AntlrInputStream(input);
            BrainfuckLexer lexer = new BrainfuckLexer(inputStream);
            CommonTokenStream tokenStream = new CommonTokenStream(lexer);
            BrainfuckParser parser = new BrainfuckParser(tokenStream);

            //            var tree = new ParseTreeWalker();

            //parser.AddParseListener(new Visitor());

            parser.analyze();

            Console.ReadKey();
        }
    }
}
