using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Kanaban.Classes;
using MaterialDesignThemes.Wpf;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace Kanaban
{
    /// <summary>
    /// Interaction logic for ctNewProject.xaml
    /// </summary>
    public partial class ctNewProject : UserControl
    {
        public ctNewProject()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Project p = new Project
            {
                Name = txtName.Text,
                Description = txtDescription.Text,
                DatabasePath = txtdbpath.Text
            };
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtdbpath.Text))
            {
                return;
            }

            if ((await AppData.Projects.All()).Any(x=>string.Equals(x.Name, p.Name, StringComparison.CurrentCultureIgnoreCase)))
            {
                MessageBox.Show("A Project with the same name already exists", "Duplicate");
                return;
            }

            if (AppData.Projects.Exists(x => x.DatabasePath == p.DatabasePath))
            {
                if (MessageBox.Show("A Project with the same Database Path already exists.\nDo you want another project with Same Database", "Duplicate",
                        MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
                {
                    return;
                }
            }
            AppData.Projects.Insert(p);
            AppData.LoadNewProject(p);
            DialogHost.CloseDialogCommand.Execute(null , null);
        }
    }
}
