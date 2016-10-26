using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace Compiler
{
    public class VisitorOptimized: BrainfuckOptimizedBaseListener
    {
        private Stack<int> _loops = new Stack<int>();
        private int _loopId = 0;

        public StringBuilder Result { get; set; }

        public VisitorOptimized()
        {
            Result = new StringBuilder();
        }

        public override void EnterRead([NotNull] BrainfuckOptimizedParser.ReadContext context)
        {
            
            A("ldloc.0");
            A("ldloc.1");
            A("call int32 [mscorlib]System.Console::Read()");
            A("stelem.i4");
            
            base.EnterRead(context);
        }

        public override void EnterPrint([NotNull] BrainfuckOptimizedParser.PrintContext context)
        {
            //system output for debug mode
            
            /*
            A("ldstr \"[\"");
            A("call void [mscorlib] System.Console::Write(string)");
            A("ldloc.1");
            A("call void [mscorlib] System.Console::Write(int32)");
            A("ldstr \"] \"");
            A("call void [mscorlib] System.Console::Write(string)");
            A("ldloc.0");
            A("ldloc.1");
            A("ldelem.i4");
            A("call void [mscorlib] System.Console::WriteLine(int32)");
            */

            //original output

            
            
            A("ldloc.0");
            A("ldloc.1");
            A("ldelem.i4");
            Result.AppendLine("call void [mscorlib] System.Console::Write(char)");            

            base.EnterPrint(context);
        }

        public override void ExitSeq_inc([NotNull] BrainfuckOptimizedParser.Seq_incContext context)
        {
            A("ldloc.0");
            A("ldloc.1");
            A("ldloc.0");
            A("ldloc.1");
            A("ldelem.i4");
            A(string.Format("ldc.i4.s {0}",1+context.Stop.StartIndex-context.Start.StartIndex));
            A("add");
            A("stelem.i4");
            base.ExitSeq_inc(context);
        }

        public override void ExitSeq_dec([NotNull] BrainfuckOptimizedParser.Seq_decContext context)
        {
            A("ldloc.0");
            A("ldloc.1");
            A("ldloc.0");
            A("ldloc.1");
            A("ldelem.i4");
            A(string.Format("ldc.i4.s {0}", 1+context.Stop.StartIndex - context.Start.StartIndex));
            A("sub");
            A("stelem.i4");

            base.EnterSeq_dec(context);
        }

        /*
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
        */

        public override void EnterNext([NotNull] BrainfuckOptimizedParser.NextContext context)
        {
            A("ldloc.1");
            A("ldc.i4.1");
            A("add");
            A("stloc.1");
            base.EnterNext(context);
        }

        public override void EnterPrev([NotNull] BrainfuckOptimizedParser.PrevContext context)
        {
            A("ldloc.1");
            A("ldc.i4.1");
            A("sub");
            A("stloc.1");
            base.EnterPrev(context);
        }

        /*
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
        }*/

        public override void EnterSloop([NotNull] BrainfuckOptimizedParser.SloopContext context)
        {

            A("ldloc.3");
            A("ldc.i4.1");
            A("add");
            A("stloc.3");

            A("ldloc.2");
            A("ldloc.3");
            A("ldloc.1");
            A("stelem.i4");

            A(string.Format("LOOP_{0}:", ++_loopId));
            _loops.Push(_loopId);

            base.EnterSloop(context);
        }

        public override void EnterEloop([NotNull] BrainfuckOptimizedParser.EloopContext context)
        {
            A("ldloc.0");
            A("ldloc.2");
            A("ldloc.3");
            A("ldelem.i4");
            A("ldelem.i4");
            A(string.Format("brtrue LOOP_{0}",_loops.Pop()));

            A("ldloc.3");
            A("ldc.i4.1");
            A("sub");
            A("stloc.3");

            base.EnterEloop(context);
        }

        public override void EnterReset_value([NotNull] BrainfuckOptimizedParser.Reset_valueContext context)
        {
            A("ldloc.0");
            A("ldloc.1");
            A("ldc.i4.0");
            A("stelem.i4");

            base.EnterReset_value(context);
        }

        private void A(String command)
        {
            Result.AppendLine(command);
        }
    }
}
