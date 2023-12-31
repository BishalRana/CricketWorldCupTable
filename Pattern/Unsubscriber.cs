using CricketWorldCupTable.data;
using CricketWorldCupTable.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketWorldCupTable.Pattern
{
    internal sealed class Unsubscriber : IDisposable
    {
        private readonly ISet<IObserver<Dictionary<Participator, Team>>> _observers;
        private readonly IObserver<Dictionary<Participator, Team>> _observer;

        public Unsubscriber(ISet<IObserver<Dictionary<Participator, Team>>> observers, IObserver<Dictionary<Participator, Team>> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose() => _observers.Remove(_observer);

    }
}
