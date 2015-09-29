using Akka.Actor;
using Akka.Configuration;
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
            var calculatorCommanderActor = system.ActorOf<CalculatorComannderActor>("calculatorCommanderActor");

            var mailInCoordinatorActor = system.ActorOf(Props.Create<MailInCoordinatorActor>(calculatorCommanderActor), "mailInCoordinatorActor");
            var checkMailMsg = new CheckMail();

            system.Scheduler.ScheduleTellRepeatedly(CalcConfig.CheckMailStartDelay, CalcConfig.CheckMailInterval,
                mailInCoordinatorActor, checkMailMsg, ActorRefs.Nobody);

            Console.ReadKey();
        }
    }
}
