using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Kanaban.Annotations;

namespace Kanaban.Input
{

    public delegate void Edited(string newText);
    /// <summary>
    /// Interaction logic for EditableTextBlock.xaml
    /// </summary>
    public partial class EditableTextBlock : INotifyPropertyChanged
    {

        public static readonly DependencyProperty ToggleVisibilityProperty = DependencyProperty.Register(
            "ToggleVisibility", typeof(Visibility), typeof(EditableTextBlock), new PropertyMetadata(Visibility.Hidden));

        public Visibility ToggleVisibility
        {
            get => (Visibility) GetValue(ToggleVisibilityProperty);
            set => SetValue(ToggleVisibilityProperty, value);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(EditableTextBlock), new PropertyMetadata(default(string)));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private bool _isEditMode;

        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                if (value == _isEditMode) return;
                _isEditMode = value;
                OnEditModeChange(value);
                OnPropertyChanged();
            }
        }

        public event Edited TextEdited;
        private string oldText;

        private void OnEditModeChange(bool value)
        {
            
            if (value) // Means we have entered Edit Mode
            {
                oldText = txt.Text;
                txt.IsReadOnly = false;
                txt.BorderThickness = new Thickness(0,0,0,2);
            }
            else // Means We have Exited Edit Mode
            {
                TextEdited?.Invoke(txt.Text);
                txt.IsReadOnly = true;
                txt.BorderThickness = new Thickness(0);
            }
        }


        public EditableTextBlock()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void EditableTextBlock_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsEditMode) return;

            switch (e.Key)
            {
                case Key.Escape:
                    txt.Text = oldText;
                    IsEditMode = false;
                    break;
                case Key.Enter:
                    IsEditMode = false;
                    break;
            }
        }
    }
}
