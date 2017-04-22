using chesslib.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using chesslib.Command;
using chesslib.Strategy;
using System.Threading.Tasks;
using chesslib.Events;
using System.Threading;

namespace chesslib.Player
{
    public class RealPlayer : IPlayer
    {
        private Game _game;
        private IStrategy strategy;

        public RealPlayer(PlayerType playerType)
        {
            PlayerType = playerType;
        }
        public Game Game
        {
            get { return _game; }
            set { _game = value; }
        }
        public PlayerType PlayerType { get; set; }
        public MakeMoveCommand MakeMoveCommand { get; set; }
        public IStrategy Strategy
        {
            get { return strategy; }
            set { strategy = value; }
        }

        public event EventsDelegates.MoveDoneEventHandler MoveDone;
        public event EventsDelegates.MovingInProcessEventHandler MovingInProcess;

        public void DoTurn()
        {
            if (MovingInProcess != null)
                MovingInProcess(this, new MovingInProcessEventArgs(this));
            Task.Factory.StartNew(() => MakeMove());
        }

        private void MakeMove()
        {
            while (Strategy == null)
            {
                Thread.Sleep(100);
            }
            var move = Strategy.PrepareMove();
            MakeMoveCommand = new MakeMoveCommand(this, move.Item1, move.Item2);

            if (MoveDone != null)
                MoveDone(this, new MoveDoneEventArgs(MakeMoveCommand));

            OnMove();
        }

        private void OnMove()
        {
            MakeMoveCommand = null;
            Strategy = null;
        }
    }
}
