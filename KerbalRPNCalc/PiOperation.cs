using System;
using System.Collections.Generic;

namespace KerbalRPNCalc
{
    internal class PiOperation : IOperation
    {
        public Stack<double> Calculate(Stack<double> stack)
        {
            stack.Push(Math.PI);
            return stack;
        }
    }
}