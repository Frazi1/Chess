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
        public Thread CurrentThread { get; private set; }

        public event EventsDelegates.MoveDoneEventHandler MoveDone;
        public event EventsDelegates.MovingInProcessEventHandler MovingInProcess;

        public void DoTurn()
        {
            if (MovingInProcess != null)
                MovingInProcess(this, new MovingInProcessEventArgs(this));
            CurrentThread = new Thread(MakeMove)
            {
                IsBackground = true
            };
            CurrentThread.Start();
        }

        public void CancelTurn()
        {
            if (CurrentThread != null && CurrentThread.IsAlive)
                CurrentThread.Abort();
        }

        private void MakeMove()
        {
            while (MakeMoveCommand == null 
                || !MakeMoveCommand.CanExecute(Game))
            {
                Thread.Sleep(100);
            }
            if (MoveDone != null)
                MoveDone(this, new MoveDoneEventArgs(MakeMoveCommand));

            OnMove();
        }

        private void OnMove()
        {
            MakeMoveCommand = null;
        }
    }
}
