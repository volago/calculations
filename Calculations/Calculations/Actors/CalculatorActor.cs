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
        private Random _rnd = new Random();

        public CalculatorActor()
        {
            Receive<CalculateLoan>(msg =>
            {
                var actorName = Context.Self.Path.Name;
                Console.WriteLine("Calculating: {0}, {1} by actor {2}", msg.From, msg.LoanId, actorName);

                var sleep = _rnd.Next(1000, 10000);
                Thread.Sleep(sleep);

                var r = _rnd.Next(400);

                //Console.WriteLine("Calculated: {0}, {1} by actor {2}, result = {3} ", msg.From, msg.LoanId, actorName, r);

                var mailOutActor = Context.ActorSelection("akka://CalculationsSystem/user/mailOut");
                mailOutActor.Tell(new SendMail(msg.From, r));
            });
        }
    }
}
