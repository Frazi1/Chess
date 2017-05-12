using chesslib.Command;
using chesslib.Strategy;
using System.Threading;
using chesslib.Events;

namespace chesslib.Player
{
    public class ComputerPlayer : IPlayer
    {
        private IStrategy Strategy { get; }
        private Thread CurrentThread { get; set; }

        public PlayerColor PlayerColor { get; set; }
        public MakeMoveCommand MakeMoveCommand { get; set; }
        public PlayerType PlayerType { get; }
        public event EventsDelegates.MoveDoneEventHandler MoveDone;

        public ComputerPlayer(PlayerColor playerColor, IStrategy strategy)
        {
            PlayerColor = playerColor;
            PlayerType = PlayerType.Computer;
            Strategy = strategy;
        }

        private void MakeMove(Game game)
        {
            Thread.Sleep(500);
            var move = Strategy.PrepareMove(this, game.Board);
            MakeMoveCommand = new MakeMoveCommand(PlayerColor, move);
            if (MoveDone != null)
                MoveDone(this, new MoveDoneEventArgs(MakeMoveCommand));

            OnMove();
        }
        private void OnMove()
        {
            MakeMoveCommand = null;
        }

        public void DoTurn(Game game)
        {
            CurrentThread = new Thread(() => MakeMove(game))
            {
                IsBackground = true
            };
            CurrentThread.Start();
        }
        public void CancelTurn()
        {
            if (CurrentThread!=null && CurrentThread.IsAlive)
                CurrentThread.Abort();
        }
    }
}
