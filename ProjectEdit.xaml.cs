using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Kanaban.Annotations;

namespace Kanaban
{
    /// <summary>
    /// Interaction logic for ProjectEdit.xaml
    /// </summary>
    public partial class ProjectEdit : INotifyPropertyChanged
    {
        private Project _currentProject;


        public Project CurrentProject
        {
            get => _currentProject;
            set
            {
                if (Equals(value, _currentProject)) return;
                _currentProject = value;
                OnPropertyChanged();
            }
        }

        public event ProjectAddedEventHandler Added;


        public ProjectEdit(Project p = null)
        {
            InitializeComponent();
            CurrentProject = p ?? new Project();
        }


        private void btn_save_click(object sender, RoutedEventArgs e)
        {
            CurrentProject.Save();
            Added?.Invoke(CurrentProject);
            MainWindow.Instance.host.IsOpen = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}