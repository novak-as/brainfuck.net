using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: compiler.exe filename");
                Environment.Exit(1);
            }


            FileInfo inputFile = new FileInfo(args[0]);
            FileInfo outputFile = new FileInfo(inputFile.FullName.Replace(inputFile.Extension, ".il"));

            var text = File.ReadAllText(inputFile.FullName);

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
            lines.AppendLine("[1] int32 currentPosition,");
            lines.AppendLine("[2] int32[] loopStack,");
            lines.AppendLine("[3] int32 loopPosition)");

            lines.AppendLine("ldc.i4.s 100");
            lines.AppendLine("newarr [mscorlib]System.Int32");
            lines.AppendLine("stloc.0");

            lines.AppendLine("ldc.i4.0");
            lines.AppendLine("stloc.1");

            lines.AppendLine("ldc.i4.s 50");
            lines.AppendLine("newarr [mscorlib]System.Int32");
            lines.AppendLine("stloc.2");

            lines.AppendLine("ldc.i4.0");
            lines.AppendLine("stloc.3");

            parser.analyze();

            lines.AppendLine(bfVisitor.Result.ToString());

            lines.AppendLine("ret");
            lines.Append("}");

            File.WriteAllText(outputFile.FullName, lines.ToString());

            ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo();
            proc.FileName = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetAssembly(typeof(string)).CodeBase).AbsolutePath), "ilasm.exe");
            proc.Arguments = outputFile.FullName;
            Process.Start(proc);

            Console.WriteLine("Done");
        }
    }
}
