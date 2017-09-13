using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Common;
using EmailSendServiceDLL;
using MailItem;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender : Window
    {
        ComboBox cbSenderSelect;
        ComboBox cbSmtpSelect;
        public WpfMailSender()
        {
            InitializeComponent();

            cbSenderSelect = (ComboBox)SenderChoice.FindName("cbSelect");
            cbSenderSelect.ItemsSource = VariablesClass.Senders;

            cbSenderSelect.ItemsSource = VariablesClass.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";

            cbSmtpSelect = (ComboBox)SmtpChoice.FindName("cbSelect");
            cbSmtpSelect.ItemsSource = VariablesClass.SmtpServers;

            cbSmtpSelect.ItemsSource = VariablesClass.SmtpServers;
            cbSmtpSelect.DisplayMemberPath = "Key";
            cbSmtpSelect.SelectedValuePath = "Value";

            DBClass db = new DBClass();
            dgEmails.ItemsSource = db.Emails;
        }

        private void miClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSendAtOnce_Click(object sender, RoutedEventArgs e)
        {
            string strLogin = cbSenderSelect.Text;
            string strPassword = cbSenderSelect.SelectedValue.ToString();
            if (string.IsNullOrEmpty(strLogin) || string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("Выберите отправителя");
                return;
            }
            string strSmtp = cbSmtpSelect.Text;
            int iPort = Convert.ToInt32(cbSmtpSelect.SelectedValue);
            if (string.IsNullOrEmpty(strSmtp) || iPort == 0)
            {
                MessageBox.Show("Выберите smtp-сервер");
                return;
            }

            string strBody;
            //var start = richTextBox.Document.ContentStart;
            //var end = richTextBox.Document.ContentEnd;
            //int difference = start.GetOffsetToPosition(end);

            if (IsRichTextBoxEmpty(richTextBox.Document))
            {
                MessageBox.Show("Не указан текст письма");
                tabControl.SelectedItem = tabLetterEditor;
                return;
            }
            else
            {
                strBody = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
            }

            EmailSendServiceClass emailSender = new EmailSendServiceClass(strLogin, strPassword, strSmtp, iPort, strBody);
            emailSender.SendMails((IQueryable<Emails>)dgEmails.ItemsSource);
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SchedulerClass sc = new SchedulerClass();
            //TimeSpan tsSendTime = sc.GetSendTime(tbTimePicker.Text);
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
            //EmailSendServiceClass emailSender = new EmailSendServiceClass(cbSenderSelect.Text, 
            //                                                              cbSenderSelect.SelectedValue.ToString(),
            //                                                              cbSmtpSelect.Text,
            //                                                              Convert.ToInt32(cbSmtpSelect.SelectedValue),
            //                                                              new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text
            //                                                              );
            //sc.SendEmails(dtSendDateTime, emailSender, (IQueryable<Emails>)dgEmails.ItemsSource);

            foreach (object l in listViewMails.Items)
            {
                MailItemControl ll = (MailItemControl)l;
                sc.DatesEmailTexts.Add(Convert.ToDateTime((((ll).TbTimePickerText))), ll.MailText);
                
                EmailSendServiceClass emailSender = new EmailSendServiceClass(cbSenderSelect.Text,
                                                                              cbSenderSelect.SelectedValue.ToString(),
                                                                              cbSmtpSelect.Text,
                                                                              Convert.ToInt32(cbSmtpSelect.SelectedValue),
                                                                              new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text
                                                                              );
                sc.SendEmails(emailSender, (IQueryable<Emails>)dgEmails.ItemsSource);
            }
        }

        private void btnClock_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabPlanner;
        }

        bool IsRichTextBoxEmpty(FlowDocument document)
        {
            string text = new TextRange(document.ContentStart, document.ContentEnd).Text;
            if (string.IsNullOrWhiteSpace(text) == false)
                return false;
            else
            {
                if (document.Blocks.OfType<BlockUIContainer>()
                    .Select(c => c.Child).OfType<Image>()
                    .Any())
                    return false;
            }
            return true;
        }

        private void tscTabSwitcher_btnNextClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex += 1;
        }

        private void tscTabSwitcher_btnPreviousClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex -= 1;
        }

        private void ButtonNewTime_Click(object sender, RoutedEventArgs e)
        {
            listViewMails.Items.Add(new MailItemControl());
            MailItemControl l = (MailItemControl)listViewMails.Items[listViewMails.Items.Count - 1];
            l.btnMinusClick += (a, b) => { listViewMails.Items.Remove(l); };
            l.btnEditClick += (a, b) => {
                TextWindow w = new TextWindow();
                w.Loaded += (a2, b2) => Data.MailText = l.MailText;
                w.Closed += (a1, b1) => l.MailText = Data.MailText;
                w.ShowDialog();
            };
        }
    }
}
