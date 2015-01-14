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

namespace PartsAPI.Engine
{
    public class EngineModes : IEnumerable<EngineMode>
    {
        private readonly List<EngineMode> _engineModes;

        public EngineModes() :
            this(new EngineList())
        {
        }

        internal EngineModes(IEnumerable<Engine> engines)
        {
            _engineModes = engines.OrderBy(x => x.Name)
                .SelectMany(engine => engine.Modes, (engine, mode) => new EngineMode(engine, mode)).ToList();
        }

        public IEnumerator<EngineMode> GetEnumerator()
        {
            return _engineModes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}