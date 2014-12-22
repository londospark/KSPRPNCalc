using System.Collections.Generic;

namespace KerbalRPNCalc
{
    internal class SwapOperation : IOperation
    {
        public Stack<double> Calculate(Stack<double> stack)
        {
            var x = stack.Pop();
            var y = stack.Pop();
            stack.Push(x);
            stack.Push(y);
            return stack;
        }
    }
}