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

namespace SPV3.Launcher.GUI.Factories
{
    public class ConfigPathFactory
    {
        public static string GetConfiguration(ConfigPathType configurationType)
        {
            switch (configurationType)
            {
                case ConfigPathType.OpenSauce:
                    return Path.Combine(FolderPathFactory.GetFolder(FolderPathType.OpenSauce),
                        ConfigPathResources.OpenSauceSettings);
                case ConfigPathType.Halo:
                    return Path.Combine(FolderPathFactory.GetFolder(FolderPathType.Halo),
                        ConfigPathResources.HaloSettings);
                case ConfigPathType.Mapping:
                    return Path.Combine(FolderPathFactory.GetFolder(FolderPathType.Halo),
                        ConfigPathResources.MappingSettings);
                case ConfigPathType.Chimera:
                    return Path.Combine(FolderPathFactory.GetFolder(FolderPathType.Halo),
                        ConfigPathResources.ChimeraSettings);
                case ConfigPathType.LastProf:
                    return Path.Combine(FolderPathFactory.GetFolder(FolderPathType.Halo),
                        ConfigPathResources.LastProfileText);
                case ConfigPathType.Initc:
                    return Path.Combine(ConfigPathResources.InitcSettings);
                default:
                    throw new ArgumentException(ConfigPathResources.InvalidNameException);
            }
        }
    }
}