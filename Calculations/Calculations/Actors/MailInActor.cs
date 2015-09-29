using Akka.Actor;
using Calculations.Messages;
using CalculationsHelpers;
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
                Console.Write("[MailInActor        ]: Checking e-mail inbox ...");

                var ex = Helpers.GetRandomInt(Config.NetworkExceptionChance);
                if (ex == 2)
                {
                     throw new SocketException();
                }

                ex = Helpers.GetRandomInt(Config.FatalExceptionChance);
                if (ex == 5)
                {
                    throw new ArgumentNullException();
                }
                
                // emulate receiving n e-mails
                int n = Helpers.GetRandomInt(Config.MaxNumberEmailsReceived);
                Console.WriteLine(" {0} e-mails found.", n);

                for (int i = 0; i < n; i++)
                {
                    var from = Helpers.GetRandomEmail();
                    var loanId = Helpers.GetRandomLoadId();
                    var calculationOrder = new CalculateLoan(from, loanId);
                    _coordinator.Tell(calculationOrder);
                }
            });
        }        
    }
}
