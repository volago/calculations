using Akka.Actor;
using Calculations.Messages;
using CalculationsHelpers;
using System;
using System.Threading;

namespace Calculations.Actors
{
    public class CalculatorActor : ReceiveActor
    {
        public CalculatorActor()
        {
            Receive<CalculateLoan>(msg =>
            {
                var actorName = Context.Self.Path.Name;
                Console.WriteLine("[CalculatorActor  {2}]: Start processing: from = {0}, loanId = {1}", msg.From, msg.LoanId, actorName);

                var sleep = Helpers.GetRandomInt(Config.MessageProcessingTimeMinMs, Config.MessageProcessingTimeMaxMs);
                Thread.Sleep(sleep);

                var result = Helpers.GetRandomInt(Config.MaxResult);
                
                var mailBoxSize = ((ActorCell)Context).NumberOfMessages;


                Console.WriteLine("[CalculatorActor  {2}]: Stop processing: from = {0}, loanId = {1}, result = {3} ", msg.From, msg.LoanId, actorName, result);
                Console.WriteLine("[CalculatorActor  {1}]: Inbox size: {0}", mailBoxSize, actorName);

                var mailOutActor = Context.ActorSelection("akka://CalculationsSystem/user/mailOut");
                mailOutActor.Tell(new SendMail(msg.From, result));
            });
        }
    }
}
