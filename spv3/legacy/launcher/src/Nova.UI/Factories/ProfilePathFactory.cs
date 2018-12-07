using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Nova.UI.Factories
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