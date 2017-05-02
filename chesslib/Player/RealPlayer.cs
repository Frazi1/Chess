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
        private MakeMoveCommand _makeMoveCommand;

        public RealPlayer(PlayerColor playerColor)
        {
            PlayerColor = playerColor;
            PlayerType = PlayerType.Human;
        }
        public PlayerColor PlayerColor { get; set; }
        public MakeMoveCommand MakeMoveCommand
        {
            get { return _makeMoveCommand; }
            set { /*if (!_game.IsPaused) */_makeMoveCommand = value; }
        }
        public Thread CurrentThread { get; private set; }
        public PlayerType PlayerType { get; private set; }

        public event EventsDelegates.MoveDoneEventHandler MoveDone;
        public event EventsDelegates.MovingInProcessEventHandler MovingInProcess;

        public void DoTurn(Game game)
        {
            if (MovingInProcess != null)
                MovingInProcess(this, new MovingInProcessEventArgs(this));
            CurrentThread = new Thread(() => MakeMove(game))
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

        private void MakeMove(Game game)
        {
            MakeMoveCommand = null;
            while (MakeMoveCommand == null 
                || !MakeMoveCommand.CanExecute(game))
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
