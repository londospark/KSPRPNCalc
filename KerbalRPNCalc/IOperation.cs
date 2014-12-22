using System.Collections.Generic;

namespace KerbalRPNCalc
{
    internal interface IOperation
    {
        Stack<double> Calculate(Stack<double> stack);
    }
}