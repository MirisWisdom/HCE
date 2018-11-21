using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using Atarashii.Modules.Profile;
using BalsamV.Annotations;

namespace BalsamV
{
    public class Main : INotifyPropertyChanged
    {
        private string _path;

        private Configuration _configuration;

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

        public void Initialise()
        {
            try
            {
                Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    "My Games", "Halo CE", "savegames", Atarashii.API.Profile.Detect(), "blam.sav");
            }
            catch (Exception)
            {
                Configuration = new Configuration();
            }
        }

        private void OnPathChanged()
        {
            Configuration = Atarashii.API.Profile.Parse(Path);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}