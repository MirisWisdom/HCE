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

﻿using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SPV3.Launcher.GUI.Factories
{
    public sealed class ProfilePathFactory
    {
        public static string GetProfile(string profileType)
        {
            switch (profileType)
            {
                case "last":
                    return GetLastProfileName();
                default:
                    throw new ArgumentException("Invalid profile type.");
            }
        }

        private static string GetLastProfileName()
        {
            var lastprofConfig = ConfigPathFactory.GetConfiguration(ConfigPathType.LastProf);
            var lastprofString = File.ReadAllText(lastprofConfig);

            var profileRegex = new Regex(@"savegames.*\\", RegexOptions.IgnoreCase);

            return profileRegex.Match(lastprofString).Value.Substring(10).TrimEnd('\\');
        }
    }
}