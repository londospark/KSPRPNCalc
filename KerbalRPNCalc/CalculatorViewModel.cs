// This file is part of KerbalRPNCalc.
// 
// KerbalRPNCalc is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// KerbalRPNCalc is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with KerbalRPNCalc. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Globalization;
using KerbalRPNCalc.Operations;

namespace KerbalRPNCalc
{
    internal class CalculatorViewModel
    {
        private bool _enterBeforeNextDigit;
        private string _inputBuffer = "0";

        public CalculatorViewModel()
        {
            T = "0";
            Z = "0";
            Y = "0";
            X = "0";
        }

        public String T { get; set; }
        public String Z { get; set; }
        public String Y { get; set; }
        public String X { get; set; }

        public void Digit(char digit)
        {
            if (_enterBeforeNextDigit)
            {
                Enter();
                _enterBeforeNextDigit = false;
            }

            _inputBuffer = Double.Parse(_inputBuffer + digit).ToString(CultureInfo.CurrentCulture);
            InputBufferToX();
        }

        public void DecimalPoint()
        {
            if (_enterBeforeNextDigit)
            {
                Enter();
                _enterBeforeNextDigit = false;
            }

            if (_inputBuffer.Contains(".")) return;

            if (_inputBuffer == String.Empty)
                _inputBuffer = "0.";
            else
                _inputBuffer = _inputBuffer + ".";

            InputBufferToX();
        }

        public void Enter()
        {
            T = Z;
            Z = Y;
            Y = X;
            _inputBuffer = "0";
        }

        private void InputBufferToX()
        {
            X = _inputBuffer;
        }

        public Stack<double> ToStack()
        {
            var stack = new Stack<double>();
            stack.Push(Double.Parse(T));
            stack.Push(Double.Parse(Z));
            stack.Push(Double.Parse(Y));
            stack.Push(Double.Parse(X));
            return stack;
        }

        public void FromStack(Stack<double> stack)
        {
            X = Pop(stack);
            Y = Pop(stack);
            Z = Pop(stack);
            T = Pop(stack);
        }

        private static String Pop(Stack<double> stack)
        {
            return stack.Count > 0 ? stack.Pop().ToString(CultureInfo.CurrentCulture) : "0";
        }

        public void Operate(IOperation operation)
        {
            FromStack(operation.Calculate(ToStack()));
            _enterBeforeNextDigit = true;
        }
    }
}