using Akka.Actor;
using Akka.Routing;
using Calculations.Messages;
using CalculationsHelpers;
using CalculationsHelpers.Messages;

namespace Calculator
{
    public class CalculatorCoordinatorActor : ReceiveActor
    {
        IActorRef _calculator;
        private readonly ActorSelection _server = Context.ActorSelection("akka.tcp://CalculationsSystem@172.18.56.77:8081/user/calculatorCommanderActor");

        public CalculatorCoordinatorActor()
        {
            Receive<CalculateLoan>(msg =>
            {
                _calculator.Tell(msg);
            });

            Receive<JoinToSystem>(msg =>
            {
                _server.Tell(new JoinToSystem());
            });
        }

        protected override void PreStart()
        {
            _calculator = Context.ActorOf(Props.Create(() => new CalculatorActor())
                .WithRouter(new RoundRobinPool(CalcConfig.NumberOfCalculatorWorkers)));

            base.PreStart();
        }
    }
}
