using DL.DataAccess;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Receiver receiver = new Receiver();
            while (receiver.FullName == null || receiver.FullName == string.Empty)
            {
                Console.Write("Введите имя получателя: ");
                receiver.FullName = Console.ReadLine();
            }

            while (receiver.Address == null || receiver.Address == string.Empty)
            {
                Console.Write("Введите адрес получателя: ");
                receiver.Address = Console.ReadLine();
            }

            var receivers = new List<Receiver>();

            using (var repository = new ReceiversRepository())
                receivers = repository.GetAll() as List<Receiver>;

            for (int i = 0; i < receivers.Count; i++)
            {
                if(receivers[i].FullName == receiver.FullName && receivers[i].Address == receivers[i].Address)
                {
                    receiver = receivers[i];
                    break;
                }
                if(i == receivers.Count - 1)
                    using (var repository = new ReceiversRepository())
                        repository.Add(receiver);
            }

            Mail mail = new Mail();

            while(mail.Theme == null || mail.Theme == string.Empty)
            {
                Console.Write("Введите тему письма: ");
                mail.Theme = Console.ReadLine();
            }

            while (mail.Text == null || mail.Text == string.Empty)
            {
                Console.WriteLine("Введите текст письма: ");
                mail.Text = Console.ReadLine();
            }

            mail.ReceiverId = receiver.Id;

            using (var repository = new MailsRepository())
                repository.Add(mail);

            Console.WriteLine("Письмо отправлено");
        }
    }
}
