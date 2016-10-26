using Antlr4.Runtime;
using NDesk.Options;
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
        private static int _availableMemory = 100;
        private static int _maxDepthNested = 10;
        private static string _inputFile;
        private static bool _showHelp = false;

        static void Main(string[] args)
        {

            var optionSet = new OptionSet()
            {
                { "f|file=", "{file} with brainfuck sources", v=> { _inputFile = v; } },
                { "m|memory=", "available memory", v=> { _availableMemory = Int32.Parse(v); } },
                { "n|nested=", "max depth of nested loop", v=> { _maxDepthNested = Int32.Parse(v); } },
                { "h|help", "show this message", v=> _showHelp = v != null }
            };

            try
            {
                var compilerParams = optionSet.Parse(args);
            }
            catch (OptionException ex)
            {
                Console.WriteLine("Invalid params, try 'compiler --help' for more options");
                Environment.Exit(1);
            }

            if (_showHelp || String.IsNullOrEmpty(_inputFile))
            {
                ShowHelp(optionSet);
                Environment.Exit(0);
            }

            FileInfo inputFile = new FileInfo(_inputFile);
            FileInfo outputFile = new FileInfo(inputFile.FullName.Replace(inputFile.Extension, ".il"));

            var text = File.ReadAllText(inputFile.FullName);

            Console.WriteLine("Compiling...");

            AntlrInputStream inputStream = new AntlrInputStream(text);
            BrainfuckLexer lexer = new BrainfuckLexer(inputStream);
            CommonTokenStream tokenStream = new CommonTokenStream(lexer);
            BrainfuckParser parser = new BrainfuckParser(tokenStream);

            var bfVisitor = new VisitorOptimized();
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

            lines.AppendFormat("ldc.i4.s {0}\n", _availableMemory);
            lines.AppendLine("newarr [mscorlib]System.Int32");
            lines.AppendLine("stloc.0");

            lines.AppendLine("ldc.i4.0");
            lines.AppendLine("stloc.1");

            lines.AppendFormat("ldc.i4.s {0}\n", _maxDepthNested);
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

        private static void ShowHelp(OptionSet options)
        {
            Console.WriteLine("Usage: compiler -f filename [-m memory_size] [-n max_nested_depth]");
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }
    }
}
