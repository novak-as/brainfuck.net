using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Antlr4.Runtime.Misc;

namespace Compiler.Generators
{
    public class VisitorOptimized: BrainfuckOptimizedBaseListener
    {
        private VisitorSettings _settings;
        private Stack<Label> _loops = new Stack<Label>();

        private ILGenerator _gen;

        public VisitorOptimized(ILGenerator generator, VisitorSettings settings)
        {
            _gen = generator;
            _settings = settings;
        }

        public override void EnterRead([NotNull] BrainfuckOptimizedParser.ReadContext context)
        {
            _gen.Emit(OpCodes.Ldloc_0);
            _gen.Emit(OpCodes.Ldloc_1);
            _gen.EmitCall(OpCodes.Call, typeof(Console).GetMethod("Read"), new Type[] { });
            _gen.Emit(OpCodes.Stelem_I4);
            
            base.EnterRead(context);
        }

        public override void EnterPrint([NotNull] BrainfuckOptimizedParser.PrintContext context)
        {

            _gen.Emit(OpCodes.Ldloc_0);
            _gen.Emit(OpCodes.Ldloc_1);
            _gen.Emit(OpCodes.Ldelem_I4);


            _gen.EmitCall(OpCodes.Call, typeof(Console).GetMethod("Write", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(char) }, null), null);
            
            base.EnterPrint(context);
        }

        public override void ExitSeq_inc([NotNull] BrainfuckOptimizedParser.Seq_incContext context)
        {
            _gen.Emit(OpCodes.Ldloc_0);
            _gen.Emit(OpCodes.Ldloc_1);
            _gen.Emit(OpCodes.Ldloc_0);
            _gen.Emit(OpCodes.Ldloc_1);
            _gen.Emit(OpCodes.Ldelem_I4);
            _gen.Emit(OpCodes.Ldc_I4_S, 1 + context.Stop.StartIndex - context.Start.StartIndex);
            _gen.Emit(OpCodes.Add);
            _gen.Emit(OpCodes.Stelem_I4);

            base.ExitSeq_inc(context);
        }

        public override void ExitSeq_dec([NotNull] BrainfuckOptimizedParser.Seq_decContext context)
        {            
            _gen.Emit(OpCodes.Ldloc_0);
            _gen.Emit(OpCodes.Ldloc_1);
            _gen.Emit(OpCodes.Ldloc_0);
            _gen.Emit(OpCodes.Ldloc_1);
            _gen.Emit(OpCodes.Ldelem_I4);
            _gen.Emit(OpCodes.Ldc_I4_S, 1 + context.Stop.StartIndex - context.Start.StartIndex);
            _gen.Emit(OpCodes.Sub);
            _gen.Emit(OpCodes.Stelem_I4);


            base.EnterSeq_dec(context);
        }

        public override void EnterNext([NotNull] BrainfuckOptimizedParser.NextContext context)
        {
            var jumtToStartLabel = _gen.DefineLabel();
            var endLabel = _gen.DefineLabel();

            if (_settings.IsCycled)
            {
                _gen.Emit(OpCodes.Ldloc_1);
                _gen.Emit(OpCodes.Ldc_I4, _settings.AvailableMemory - 1);
                _gen.Emit(OpCodes.Ceq);
                _gen.Emit(OpCodes.Brtrue_S, jumtToStartLabel);
            }

            _gen.Emit(OpCodes.Ldloc_1);
            _gen.Emit(OpCodes.Ldc_I4_1);
            _gen.Emit(OpCodes.Add);
            _gen.Emit(OpCodes.Stloc_1);

            if (_settings.IsCycled)
            {
                _gen.Emit(OpCodes.Br_S, endLabel);

                _gen.MarkLabel(jumtToStartLabel);
                _gen.Emit(OpCodes.Ldc_I4_0);
                _gen.Emit(OpCodes.Stloc_1);

                _gen.MarkLabel(endLabel);
            }

            base.EnterNext(context);
        }

        public override void EnterPrev([NotNull] BrainfuckOptimizedParser.PrevContext context)
        {
            var jumtToEndLabel = _gen.DefineLabel();
            var endLabel = _gen.DefineLabel();

            if (_settings.IsCycled)
            {
                _gen.Emit(OpCodes.Ldloc_1);
                _gen.Emit(OpCodes.Ldc_I4_0);
                _gen.Emit(OpCodes.Ceq);
                _gen.Emit(OpCodes.Brtrue_S, jumtToEndLabel);
            }

            _gen.Emit(OpCodes.Ldloc_1);
            _gen.Emit(OpCodes.Ldc_I4_1);
            _gen.Emit(OpCodes.Sub);
            _gen.Emit(OpCodes.Stloc_1);

            if (_settings.IsCycled)
            {
                _gen.Emit(OpCodes.Br_S, endLabel);

                _gen.MarkLabel(jumtToEndLabel);
                _gen.Emit(OpCodes.Ldc_I4, _settings.AvailableMemory - 1);
                _gen.Emit(OpCodes.Stloc_1);

                _gen.MarkLabel(endLabel);
            }

            base.EnterPrev(context);
        }

        public override void EnterSloop([NotNull] BrainfuckOptimizedParser.SloopContext context)
        {

            _gen.Emit(OpCodes.Ldloc_3);
            _gen.Emit(OpCodes.Ldc_I4_1);
            _gen.Emit(OpCodes.Add);
            _gen.Emit(OpCodes.Stloc_3);

            _gen.Emit(OpCodes.Ldloc_2);
            _gen.Emit(OpCodes.Ldloc_3);
            _gen.Emit(OpCodes.Ldloc_1);
            _gen.Emit(OpCodes.Stelem_I4);

            var newLoop = _gen.DefineLabel();
            _gen.MarkLabel(newLoop);
            _loops.Push(newLoop);

            base.EnterSloop(context);
        }

        public override void EnterEloop([NotNull] BrainfuckOptimizedParser.EloopContext context)
        {
            _gen.Emit(OpCodes.Ldloc_0);
            _gen.Emit(OpCodes.Ldloc_2);
            _gen.Emit(OpCodes.Ldloc_3);
            _gen.Emit(OpCodes.Ldelem_I4);
            _gen.Emit(OpCodes.Ldelem_I4);
            _gen.Emit(OpCodes.Brtrue, _loops.Pop());

            _gen.Emit(OpCodes.Ldloc_3);
            _gen.Emit(OpCodes.Ldc_I4_1);
            _gen.Emit(OpCodes.Sub);
            _gen.Emit(OpCodes.Stloc_3);

            base.EnterEloop(context);
        }

        public override void EnterReset_value([NotNull] BrainfuckOptimizedParser.Reset_valueContext context)
        {

            _gen.Emit(OpCodes.Ldloc_0);
            _gen.Emit(OpCodes.Ldloc_1);
            _gen.Emit(OpCodes.Ldc_I4_0);
            _gen.Emit(OpCodes.Stelem_I4);

            base.EnterReset_value(context);
        }
    }
}
