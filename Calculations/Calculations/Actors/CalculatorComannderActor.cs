using System;
using Akka.Actor;
using CalculationsHelpers.Messages;
using System.Collections.Generic;
using Calculations.Messages;
using CalculationsHelpers;
using System.Linq;

namespace Calculations.Actors
{
    public class CalculatorComannderActor : TypedActor
        , IHandle<JoinToSystem>
        , IHandle<CalculateLoan>
    {
        private List<IActorRef> _coordinators = new List<IActorRef>();
        private int _button = 0;

        public void Handle(CalculateLoan message)
        {
            if (_coordinators.Any())
            {
                _coordinators[_button].Tell(message);
                _button = _button + 1;
                _button = _button % _coordinators.Count;
            }
            
        }

        public void Handle(JoinToSystem message)
        {
            _coordinators.Add(Sender);
        }
    }
}
