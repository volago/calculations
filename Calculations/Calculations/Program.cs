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
            var coordinator = system.ActorOf<CalculatorCoordinatorActor>("coordinator");

            var mailInActor = system.ActorOf(Props.Create<MailInActor>(coordinator), "mailInActor");
            var checkMailMsg = new CheckMail();

            system.Scheduler.ScheduleTellRepeatedly(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(15),
                mailInActor, checkMailMsg, ActorRefs.Nobody);

            Console.ReadKey();
        }
    }
}
