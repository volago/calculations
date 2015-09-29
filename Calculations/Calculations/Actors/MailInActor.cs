using Akka.Actor;
using Calculations.Messages;
using System;
using System.Net.Sockets;

namespace Calculations.Actors
{
    public class MailInActor : ReceiveActor
    {
        private IActorRef _coordinator;

        public MailInActor(IActorRef coordinator)
        {
            _coordinator = coordinator;

            Receive<CheckMail>(m =>
            {
                Console.Write("Checking e-mail inbox ...");

                var ex = Helpers.GetRandomInt(0, 40);
                if (ex == 2)
                {
                     throw new SocketException();
                }

                ex = Helpers.GetRandomInt(0, 100000);
                if (ex == 5)
                {
                    throw new ArgumentNullException();
                }
                
                // emulate receiving n e-mails
                int n = Helpers.GetRandomInt(0, 10);
                Console.WriteLine(" {0} e-mails found.", n);

                for (int i = 0; i < n; i++)
                {
                    var from = string.Format("{0}@client.com", Helpers.GetRandomString(10));
                    var loanId = Helpers.GetRandomInt(1000000, 2000000);
                    var calculationOrder = new CalculateLoan(from, loanId);
                    _coordinator.Tell(calculationOrder);
                }
            });
        }        
    }
}
