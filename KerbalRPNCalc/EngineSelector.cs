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
using System.Linq;
using UnityEngine;

namespace KerbalRPNCalc
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    internal class EngineSelector : MonoBehaviour
    {
        private bool _visible = false;
        private Rect _screenRect = new Rect(0, 0, 400, 500);
        private readonly EngineList _engines = new EngineList();
        private Action<double> _callback;
        private Vector2 _scrollPosition = new Vector2(0, 0);

        public void OnGUI()
        {
            if (!_visible)
                return;

            _screenRect = GUILayout.Window(GetInstanceID(), _screenRect, id =>
            {
                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, HighLogic.Skin.scrollView);
                GUILayout.BeginVertical();

                foreach(var engine in _engines.OrderBy(x => x.Name))
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(engine.Name + " (Modes: " + engine.Modes.Count + ")", HighLogic.Skin.label, GUILayout.ExpandWidth(true));
                    if (GUILayout.Button("Sea Level", HighLogic.Skin.button, GUILayout.Width(80.0f)))
                    {
                        _callback(engine.Modes.First().SeaLevelISP);
                        Hide();
                    }
                    if (GUILayout.Button("Space", HighLogic.Skin.button, GUILayout.Width(80.0f)))
                    {
                        _callback(engine.Modes.First().VacuumISP);
                        Hide();
                    }
                    GUILayout.EndHorizontal();
                }
                    
                GUILayout.EndVertical();
                GUILayout.EndScrollView();
                GUI.DragWindow();
            }, "Engine Information Finderator", HighLogic.Skin.window);
        }

        public void Show(Action<double> callback)
        {
            _callback = callback;
            _visible = true;
        }

        public void Hide()
        {
            _visible = false;
        }
    }
}