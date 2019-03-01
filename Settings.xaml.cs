using System.Windows;

namespace Kanaban
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_OnUnloaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            
        }
    }
}
