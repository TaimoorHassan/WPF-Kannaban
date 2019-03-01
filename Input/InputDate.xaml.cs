using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Kanaban.Input
{
    /// <summary>
    /// Interaction logic for InputDate.xaml
    /// </summary>
    public partial class InputDate : UserControl  , INotifyPropertyChanged
    {
        public static readonly DependencyProperty TextDependencyProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(InputDate));
        public static readonly DependencyProperty CurrentDateDependencyProperty = DependencyProperty.Register(nameof(CurrentDate), typeof(DateTime), typeof(InputDate));

        public InputDate()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string Text
        {
            get
            {
                OnPropertyChanged(nameof(Text));
                return (string) GetValue(TextDependencyProperty);
            }
            set
            {
                SetValue(TextDependencyProperty, value);
                OnPropertyChanged(nameof(Text));
            }
        }

        public DateTime CurrentDate
        {
            get
            {
                OnPropertyChanged();
                return (DateTime) GetValue(CurrentDateDependencyProperty);
            }
            set
            {
                SetValue(CurrentDateDependencyProperty, value);
                OnPropertyChanged(nameof(CurrentDate));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event SelectionChangedEventHandler CurrentDateChanged;

        public void SetDate(DateTime date)
        {
            dp.SelectedDate = date;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentDateChanged?.Invoke(sender, e);
        }
    }
}
