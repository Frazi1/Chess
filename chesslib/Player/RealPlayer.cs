using chesslib.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using chesslib.Command;
using chesslib.Strategy;
using System.Threading.Tasks;

namespace chesslib.Player
{
    public class RealPlayer : IPlayer, IObservable<RealPlayer>
    {
        private Game _game;
        private IStrategy strategy;
        public Game Game
        {
            get { return _game; }
            set { _game = value; }
        }

        public RealPlayer(PlayerType playerType)
        {
            PlayerType = playerType;
            _observers = new List<IObserver<RealPlayer>>();
        }

        public PlayerType PlayerType { get; set; }
        public MakeMoveCommand MakeMoveCommand { get; set; }
        public IStrategy Strategy
        {
            get { return strategy; }
            set { strategy = value; }
        }

        public void PrepareMove()
        {
            Task<Tuple<Piece, Cell>> t = new Task<Tuple<Piece, Cell>>(() => Strategy.PrepareMove());
            t.Start();
            
            var move = t.Result; 
            MakeMoveCommand = new MakeMoveCommand(this, move.Item1, move.Item2);
        }

        public void MakeMove()
        {
            PrepareMove();
            if (MoveDone != null)
                MoveDone(this, new MoveDoneEventArgs(MakeMoveCommand));
        }

        private void OnMove()
        {
            MakeMoveCommand = null;
        }
        #region Game Observer

        public void OnNext(bool value)
        {
            if (value)
                NotifyForMoveInput();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IObservable
        private List<IObserver<RealPlayer>> _observers;

        public event PlayerEventsDelegates.MoveDoneEventHandler MoveDone;

        public IDisposable Subscribe(IObserver<RealPlayer> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber<RealPlayer>(_observers, observer);
        }
        private void NotifyForMoveInput()
        {
            foreach (var obs in _observers)
            {
                obs.OnNext(this);
            }
        }
        #endregion
    }
}
