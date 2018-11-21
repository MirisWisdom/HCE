using System;
using System.Linq;
using System.Windows;
using Atarashii.Modules.Profile.Options;
using Microsoft.Win32;

namespace BalsamV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Main _main;

        public MainWindow()
        {
            InitializeComponent();
            InitialiseComboBoxes();

            _main = (Main) DataContext;
            _main.Initialise();
        }

        private void InitialiseComboBoxes()
        {
            ColourComboBox.ItemsSource = Enum.GetValues(typeof(Colour.Type)).Cast<Colour.Type>();
            ConnectionComboBox.ItemsSource = Enum.GetValues(typeof(Connection.Type)).Cast<Connection.Type>();
            FrameRateComboBox.ItemsSource = Enum.GetValues(typeof(FrameRate.Type)).Cast<FrameRate.Type>();
            TextureQualityComboBox.ItemsSource = Enum.GetValues(typeof(Quality.Type)).Cast<Quality.Type>();
            ParticlesComboBox.ItemsSource = Enum.GetValues(typeof(Particles.Type)).Cast<Particles.Type>();
            AudioQualityComboBox.ItemsSource = Enum.GetValues(typeof(Quality.Type)).Cast<Quality.Type>();
            VarietyComboBox.ItemsSource = Enum.GetValues(typeof(Quality.Type)).Cast<Quality.Type>();
        }

        private void Load(object sender, RoutedEventArgs routedEventArgs)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Profile Binary|blam.sav"
            };
            
            if (dialog.ShowDialog() == true)
                _main.Path = dialog.FileName;
        }
    }
}