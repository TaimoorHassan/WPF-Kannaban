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
            File.WriteAllText(DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".txt", DB.logtext);
        }
    }
}