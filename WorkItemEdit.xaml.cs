using System;
using System.Windows;
using System.Windows.Controls;
using LiteDB;
using MaterialDesignThemes.Wpf;

namespace Kanaban
{
    public delegate void SimpleEvent();

    /// <summary>
    /// Interaction logic for WorkItemEdit.xaml
    /// </summary>
    public partial class WorkItemEdit : UserControl
    {
        private WorkItem CurrentItem { get; set; }
        private ListType currentListType;
        private bool isNewItem = false;
        public event SimpleEvent Added;


        public WorkItemEdit(ListType type, WorkItem item = null)
        {
            InitializeComponent();
            currentListType = type;
            if (item != null)
            {
                CurrentItem = item;
            }
            else
            {
                CurrentItem = new WorkItem();
                CurrentItem._id = ObjectId.NewObjectId();
                isNewItem = true;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void WorkItemEdit_OnLoaded(object sender, RoutedEventArgs e)
        {
            cmbPriority.ItemsSource = Enum.GetNames(typeof(Priority));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentItem.Title = txtTitle.Text;
            CurrentItem.Description = txtDescription.Text;
            CurrentItem.Priority = (Priority) cmbPriority.SelectedIndex;
            CurrentItem.Type = currentListType;

            DB.CurrentProject.Add(CurrentItem);

            Added?.Invoke();

            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}