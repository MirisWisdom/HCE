using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using BalsamV.GUI.Properties;

namespace BalsamV.GUI
{
    /// <summary>
    ///     Main model for the HCE.BalsamV program.
    /// </summary>
    public sealed class Main : INotifyPropertyChanged
    {
        /// <summary>
        ///     Selected blam.sav exists.
        /// </summary>
        private bool _canEdit;

        /// <summary>
        ///     Atarashii Profile Configuration for the selected blam.sav.
        /// </summary>
        private Configuration _configuration;

        /// <summary>
        ///     Selected Blam.sav absolute path.
        /// </summary>
        private string _path;

        /// <summary>
        ///     <see cref="_path" />
        /// </summary>
        public string Path
        {
            get => _path;
            set
            {
                if (value == _path) return;
                _path = value;
                OnPropertyChanged();
                OnPathChanged();
            }
        }

        /// <summary>
        ///     <see cref="_configuration" />
        /// </summary>
        public Configuration Configuration
        {
            get => _configuration;
            set
            {
                if (Equals(value, _configuration)) return;
                _configuration = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     <see cref="_canEdit" />
        /// </summary>
        public bool CanEdit
        {
            get => _canEdit;
            set
            {
                if (value == _canEdit) return;
                _canEdit = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Attempts to auto-detect & load a blam.sav on the file system.
        /// </summary>
        public void Initialise()
        {
            try
            {
                var profile = LastprofFactory.Get(LastprofFactory.Type.Detect).Parse();

                Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    "My Games", "Halo CE", "savegames", profile, "blam.sav");
            }
            catch (Exception)
            {
                Configuration = new Configuration();
            }
        }

        public void Save()
        {
            using (var ms = new MemoryStream())
            using (var fs = File.Open(Path, FileMode.Open))
            {
                fs.CopyTo(ms);

                new ConfigurationPatcher(Configuration).PatchTo(ms);
                new ConfigurationForger().Forge(ms);

                ms.Position = 0;
                fs.Position = 0;

                ms.CopyTo(fs);
            }
        }

        /// <summary>
        ///     Invoke parsing of the selected blam.sav upon path change.
        /// </summary>
        private void OnPathChanged()
        {
            if (!File.Exists(Path)) return;

            using (var fs = File.Open(Path, FileMode.Open))
            {
                Configuration = ConfigurationFactory.GetFromStream(fs);
            }

            CanEdit = true;
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}