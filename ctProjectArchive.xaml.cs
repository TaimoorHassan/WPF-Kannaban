using Kanaban.Annotations;
using Kanaban.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Kanaban
{
    /// <summary>
    /// Interaction logic for ctProjectArchive.xaml
    /// </summary>
    public partial class ctProjectArchive : UserControl, INotifyPropertyChanged
    {

        public ctProjectArchive()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button b && b.DataContext is Project p)
            {
                if (File.Exists($@"Data\{p.Name}DB"))
                {
                    MessageBox.Show("Same Database Already Exists");
                    return;
                }

                /// Fazool
                AppData.ProjectArchive.Delete(p._id);
                AppData.Projects.Insert(p); //
                File.Move(p.DatabasePath, $@"Data\{p.Name}DB");
                p.DatabasePath = $@"Data\{p.Name}DB";
                ArchivedProjects = await AppData.ProjectArchive.All();

            }
        }

        private async void CtProjectArchive_OnLoaded(object sender, RoutedEventArgs e)
        {
            ArchivedProjects = await AppData.ProjectArchive.All();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<Project> ArchivedProjects { get; set; }
    }
}
