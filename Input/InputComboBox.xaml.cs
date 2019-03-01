using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kanaban.Input
{
    /// <summary>
    /// Interaction logic for InputComboBox.xaml
    /// </summary>
    public partial class InputComboBox : UserControl
    {
    public static readonly DependencyProperty LabelDependencyProperty = DependencyProperty.Register(nameof(Label), typeof(string), typeof(InputComboBox));
        public static readonly DependencyProperty DetailLeftDependencyProperty = DependencyProperty.Register(nameof(DetailLeft), typeof(string), typeof(InputComboBox));
        public static readonly DependencyProperty DetailRightDependencyProperty = DependencyProperty.Register(nameof(DetailRight), typeof(string), typeof(InputComboBox));
        public static readonly DependencyProperty ItemsSourceDependencyProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(InputComboBox));
        public static readonly DependencyProperty IsEditableDependencyProperty = DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(InputComboBox));


        public InputComboBox()
        {
            InitializeComponent();
            DataContext = this;
        }


        public string DetailLeft
        {
            get
            {
                OnPropertyChanged(nameof(DetailLeft));
                return (string) GetValue(DetailLeftDependencyProperty);
            }
            set
            {
                SetValue(DetailLeftDependencyProperty, value);
                OnPropertyChanged(nameof(DetailLeft));
            }
        }

        public string DetailRight
        {
            get
            {
                OnPropertyChanged(nameof(DetailRight));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DetailRight)));

                return (string) GetValue(DetailRightDependencyProperty);
            }
            set
            {
                SetValue(DetailRightDependencyProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DetailRight)));

                OnPropertyChanged(nameof(DetailRight));
            }
        }

        public bool IsEditable
        {
            get
            {
                OnPropertyChanged(nameof(IsEditable));
                return (bool) GetValue(IsEditableDependencyProperty);
            }
            set
            {
                SetValue(IsEditableDependencyProperty, value);
                OnPropertyChanged(nameof(IsEditable));
            }
        }


        public IEnumerable ItemsSource
        {
            get
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemsSource)));
                return (IEnumerable) GetValue(ItemsSourceDependencyProperty);
            }
            set
            {
                SetValue(ItemsSourceDependencyProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemsSource)));
            }
        }

        public string Label
        {
            get
            {
                OnPropertyChanged(nameof(Label));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Label)));
                return (string) GetValue(LabelDependencyProperty);
            }
            set
            {
                OnPropertyChanged(nameof(Label));
                SetValue(LabelDependencyProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Label)));
                OnPropertyChanged(nameof(Label));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        

        public object GetSelectedItem()
        {
            return cmb.SelectedItem;
        }

        public void SetItemsSource(IEnumerable _source)
        {
            cmb.ItemsSource = null;
            cmb.Items.Clear();
            cmb.ItemsSource = _source;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

        

        public void SelectFirst()
        {
            if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
        }
    }


    public class RelayCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var a = parameter as Action;
            a?.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
