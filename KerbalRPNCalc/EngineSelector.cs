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
using PartsAPI.Engine;
using UnityEngine;

namespace KerbalRPNCalc
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    internal class EngineSelector : MonoBehaviour
    {
        private Action<double> _callback;
        private Rect _screenRect = new Rect(100, 100, 600, 350);
        private Vector2 _scrollPosition = new Vector2(0, 0);
        private readonly EngineModes _engineModes = new EngineModes();

        public EngineSelector()
        {
            if (PlayerPrefs.HasKey("KSPCalc.EngineSelector.X") && PlayerPrefs.HasKey("KSPCalc.EngineSelector.Y"))
            {
                _screenRect.x = PlayerPrefs.GetFloat("KSPCalc.EngineSelector.X");
                _screenRect.y = PlayerPrefs.GetFloat("KSPCalc.EngineSelector.Y");
            }
            Visible = false;
        }

        public bool Visible { get; private set; }

        public void OnGUI()
        {
            if (!Visible)
                return;

            _screenRect = GUILayout.Window(GetInstanceID(), _screenRect, id =>
            {
                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, HighLogic.Skin.scrollView);
                GUILayout.BeginVertical();

                foreach (var engineMode in _engineModes)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(engineMode.Engine.Name + " " + engineMode.Mode.Name, HighLogic.Skin.label,
                        GUILayout.ExpandWidth(true));
                    if (GUILayout.Button("Sea Level", HighLogic.Skin.button, GUILayout.Width(80.0f)))
                    {
                        _callback(engineMode.Mode.SeaLevelISP);
                        Hide();
                    }
                    if (GUILayout.Button("Space", HighLogic.Skin.button, GUILayout.Width(80.0f)))
                    {
                        _callback(engineMode.Mode.VacuumISP);
                        Hide();
                    }
                    GUILayout.EndHorizontal();
                }

                GUILayout.EndVertical();
                GUILayout.EndScrollView();
                GUI.DragWindow();
            
            }, "Engine Information Finderator", HighLogic.Skin.window);

            PlayerPrefs.SetFloat("KSPCalc.EngineSelector.X", _screenRect.x);
            PlayerPrefs.SetFloat("KSPCalc.EngineSelector.Y", _screenRect.y);
        }

        public void Show(Action<double> callback)
        {
            _callback = callback;
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
            PlayerPrefs.Save();
        }
    }
}