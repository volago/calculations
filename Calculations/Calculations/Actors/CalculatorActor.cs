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
    public class CalculatorActor : ReceiveActor
    {
        public CalculatorActor()
        {
            Receive<CalculateLoan>(msg =>
            {
                var actorName = Context.Self.Path.Name;
                Console.WriteLine("Calculating: {0}, {1} by actor {2}", msg.From, msg.LoanId, actorName);

                var sleep = Helpers.GetRandomInt(1000, 10000);
                Thread.Sleep(sleep);

                var r = Helpers.GetRandomInt(0, 400);
                
                var mailBoxSize = ((ActorCell)Context).NumberOfMessages;


                Console.WriteLine("Calculated: {0}, {1} by actor {2}, result = {3} ", msg.From, msg.LoanId, actorName, r);
                Console.WriteLine("Inbox size: {0}", mailBoxSize);

                var mailOutActor = Context.ActorSelection("akka://CalculationsSystem/user/mailOut");
                mailOutActor.Tell(new SendMail(msg.From, r));
            });
        }
    }
}
