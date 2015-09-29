using Akka.Actor;
using Calculations.Messages;
using System;
using System.Net.Sockets;

namespace Calculations.Actors
{
    public class MailInCoordinatorActor : TypedActor
        , IHandle<CheckMail>
    {
        private IActorRef _mailInActor;
        private IActorRef _calculatorCommander;

        public MailInCoordinatorActor(IActorRef calculatorCommander)
        {
            _calculatorCommander = calculatorCommander;
        }

        protected override void PreStart()
        {
            _mailInActor = Context.ActorOf(Props.Create<MailInActor>(_calculatorCommander), "mailInActor");
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
                        Console.WriteLine("[MailInCoordinator   ]: Network exception - restarting MailInActor ...");
                        return Directive.Restart;                        
                    }
                    else
                    {
                        Console.WriteLine("[MailInCoordinator   ]: MailInActor crashed - stopping MailInActor ... further messages won't be process.");
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
