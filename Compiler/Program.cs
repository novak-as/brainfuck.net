using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = args[0];

            var text = File.ReadAllText(filename);

            Console.WriteLine("Compiling...");

            AntlrInputStream inputStream = new AntlrInputStream(text);
            BrainfuckLexer lexer = new BrainfuckLexer(inputStream);
            CommonTokenStream tokenStream = new CommonTokenStream(lexer);
            BrainfuckParser parser = new BrainfuckParser(tokenStream);

            var bfVisitor = new Visitor();
            parser.AddParseListener(bfVisitor);

            var lines = new StringBuilder();
            lines.AppendLine(".assembly extern mscorlib {}");
            lines.AppendLine(".assembly Test");
            lines.AppendLine("{");
            lines.AppendLine(".ver 1:0:1:0");
            lines.AppendLine("}");
            lines.AppendLine(".module test.exe");
            lines.AppendLine(".method static void main() cil managed");
            lines.AppendLine("{");
            lines.AppendLine(".maxstack 8");
            lines.AppendLine(".entrypoint");
            lines.AppendLine(".locals init ([0] int32[] memory,");
            lines.AppendLine("[1] int32 currentPosition)");
            
            lines.AppendLine("ldc.i4.s 100");
            lines.AppendLine("newarr [mscorlib]System.Int32");
            lines.AppendLine("stloc.0");
            lines.AppendLine("ldc.i4.0");
            lines.AppendLine("stloc.1");

            parser.analyze();

            lines.AppendLine(bfVisitor.Result.ToString());

            lines.AppendLine("ret");
            lines.Append("}");

            File.WriteAllText("test.il", lines.ToString());

            ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo();
            proc.FileName = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\ilasm.exe";
            proc.Arguments = @"D:\Projects\Brainfuck.net\Compiler\bin\Debug\test.il";
            Process.Start(proc);

            Console.WriteLine("Done");
        }
    }
}
