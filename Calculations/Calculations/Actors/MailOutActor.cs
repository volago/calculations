using Akka.Actor;
using Calculations.Messages;
using CalculationsHelpers;
using System;
using System.Threading;

namespace Calculations.Actors
{
    public class MailOutActor : ReceiveActor
    {
        public MailOutActor()
        {
            Receive<SendMail>(msg =>
                {
                    Thread.Sleep(Config.MailOutDelayMs);
                    Console.WriteLine("[MailOutActor       ]: E-mail to {0} with result {1} was sent.", msg.To, msg.Result);
                });
        }
    }
}
