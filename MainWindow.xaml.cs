using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using WindowState = System.Windows.WindowState;

namespace Kanaban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ListBox dragSource;

        internal static MainWindow Instance { get; set; }

        public event SimpleEvent DatabaseChanged;

        public MainWindow()
        {
            InitializeComponent();
            Properties.Settings.Default.Upgrade();
            Instance = this;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            DB.InitializeDatabase();
        }


        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btn_new_project_click(object sender, RoutedEventArgs e)
        {
            ProjectEdit pe = new ProjectEdit();
            pe.Added += () => { };
            host.DialogContent = pe;
            host.IsOpen = true;
        }
    }


    public class PriorityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var a = (Priority) value;
            var priority = a;
            string Color = null;
            if (priority == Priority.High)
            {
                Color = "#E74C3C";
            }
            else if (priority == Priority.Normal)
            {
                Color = "#3498DB";
            }
            else if (priority == Priority.Low)
            {
                Color = "#2ECC71";
            }
            else
            {
                Color = "#3498DB";
            }

            return Color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}