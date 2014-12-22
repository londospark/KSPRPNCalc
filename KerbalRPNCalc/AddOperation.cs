using System.Collections.Generic;
using System.Linq;

namespace KerbalRPNCalc
{
    internal class AddOperation : IOperation
    {
        public Stack<double> Calculate(Stack<double> stack)
        {
            var operands = new[] {stack.Pop(), stack.Pop()};
            stack.Push(operands.Sum());
            return stack;
        }
    }
}