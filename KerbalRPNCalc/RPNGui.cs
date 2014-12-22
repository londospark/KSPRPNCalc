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

        public void Awake()
        {
            GameEvents.onGUIApplicationLauncherReady.Add(AddButton);
        }

        public void OnDestroy()
        {
            GameEvents.onGUIApplicationLauncherReady.Remove(AddButton);

            if (_button != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(_button);
            }
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
                var calculatorScreenStyle = HighLogic.Skin.textField;
                calculatorScreenStyle.alignment = TextAnchor.MiddleRight;
                _calculatorViewModel.T = GUILayout.TextField(_calculatorViewModel.T, calculatorScreenStyle);
                _calculatorViewModel.Z = GUILayout.TextField(_calculatorViewModel.Z, calculatorScreenStyle);
                _calculatorViewModel.Y = GUILayout.TextField(_calculatorViewModel.Y, calculatorScreenStyle);
                _calculatorViewModel.X = GUILayout.TextField(_calculatorViewModel.X, calculatorScreenStyle);
                GUILayout.EndVertical();

                BuildOperations();

                BuildNumPad();

                GUI.DragWindow();
            }, "Kerbal RPN Calculator", HighLogic.Skin.window);
        }

        private void BuildOperations()
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            OperationButton("ln", new LnOperation());
            OperationButton("PI", new PiOperation());
            OperationButton("y^x", new PowerOperation());
            OperationButton("SWAP", new SwapOperation());
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            OperationButton("+", new AddOperation());
            OperationButton("-", new SubtractOperation());
            OperationButton("*", new MultiplyOperation());
            OperationButton("/", new DivideOperation());
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