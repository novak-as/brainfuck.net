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
using Compiler.Generators;

namespace Compiler
{
    class Program
    {
        private static bool _isCycled = false;
        private static int _availableMemory = 100;
        private static int _maxDepthNested = 10;
        private static string _inputFile;
        private static string _assemblyName = "Program";
        private static uint _version = 1;
        private static bool _showHelp = false;

        static void Main(string[] args)
        {

            var optionSet = new OptionSet()
            {
                { "f|file=", "{file} with brainfuck sources", v=> { _inputFile = v; } },
                { "assembly_name=", "assembly name", v=> { _assemblyName = v; } },
                { "v|version=","version", v=>{ uint.TryParse(v, out _version); } },
                { "m|memory=", "available memory", v=> { _availableMemory = int.Parse(v); } },
                { "n|nested=", "max depth of nested loop", v=> { _maxDepthNested = int.Parse(v); } },
                { "c|cycled", "is cycled", v=> _isCycled = v !=null },
                { "h|help", "show this message", v=> _showHelp = v != null }
            };

            try
            {
                var compilerParams = optionSet.Parse(args);
            }
            catch (OptionException ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Invalid params, try 'compiler --help' for more options");
                Environment.Exit(1);
            }

            if (_showHelp || string.IsNullOrEmpty(_inputFile))
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
                new AssemblyName(string.Format("{0}, Version={1}",_assemblyName, _version)),
                AssemblyBuilderAccess.Save);

            var module = assembly.DefineDynamicModule(_assemblyName, outputFile.Name);
            var dclass = module.DefineType("Program", TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.Public);

            var method = dclass.DefineMethod("main", MethodAttributes.Static, typeof(void), new Type[] { });
            method.SetCustomAttribute(new CustomAttributeBuilder(typeof(STAThreadAttribute).GetConstructor(new Type[] { }), new object[] { }));

            assembly.SetEntryPoint(method);

            
            var body = method.GetILGenerator();

            body.DeclareLocal(typeof(int[])); // [0] int32 memory
            body.DeclareLocal(typeof(int)); // [1] int32 currentPosition
            body.DeclareLocal(typeof(int[])); // [2] int32[] loopsStack
            body.DeclareLocal(typeof(int)); // [3] int32 loopPosition

            body.Emit(OpCodes.Ldc_I4_S, _availableMemory);
            body.Emit(OpCodes.Newarr, typeof(int));
            body.Emit(OpCodes.Stloc_0);

            body.Emit(OpCodes.Ldc_I4_0);
            body.Emit(OpCodes.Stloc_1);

            body.Emit(OpCodes.Ldc_I4_S, _maxDepthNested);
            body.Emit(OpCodes.Newarr, typeof(int));
            body.Emit(OpCodes.Stloc_2);

            body.Emit(OpCodes.Ldc_I4_0);
            body.Emit(OpCodes.Stloc_3);


            var bfVisitor = new VisitorOptimized(body, new VisitorSettings(_availableMemory, _isCycled));
            parser.AddParseListener(bfVisitor);

            parser.analyze();

            body.Emit(OpCodes.Ret); 

            dclass.CreateType();

            assembly.Save(outputFile.Name);

            Console.WriteLine("Done");
        }

        private static void ShowHelp(OptionSet options)
        {
            Console.WriteLine("Usage: compiler -f filename [options]");
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }
    }
}
