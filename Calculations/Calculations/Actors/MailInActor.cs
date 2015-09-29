using Akka.Actor;
using Calculations.Messages;
using CalculationsHelpers;
using System;
using System.Net.Sockets;

namespace Calculations.Actors
{
    public class MailInActor : ReceiveActor
    {
        private IActorRef commander;

        public MailInActor(IActorRef commander)
        {
            this.commander = commander;

            Receive<CheckMail>(m =>
            {
                Console.Write("[MailInActor        ]: Checking e-mail inbox ...");

                var ex = Helpers.GetRandomInt(CalcConfig.NetworkExceptionChance);
                if (ex == 2)
                {
                     throw new SocketException();
                }

                ex = Helpers.GetRandomInt(CalcConfig.FatalExceptionChance);
                if (ex == 5)
                {
                    throw new ArgumentNullException();
                }
                
                // emulate receiving n e-mails
                int n = Helpers.GetRandomInt(CalcConfig.MaxNumberEmailsReceived);
                Console.WriteLine(" {0} e-mails found.", n);

                for (int i = 0; i < n; i++)
                {
                    var from = Helpers.GetRandomEmail();
                    var loanId = Helpers.GetRandomLoadId();
                    var calculationOrder = new CalculateLoan(from, loanId);
                    this.commander.Tell(calculationOrder);
                }
            });
        }        
    }
}
