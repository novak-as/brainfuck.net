using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace Compiler
{
    public class Visitor: BrainfuckBaseListener
    {
        public StringBuilder Result { get; set; }

        public Visitor()
        {
            Result = new StringBuilder();
        }

        public override void EnterExpr([NotNull] BrainfuckParser.ExprContext context)
        {
            base.EnterExpr(context);
        }

        public override void EnterRead([NotNull] BrainfuckParser.ReadContext context)
        {
            
            A("ldloc.0");
            A("ldloc.1");
            A("call int32 [mscorlib]System.Console::Read()");
            A("stelem.i4");
            
            base.EnterRead(context);
        }

        public override void EnterPrint([NotNull] BrainfuckParser.PrintContext context)
        {

            A("ldstr \"[\"");
            A("call void [mscorlib] System.Console::Write(string)");
            A("ldloc.1");
            A("call void [mscorlib] System.Console::Write(int32)");
            A("ldstr \"] \"");
            A("call void [mscorlib] System.Console::Write(string)");
            A("ldloc.0");
            A("ldloc.1");
            A("ldelem.i4");
            Result.AppendLine("call void [mscorlib] System.Console::WriteLine(int32)");

            /*
            A("ldloc.0");
            A("ldloc.1");
            A("ldelem.i4");
            Result.AppendLine("call void [mscorlib] System.Console::Write(int32)");
            */

            base.EnterPrint(context);
        }

        public override void EnterAdd([NotNull] BrainfuckParser.AddContext context)
        {
            A("ldloc.0");
            A("ldloc.1");
            A("ldloc.0");
            A("ldloc.1");
            A("ldelem.i4");
            A("ldc.i4.1");
            A("add");
            A("stelem.i4");
            base.EnterAdd(context);
        }

        public override void EnterNext([NotNull] BrainfuckParser.NextContext context)
        {
            A("ldloc.1");
            A("ldc.i4.1");
            A("add");
            A("stloc.1");
            base.EnterNext(context);
        }

        public override void EnterPrev([NotNull] BrainfuckParser.PrevContext context)
        {
            A("ldloc.1");
            A("ldc.i4.1");
            A("sub");
            A("stloc.1");
            base.EnterPrev(context);
        }

        public override void EnterSub([NotNull] BrainfuckParser.SubContext context)
        {
            A("ldloc.0");
            A("ldloc.1");
            A("ldloc.0");
            A("ldloc.1");
            A("ldelem.i4");
            A("ldc.i4.1");
            A("sub");
            A("stelem.i4");
            base.EnterSub(context);
        }

        public override void EnterLoop([NotNull] BrainfuckParser.LoopContext context)
        {
            base.EnterLoop(context);
        }

        public override void ExitLoop([NotNull] BrainfuckParser.LoopContext context)
        {
            base.ExitLoop(context);
        }

        public override void EnterEloop([NotNull] BrainfuckParser.EloopContext context)
        {
            base.EnterEloop(context);
        }

        public override void EnterSloop([NotNull] BrainfuckParser.SloopContext context)
        {
            base.EnterSloop(context);
        }

        private void A(String command)
        {
            Result.AppendLine(command);
        }
    }
}
