using System;
using System.Collections.Generic;

namespace KerbalRPNCalc.Operations
{
    internal class Exp : IOperation
    {
        public Stack<double> Calculate(Stack<double> stack)
        {
            var x = stack.Pop();
            stack.Push(Math.Exp(x));
            return stack;
        }
    }
}