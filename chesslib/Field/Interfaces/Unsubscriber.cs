using System;
using System.Collections.Generic;

namespace chesslib.Field
{
    internal class Unsubscriber : IDisposable
    {
        private List<IObserver<Piece>> _observers;
        private IObserver<Piece> _observer;

        public Unsubscriber(List<IObserver<Piece>> observers, IObserver<Piece> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}