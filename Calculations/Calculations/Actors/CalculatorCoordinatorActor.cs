using Akka.Actor;
using Akka.Routing;
using Calculations.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations.Actors
{
    public class CalculatorCoordinatorActor : ReceiveActor
    {
        IActorRef _calculator;

        public CalculatorCoordinatorActor()
        {
            Receive<CalculateLoan>(msg =>
                {
                    _calculator.Tell(msg);
                });
        }

        protected override void PreStart()
        {
            _calculator = Context.ActorOf(Props.Create(() => new CalculatorActor())
                .WithRouter(new RoundRobinPool(10)));

            base.PreStart();
        }
    }
}
