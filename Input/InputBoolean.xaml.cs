using System.Windows;
using System.Windows.Controls;

namespace Kanaban.Input
{
    /// <summary>
    /// Interaction logic for InputBoolean.xaml
    /// </summary>
    public partial class InputBoolean : UserControl
    {
        public InputBoolean()
        {
            InitializeComponent();
        }
        
        public static DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(string), typeof(InputBoolean), new PropertyMetadata(""));

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

        public static DependencyProperty CheckedProperty = DependencyProperty.Register(
            "Checked", typeof(bool?), typeof(InputBoolean), (PropertyMetadata)new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal ));

        public bool? Checked
        {
            get
            {
                return (bool?)GetValue(CheckedProperty);
            }
            set
            {
                SetValue(CheckedProperty, value);
            }
        }
    }
}
