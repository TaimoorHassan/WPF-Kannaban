using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Kanaban.Input
{
    /// <summary>
    /// Interaction logic for InputNumbers.xaml
    /// </summary>
    public partial class InputNumbers : UserControl
    {
        public InputNumbers()
        {
            InitializeComponent();
        }

        public static DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(string), typeof(InputNumbers), new PropertyMetadata(""));

        public string Label
        {
            get
            {
                return (string)GetValue(LabelProperty);
            }
            set
            {
                SetValue(LabelProperty, value);
            }
        }

        public static DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(InputNumbers), (PropertyMetadata)new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal, null, null, true, UpdateSourceTrigger.PropertyChanged));

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }


        private void Txt_OnGotFocus(object sender, RoutedEventArgs e)
        {
            txt.SelectAll();

        }
    }
}
