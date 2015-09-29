using Akka.Actor;
using Calculations.Actors;
using Calculations.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            system.Scheduler.ScheduleTellRepeatedly(TimeSpan.FromSeconds(0), TimeSpan.FromMilliseconds(200),
                mailInCoordinatorActor, checkMailMsg, ActorRefs.Nobody);

            Console.ReadKey();
        }
    }
}
