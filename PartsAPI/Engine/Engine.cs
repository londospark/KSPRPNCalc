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

namespace PartsAPI.Engine
{
    public class Engine
    {
        public string Name { get; private set; }
        public List<Mode> Modes { get; private set; }

        private Engine(string name, List<Mode> modes)
        {
            Name = name;
            Modes = modes;
        }

        internal static Builder CreateEngineWithName(string name)
        {
            return new Builder(name);
        }

        internal class Builder
        {
            private readonly string _engineName;
            private readonly List<Mode> _modes = new List<Mode>(); 

            public Builder(string engineName)
            {
                _engineName = engineName;
            }

            public Builder WithMode(Action<Mode> modeMaker)
            {
                var mode = new Mode();
                modeMaker(mode);
                _modes.Add(mode);
                return this;
            }

            public static implicit operator Engine(Builder builder)
            {
                return new Engine(builder._engineName, builder._modes);
            }
        }

        public class Mode
        {
            public string Name { get; internal set; }
            public float SeaLevelISP { get; internal set; }
            public float VacuumISP { get; internal set; }
        }
    }
}