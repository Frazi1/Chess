using chesslib.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using chesslib.Command;
using chesslib.Strategy;
using System.Threading.Tasks;
using chesslib.Events;

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
    }
}
