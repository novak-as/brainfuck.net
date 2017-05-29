using Antlr4.Runtime;
using NDesk.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
                Console.ReadKey();
                return;
            }

            FileInfo inputFile = new FileInfo(_inputFile);
            FileInfo outputFile = new FileInfo(inputFile.FullName.Replace(inputFile.Extension, ".exe"));

            var text = File.ReadAllText(inputFile.FullName);

            Console.WriteLine("Compiling...");

            AntlrInputStream inputStream = new AntlrInputStream(text);
            BrainfuckOptimizedLexer lexer = new BrainfuckOptimizedLexer(inputStream);
            CommonTokenStream tokenStream = new CommonTokenStream(lexer);
            BrainfuckOptimizedParser parser = new BrainfuckOptimizedParser(tokenStream);

            var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName("Test, Version=1.0.0.0"),
                AssemblyBuilderAccess.Save);

            var module = assembly.DefineDynamicModule("Test.Test", outputFile.Name);
            var dclass = module.DefineType("Program", TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.Public);

            var method = dclass.DefineMethod("main", MethodAttributes.Static, typeof(void), new Type[] { });
            method.SetCustomAttribute(new CustomAttributeBuilder(typeof(STAThreadAttribute).GetConstructor(new Type[] { }), new object[] { }));

            assembly.SetEntryPoint(method);

            //method.DefineParameter(1, ParameterAttributes.None, "args");

            
            var body = method.GetILGenerator();

            body.DeclareLocal(typeof(Int32[])); // [0] int32 memory
            body.DeclareLocal(typeof(Int32)); // [1] int32 currentPosition
            body.DeclareLocal(typeof(Int32[])); // [2] int32[] loopsStack
            body.DeclareLocal(typeof(Int32)); // [3] int32 loopPosition

            body.Emit(OpCodes.Ldc_I4_S, _availableMemory);
            body.Emit(OpCodes.Newarr, typeof(Int32));
            body.Emit(OpCodes.Stloc_0);

            body.Emit(OpCodes.Ldc_I4_0);
            body.Emit(OpCodes.Stloc_1);

            body.Emit(OpCodes.Ldc_I4_S, _maxDepthNested);
            body.Emit(OpCodes.Newarr, typeof(Int32));
            body.Emit(OpCodes.Stloc_2);

            body.Emit(OpCodes.Ldc_I4_0);
            body.Emit(OpCodes.Stloc_3);


            var bfVisitor = new VisitorOptimized(body);
            parser.AddParseListener(bfVisitor);

            parser.analyze();

            body.Emit(OpCodes.Ret); 

            dclass.CreateType();

            assembly.Save(outputFile.Name);

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
