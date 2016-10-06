using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace BFCore
{
    public class Visitor: BrainfuckBaseListener
    {

        int[] memory = new int[30000];
        int current = 0;

        public Visitor()
        {

        }

        public override void EnterRead([NotNull] BrainfuckParser.ReadContext context)
        {
            memory[current] = Console.Read();
            base.EnterRead(context);
        }

        public override void EnterAdd([NotNull] BrainfuckParser.AddContext context)
        {
            memory[current] += 1;
            base.EnterAdd(context);
        }

        public override void EnterNext([NotNull] BrainfuckParser.NextContext context)
        {
            current += 1;
            base.EnterNext(context);
        }

        public override void EnterPrev([NotNull] BrainfuckParser.PrevContext context)
        {
            current -= 1;
            base.EnterPrev(context);
        }

        public override void EnterSub([NotNull] BrainfuckParser.SubContext context)
        {
            memory[current] -= 1;
            base.EnterSub(context);
        }

        public override void EnterPrint([NotNull] BrainfuckParser.PrintContext context)
        {
            Console.Write(memory[current]);
            base.EnterPrint(context);
        }

        public override void EnterEloop([NotNull] BrainfuckParser.EloopContext context)
        {
            base.EnterEloop(context);
        }

        public override void EnterSloop([NotNull] BrainfuckParser.SloopContext context)
        {
            base.EnterSloop(context);
        }
    }
}
