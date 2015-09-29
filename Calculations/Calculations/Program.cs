using Akka.Actor;
using Calculations.Actors;
using Calculations.Messages;
using CalculationsHelpers;
using System;

namespace Calculations
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("CalculationsSystem");

            var mailOutActor = system.ActorOf<MailOutActor>("mailOut");
            var calculatorCoordinatorActor = system.ActorOf<CalculatorCoordinatorActor>("calculatorCoordinator");

            var mailInCoordinatorActor = system.ActorOf(Props.Create<MailInCoordinatorActor>(calculatorCoordinatorActor), "mailInCoordinatorActor");
            var checkMailMsg = new CheckMail();

            system.Scheduler.ScheduleTellRepeatedly(Config.CheckMailStartDelay, Config.CheckMailInterval,
                mailInCoordinatorActor, checkMailMsg, ActorRefs.Nobody);

            Console.ReadKey();
        }
    }
}
