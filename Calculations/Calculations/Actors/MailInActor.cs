using Akka.Actor;
using Calculations.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations.Actors
{
    public class MailInActor : ReceiveActor
    {
        private Random _rnd = new Random();
        private IActorRef _coordinator;

        public MailInActor(IActorRef coordinator)
        {
            _coordinator = coordinator;

            Receive<CheckMail>(m =>
            {
                Console.Write("Checking e-mail inbox ...");
                
                // emulate receiving n e-mails
                int n = _rnd.Next(0, 10);
                Console.WriteLine(" {0} e-mails found.", n);

                for (int i = 0; i < n; i++)
                {
                    var from = string.Format("{0}@client.com", RandomString(10).ToLower());
                    var loanId = _rnd.Next(1000000, 2000000);
                    var calculationOrder = new CalculateLoan(from, loanId);
                    _coordinator.Tell(calculationOrder);
                }
            });
        }


        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _rnd.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
