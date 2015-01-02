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
using UnityEngine;

namespace KerbalRPNCalc
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    internal class EngineSelector : MonoBehaviour
    {
        private bool _visible = false;
        private Rect _screenRect = new Rect(0, 0, 300, 0);
        private readonly EngineList _engines = new EngineList();
        private Action<double> _callback;

        public void OnGUI()
        {
            if (!_visible)
                return;

            _screenRect = GUILayout.Window(GetInstanceID(), _screenRect, id =>
            {
                GUILayout.BeginVertical();

                foreach(var engine in _engines)
                {
                    if (GUILayout.Button(engine.Name + ": " + engine.ISP, HighLogic.Skin.button))
                    {
                        _callback(engine.ISP);
                        Hide();
                    }
                }
                    
                GUILayout.EndVertical();
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