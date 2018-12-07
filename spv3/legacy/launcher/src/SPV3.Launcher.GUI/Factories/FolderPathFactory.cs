using System;
using System.IO;

namespace SPV3.Launcher.GUI.Factories
{
    public sealed class FolderPathFactory
    {
        public static string GetFolder(FolderPathType folderType)
        {
            var myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var myGames = Path.Combine(myDocuments, "My Games");
            var haloFolder = Path.Combine(myGames, "Halo CE");

            switch (folderType)
            {
                case FolderPathType.Halo:
                    return Path.Combine(haloFolder);
                case FolderPathType.OpenSauce:
                    return Path.Combine(haloFolder, "OpenSauce");
                case FolderPathType.Profiles:
                    return Path.Combine(haloFolder, "savegames");
                default:
                    throw new ArgumentException("Invalid folder name.");
            }
        }
    }
}