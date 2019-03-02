using System;
using System.Globalization;
using System.Linq;
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
            var cp = DB.InitializeDatabase();
            SelectProject(cp);
        }

        internal void SelectProject(Project p)
        {
            if (p != null)
            {
                cmbProjects.Items.Clear();
                var allprojects = DB.Projects.FindAll().ToList();
                var index = allprojects.FindIndex(x => x._id == p._id);

                foreach (var project in allprojects)
                {
                    cmbProjects.Items.Add(project);
                }

                cmbProjects.SelectedIndex = index;
            }
            else
            {
                NullifyData();
            }
        }

        private void NullifyData()
        {
            DB.CurrentProject = null;
            cmbProjects.Items.Clear();
            cmbProjects.SelectedIndex = -1;
            DB.Projects.Foreach(x => { cmbProjects.Items.Add(x);});
            
            foreach (var boardColumn in MainGrid.Children.OfType<BoardColumn>())
            {
                boardColumn.lst.Items.Clear();
            }
            snackbar.MessageQueue.Enqueue("Project Unloaded");

        }


        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbProjects.SelectedItem is Project p)
            {
                LoadProjectData(p);
            }
            
        }

        private void btn_new_project_click(object sender, RoutedEventArgs e)
        {
            ProjectEdit pe = new ProjectEdit();
            pe.Added += SelectProject;
            host.DialogContent = pe;
            host.IsOpen = true;
        }

        internal void LoadProjectData(Project project)
        {
            if (project != null)
            {
                DB.CurrentProject = project;
                foreach (var boardColumn in MainGrid.Children.OfType<BoardColumn>())
                {
                    boardColumn.LoadData();
                }

                snackbar.MessageQueue.Enqueue("Project Loaded : " + project.Name);
            }
        }

        private void btn_delete_project_click(object sender, RoutedEventArgs e)
        {
            if (cmbProjects.SelectedItem is Project p)
            {
                DB.Projects.Delete(x => x._id == p._id);
            }
            NullifyData();
        }

        private void EditableTextBlock_OnTextEdited(string newtext)
        {
            MessageBox.Show(newtext);
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