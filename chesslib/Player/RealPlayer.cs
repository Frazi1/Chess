using chesslib.Command;
using chesslib.Events;
using System.Threading;

namespace chesslib.Player
{
    public class RealPlayer : IPlayer
    {
        private MakeMoveCommand _makeMoveCommand;

        private Thread CurrentThread { get; set; }

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
        public PlayerType PlayerType { get; private set; }

        public event EventsDelegates.MoveDoneEventHandler MoveDone;

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
            if (CurrentThread != null && CurrentThread.IsAlive)
                CurrentThread.Abort();
        }
    }
}
