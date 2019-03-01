using System;
using System.IO;
using System.Windows;

namespace Kanaban
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnExit(object sender, ExitEventArgs e)
        {
            File.WriteAllText(DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".txt", DB.logtext);
        }
    }
}