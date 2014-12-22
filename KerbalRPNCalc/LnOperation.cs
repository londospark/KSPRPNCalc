using System;
using System.Collections.Generic;

namespace KerbalRPNCalc
{
    internal class LnOperation : IOperation
    {
        public Stack<double> Calculate(Stack<double> stack)
        {
            var operand = stack.Pop();
            stack.Push(Math.Log(operand));
            return stack;
        }
    }
}