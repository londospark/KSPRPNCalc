using System;
using System.Collections.Generic;

namespace KerbalRPNCalc
{
    internal class PowerOperation : IOperation
    {
        public Stack<double> Calculate(Stack<double> stack)
        {
            var x = stack.Pop();
            var y = stack.Pop();
            stack.Push(Math.Pow(y, x));
            return stack;
        }
    }
}