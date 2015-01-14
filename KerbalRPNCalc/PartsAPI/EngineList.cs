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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KerbalRPNCalc.PartsAPI
{
    internal class EngineList : IEnumerable<Engine>
    {
        private readonly List<Engine> _engines;

        public EngineList()
        {
            _engines = Resources.FindObjectsOfTypeAll<ModuleEngines>()
                .Select(EngineFactory.Normalise)
                .Union(Resources.FindObjectsOfTypeAll<ModuleEnginesFX>()
                    .Select(EngineFactory.Normalise))
                .Where(x => !Resources.FindObjectsOfTypeAll<MultiModeEngine>()
                    .Select(EngineFactory.Normalise).Select(y => y.Name).Contains(x.Name))
                .Union(Resources.FindObjectsOfTypeAll<MultiModeEngine>()
                    .Select(EngineFactory.Normalise)).ToList();
        }

        public IEnumerator<Engine> GetEnumerator()
        {
            return _engines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}