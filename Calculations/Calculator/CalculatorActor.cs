using Akka.Actor;
using Calculations.Messages;
using CalculationsHelpers;
using System;
using System.Threading;

namespace Calculator
{
    public class CalculatorActor : ReceiveActor
    {
        public CalculatorActor()
        {
            Receive<CalculateLoan>(msg =>
            {
                var actorName = Context.Self.Path.Name;
                Console.WriteLine("[CalculatorActor  {2}]: Start processing: from = {0}, loanId = {1}", msg.From, msg.LoanId, actorName);

                var sleep = Helpers.GetRandomInt(CalcConfig.MessageProcessingTimeMinMs, CalcConfig.MessageProcessingTimeMaxMs);
                Thread.Sleep(sleep);

                var result = Helpers.GetRandomInt(CalcConfig.MaxResult);
                
                var mailBoxSize = ((ActorCell)Context).NumberOfMessages;


                Console.WriteLine("[CalculatorActor  {2}]: Stop processing: from = {0}, loanId = {1}, result = {3} ", msg.From, msg.LoanId, actorName, result);
                Console.WriteLine("[CalculatorActor  {1}]: Inbox size: {0}", mailBoxSize, actorName);

                var mailOutActor = Context.ActorSelection("akka.tcp://CalculationsSystem@172.18.56.77:8081/user/mailOut");
                mailOutActor.Tell(new SendMail(msg.From, result));
            });
        }
    }
}
