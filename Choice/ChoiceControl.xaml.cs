using System.Windows;
using System.Windows.Controls;

namespace Choice
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ChoiceControl : UserControl
    {
        public ChoiceControl()
        {
            InitializeComponent();
        }

        private string labelText = "";

        public string LabelText
        {
            get { return labelText; }
            set
            {
                labelText = value;
                label.Content = value;
            }
        }

        public static readonly DependencyProperty ComboboxSelectedItemProperty =
        DependencyProperty.Register("CbSelectedItem", typeof(object), typeof(ChoiceControl), new UIPropertyMetadata(""));

        public object CbSelectedItem
        {
            get { return (object)GetValue(ComboboxSelectedItemProperty); }
            set { SetValue(ComboboxSelectedItemProperty, value); }
        }
        
        public event RoutedEventHandler btnPlusClick;
        public event RoutedEventHandler btnMinusClick;
        public event RoutedEventHandler btnEditClick;
                
        private void btnAddSender_Click(object sender, RoutedEventArgs e)
        {
            btnPlusClick?.Invoke(sender, e);
        }

        private void btnEditSender_Click(object sender, RoutedEventArgs e)
        {
            btnEditClick?.Invoke(sender, e);
        }

        private void btnDeleteSender_Click(object sender, RoutedEventArgs e)
        {
            btnMinusClick?.Invoke(sender, e);
        }
    }
}
