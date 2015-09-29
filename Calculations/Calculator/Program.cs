using Akka.Actor;
using Akka.Configuration;
using Calculations.Messages;
using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {    
            using (var system = ActorSystem.Create("CalculatorsSystem"))
            {
                var client = system.ActorOf(Props.Create<CalculatorCoordinatorActor>());
                client.Tell(new JoinToSystem());

                Console.ReadKey();
            };
        }
    }
}
