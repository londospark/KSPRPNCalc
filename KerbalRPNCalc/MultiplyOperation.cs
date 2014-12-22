using System.Collections.Generic;

namespace KerbalRPNCalc
{
    internal class MultiplyOperation : IOperation
    {
        public Stack<double> Calculate(Stack<double> stack)
        {
            var x = stack.Pop();
            var y = stack.Pop();
            stack.Push(y * x);
            return stack;
        }
    }
}