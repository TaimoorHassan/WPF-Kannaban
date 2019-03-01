using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Kanaban.Input
{
    /// <summary>
    /// Interaction logic for InputText.xaml
    /// </summary>
    public partial class InputText : UserControl
    {
        public InputText()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextForegroundProperty = DependencyProperty.Register(
            "TextForeground", typeof(Brush), typeof(InputText), new PropertyMetadata(Brushes.Black));

        public Brush TextForeground
        {
            get { return (Brush) GetValue(TextForegroundProperty); }
            set { SetValue(TextForegroundProperty, value); }
        }

        public static DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(string), typeof(InputText), new PropertyMetadata(""));

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
            "Text", typeof(string), typeof(InputText), (PropertyMetadata)new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));

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


        private void Txt_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            txt.SelectAll();
        }
    }
}
