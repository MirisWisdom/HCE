/**
 * Copyright (C) 2019 Emilian Roman
 * 
 * This file is part of SPV3.Launcher.
 * 
 * SPV3.Launcher is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * SPV3.Launcher is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with SPV3.Launcher.  If not, see <http://www.gnu.org/licenses/>.
 */

using SPV3.Loader;

namespace SPV3.Launcher.GUI
{
    /// <summary>
    ///     Main model used by the SPV3.2 Launcher.
    /// </summary>
    public class Main
    {
        /// <summary>
        ///     Invokes the SPV3.Loader's HCE executable loading routine.
        /// </summary>
        public void Load()
        {
            new SPV3.Loader.Loader(new LoaderConfiguration()).Start(ExecutableFactory.Detect());
        }
    }
}