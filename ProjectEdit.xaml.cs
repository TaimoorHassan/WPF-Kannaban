using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Kanaban
{
    /// <summary>
    /// Interaction logic for ProjectEdit.xaml
    /// </summary>
    public partial class ProjectEdit : UserControl , INotifyPropertyChanged
    {


        public Project CurrentProject { get; set; }
        public event SimpleEvent Added;


        public ProjectEdit(Project p = null)
        {
            InitializeComponent();
            CurrentProject = p ?? new Project();
        }
      

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btn_save_click(object sender, RoutedEventArgs e)
        {
            CurrentProject.Save();
            Added?.Invoke();
        }
    }
}
