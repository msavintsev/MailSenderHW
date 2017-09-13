using System.Collections.Generic;
using CodePasswordDLL;

namespace MailSender
{
        public static class VariablesClass
        {
            public static Dictionary<string, string> Senders
            {
                get { return dicSenders; }
            }
            private static Dictionary<string, string> dicSenders = new Dictionary<string, string>()
            {
                { "1111@yandex.ru", CodePassword.getPassword("1111") },
                { "1111@gmail.com", CodePassword.getPassword("1111") }
            };

            public static Dictionary<string, int> SmtpServers
            {
                get { return dicSmtpServers; }
            }
            private static Dictionary<string, int> dicSmtpServers = new Dictionary<string, int>()
            {
                { "smtp.yandex.ru", 25 },
                { "smtp.gmail.com", 587 }
            };
    }
}
