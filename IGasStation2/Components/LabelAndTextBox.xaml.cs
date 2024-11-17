using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGasStation2.Components
{
    /// <summary>
    /// Interaction logic for LabelAndTextBox.xaml
    /// </summary>
    public partial class LabelAndTextBox : UserControl
    {
        public LabelAndTextBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ControlLabelContentProperty =
        DependencyProperty.Register("ControlLabelContent",
                                    typeof(string),
                                    typeof(LabelAndTextBox));

        public string ControlLabelContent
        {
            get { return (string)GetValue(ControlLabelContentProperty); }
            set { SetValue(ControlLabelContentProperty, value); }
        }

        public static readonly DependencyProperty ControlTextBoxTextProperty =
        DependencyProperty.Register("ControlTextBoxText",
                                    typeof(string),
                                    typeof(LabelAndTextBox));

        public string ControlTextBoxText
        {
            get { return (string)GetValue(ControlTextBoxTextProperty); }
            set { SetValue(ControlTextBoxTextProperty, value); }
        }
    }
}
