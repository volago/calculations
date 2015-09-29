using Akka.Actor;
using Calculations.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Calculations.Actors
{
    public class MailInCoordinatorActor : TypedActor
        , IHandle<CheckMail>
    {
        private IActorRef _mailInActor;
        private IActorRef _calculatorCoordinator;

        public MailInCoordinatorActor(IActorRef calculatorCoordinator)
        {
            _calculatorCoordinator = calculatorCoordinator;
        }

        protected override void PreStart()
        {
            _mailInActor = Context.ActorOf(Props.Create<MailInActor>(_calculatorCoordinator), "mailInActor");
            base.PreStart();
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                maxNrOfRetries: 10,
                withinTimeRange: TimeSpan.FromSeconds(3),
                localOnlyDecider: x =>
                {
                    if (x is SocketException)
                    {
                        Console.WriteLine("Network exception - restarting MailInActor ...");
                        return Directive.Restart;                        
                    }
                    else
                    {
                        Console.WriteLine("MailInActor crashed - stopping MailInActor ... further messages won't be process.");
                        Environment.Exit(2);
                        return Directive.Stop;
                    }
                });
        }        

        public void Handle(CheckMail message)
        {
            _mailInActor.Tell(message);
        }
    }
}
