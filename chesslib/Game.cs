using chesslib.Command;
using chesslib.Events;
using chesslib.Field;
using chesslib.Player;
using chesslib.Utils;
using System.Collections.Generic;

namespace chesslib
{
    public class Game
    {
        private const int SIZE = 8;
        private List<MakeMoveCommand> _moveCommands;

        public Board Board { get; private set; }
        public bool IsPaused
        {
            get { return Board.IsPaused; }
            set { Board.IsPaused = value; }
        }
        public bool IsGameFinished { get { return Board.IsGameFinished; } }
        public IPlayer CurrentPlayer { get { return Board.CurrentPlayer; } }
        public List<MakeMoveCommand> MoveCommands
        {
            get
            {
                return _moveCommands;
            }

            set
            {
                _moveCommands = value;
            }
        }
        public GameUtils GameUtils { get; set; }

        public event EventsDelegates.GameStateChangedEventHandler GameStateChanged;

        public Game()
        {
            Board = new Board(SIZE);
            GameUtils = new GameUtils(this);
            MoveCommands = new List<MakeMoveCommand>();
        }

        //public bool MakeMove(Piece piece, Cell nextCell, IPlayer player)
        //{
        //    if (IsPaused)
        //        return false;
        //    if (CurrentPlayer != player)
        //        return false;
        //    GameUtils.SaveState();
        //    DestroyPiece(piece, nextCell);
        //    bool moved = piece.MoveTo(nextCell, player);
        //    if (!moved)
        //        return false;

        //    Update(this);
        //    ChangePlayers();
        //    return true;
        //}


        public bool AddPlayer(IPlayer player)
        {
            bool added = Board.AddPlayer(player);
            if (added)
            {
                player.MoveDone += Player_MoveDone;
                player.Game = this;
            }
            return added;
        }
        public void Start()
        {
            Board.Start();
            RaiseGameStateChange();
        }

        private void Player_MoveDone(object sender, MoveDoneEventArgs e)
        {
            if (e.MoveCommand.CanExecute(this))
            {
                e.MoveCommand.Execute(this);
                GameUtils.SaveState(e.MoveCommand);
                ChangeTurn();
                //RaiseGameStateChange();
            }
        }
        internal void RaiseGameStateChange()
        {
            if (GameStateChanged != null)
                GameStateChanged(this, new GameStateChangedEventArgs(this));
        }

        internal void ChangeTurn()
        {
            Board.ChangeTurn();
            RaiseGameStateChange();
        }

    }
}
