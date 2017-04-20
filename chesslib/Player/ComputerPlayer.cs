using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using chesslib.Command;
using chesslib.Strategy;
using System.Threading;

namespace chesslib.Player
{
    public class ComputerPlayer : IPlayer
    {
        public PlayerType PlayerType { get; set; }
        public MakeMoveCommand MakeMoveCommand { get; set; }
        public IStrategy Strategy { get; set; }
        public Game Game { get; set; }

        public event PlayerEventsDelegates.MoveDoneEventHandler MoveDone;
        public ComputerPlayer(PlayerType playerType)
        {
            PlayerType = playerType;
        }

        public void MakeMove()
        {
            Thread.Sleep(500);
            var move = Strategy.PrepareMove();
            MakeMoveCommand = new MakeMoveCommand(this, move.Item1, move.Item2);
            if (MoveDone != null)
                MoveDone(this, new MoveDoneEventArgs(MakeMoveCommand));

            OnMove();

        }

        private void OnMove()
        {
            MakeMoveCommand = null;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(bool value)
        {
            Thread t = new Thread(() => MakeMove());
            t.IsBackground = true;
            t.Start();
        }
    }
}
