using System;
using System.Windows;
using System.Windows.Controls;

namespace MailItem
{
    /// <summary>
    /// Interaction logic for MailItemControl.xaml
    /// </summary>
    public partial class MailItemControl : UserControl
    {
        private string mailText = "";

        private string tbTimePickerText = "";
        public string TbTimePickerText
        {
            get { return tbTimePicker.Text; }
            set
            {
                tbTimePickerText = value;
                tbTimePicker.Text = value;
            }
        }

        public string MailText
        {
            get
            {
                return mailText;
            }

            set
            {
                mailText = value;
            }
        }

        public MailItemControl()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler btnEditClick;
        public event RoutedEventHandler btnMinusClick;

        private void btnEditSender_Click(object sender, RoutedEventArgs e)
        {
            btnEditClick?.Invoke(sender, e);
        }

        private void btnDeleteSender_Click(object sender, RoutedEventArgs e)
        {
            btnMinusClick?.Invoke(sender, e);
        }

        private void tbTimePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //TimeSpan tsSendTime = GetSendTime(tbTimePicker.Text);

            //if (tsSendTime == new TimeSpan())
            //{
            //    MessageBox.Show("Некорректный формат даты");
            //    return;
            //}
            //DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);
            //if (dtSendDateTime < DateTime.Now)
            //{
            //    MessageBox.Show("Дата и время отправки писем не могут быть раньше, чем настоящее время");
            //    return;
            //}
        }
        public TimeSpan GetSendTime(string strSendTime)
        {
            TimeSpan tsSendTime = new TimeSpan();
            try
            {
                tsSendTime = TimeSpan.Parse(strSendTime);
            }
            catch { }

            return tsSendTime;
        }
    }
}
