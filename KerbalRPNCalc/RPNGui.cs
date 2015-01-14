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
using KerbalRPNCalc.Operations;
using UnityEngine;

namespace KerbalRPNCalc
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class RPNGui : MonoBehaviour
    {
        private ApplicationLauncherButton _button;
        private Rect _screenRect = new Rect(0, 0, 200, 0);
        private bool _isVisible;
        private readonly CalculatorViewModel _calculatorViewModel = new CalculatorViewModel();
        private EngineSelector _engineSelector;

        public void Awake()
        {
            GameEvents.onGUIApplicationLauncherReady.Add(AddButton);
            _engineSelector = gameObject.AddComponent<EngineSelector>();
        }

        public void OnDestroy()
        {
            GameEvents.onGUIApplicationLauncherReady.Remove(AddButton);

            if (_button != null)
                ApplicationLauncher.Instance.RemoveModApplication(_button);
        }

        private void AddButton()
        {
            if (_button == null)
            {
                _button = ApplicationLauncher.Instance.AddModApplication(() => _isVisible = true,
                    () => _isVisible = false, null, null, null,
                    null, ApplicationLauncher.AppScenes.ALWAYS,
                    GameDatabase.Instance.GetTexture("KerbalRPNCalc/Textures/Icon", false));
            }
        }

        public void OnGUI()
        {
            if (!_isVisible) return;

            _screenRect = GUILayout.Window(GetInstanceID(), _screenRect, id =>
            {
                GUILayout.BeginVertical();
                _calculatorViewModel.T = CalculatorDisplay("T:", _calculatorViewModel.T);
                _calculatorViewModel.Z = CalculatorDisplay("Z:", _calculatorViewModel.Z);
                _calculatorViewModel.Y = CalculatorDisplay("Y:", _calculatorViewModel.Y);
                _calculatorViewModel.X = CalculatorDisplay("X:", _calculatorViewModel.X);
                GUILayout.EndVertical();
                BuildEngineInfoButton();
                BuildOperations();
                BuildNumPad();
                GUI.DragWindow();
            }, "Kerbal RPN Calculator", HighLogic.Skin.window);
        }

        private static string CalculatorDisplay(string label, string value)
        {
            var calculatorScreenStyle = HighLogic.Skin.textField;
            calculatorScreenStyle.alignment = TextAnchor.MiddleRight;

            GUILayout.BeginHorizontal();
            GUILayout.Label(label, HighLogic.Skin.label);
            var textFieldValue = GUILayout.TextField(value, calculatorScreenStyle);
            GUILayout.EndHorizontal();

            return textFieldValue;
        }

        private void BuildEngineInfoButton()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Engine Information", HighLogic.Skin.button))
            {
                _engineSelector.Show(value => _calculatorViewModel.Operate(new Value(value)));
            }
            GUILayout.EndHorizontal();
        }

        private void BuildOperations()
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            OperationButton("ln", new Ln());
            OperationButton("e^x", new Exp());
            OperationButton("PI", new Value(Math.PI));
            OperationButton("y^x", new Power());
            OperationButton("SWAP", new Swap());
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            OperationButton("+", new Add());
            OperationButton("-", new Subtract());
            OperationButton("*", new Multiply());
            OperationButton("/", new Divide());
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private void OperationButton(string text, IOperation operation)
        {
            if (GUILayout.Button(text, HighLogic.Skin.button))
            {
                _calculatorViewModel.Operate(operation);
            }
        }

        private void BuildNumPad()
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            DigitButton("7");
            DigitButton("4");
            DigitButton("1");
            DigitButton("0");
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            DigitButton("8");
            DigitButton("5");
            DigitButton("2");
            DecimalButton();
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            DigitButton("9");
            DigitButton("6");
            DigitButton("3");
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            EnterButton();
        }

        private void DecimalButton()
        {
            if (GUILayout.Button(".", HighLogic.Skin.button))
            {
                _calculatorViewModel.DecimalPoint();
            }
        }

        private void DigitButton(string digit)
        {
            if (GUILayout.Button(digit, HighLogic.Skin.button))
            {
                _calculatorViewModel.Digit(digit[0]);
            }
        }

        private void EnterButton()
        {
            if (GUILayout.Button("ENTER", HighLogic.Skin.button))
            {
                _calculatorViewModel.Enter();
            }
        }
    }
}