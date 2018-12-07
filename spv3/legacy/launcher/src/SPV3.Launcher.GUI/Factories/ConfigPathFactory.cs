using System;
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