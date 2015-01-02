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

namespace KerbalRPNCalc
{
    internal class EngineFactory
    {
        public static Engine Normalise(ModuleEngines engine)
        {
            return new Engine(Part.FromGO(engine.gameObject).partInfo.title, engine.atmosphereCurve.Evaluate(1.0f));
        }

        public static Engine Normalise(ModuleEnginesFX engine)
        {
            return new Engine(Part.FromGO(engine.gameObject).partInfo.title, engine.atmosphereCurve.Evaluate(1.0f));
        }
    }
}