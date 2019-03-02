using System.Windows;

namespace Kanaban
{
    /// <summary>
    /// Interaction logic for Preferences.xaml
    /// </summary>
    public partial class Preferences : Window
    {
        public Preferences()
        {
            InitializeComponent();
        }

        private void Settings_OnUnloaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
