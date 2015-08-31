using Akka.Actor;
using Calculations.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Calculations.Actors
{
    public class MailOutActor : ReceiveActor
    {
        public MailOutActor()
        {
            Receive<SendMail>(msg =>
                {
                    Thread.Sleep(500);
                    Console.WriteLine("Sended e-mail to {0} with result {1}.", msg.To, msg.Result);
                });
        }
    }
}
