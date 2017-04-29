using chesslib.Command;
using chesslib.Events;
using chesslib.Field;
using chesslib.Memento;
using chesslib.Player;
using chesslib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace chesslib
{
    public class Game : IOriginator<MakeMoveCommand>
    {
        private const int SIZE = 8;
        private MakeMoveCommand _prevMoveCommand;
        private IPlayer _currentPlayer;

        public GameUtils GameUtils { get; private set; }

        public Board Board { get; private set; }
        public bool IsPaused
        {
            get { return Board.IsPaused; }
            set { Board.IsPaused = value; }
        }
        public bool IsGameFinished { get { return Board.IsGameFinished; } }
        public List<IPlayer> Players { get; private set; }
        public IPlayer CurrentPlayer
        {
            get { return _currentPlayer; }
            private set
            {
                if (_currentPlayer != value)
                {
                    _currentPlayer = value;
                }
            }
        }
        public event EventsDelegates.GameStateChangedEventHandler GameStateChanged;

        public Game()
        {
            Board = new Board(SIZE);
            GameUtils = new GameUtils(this);
            Players = new List<IPlayer>();
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

        public void LoadPreviousState()
        {
            CurrentPlayer.CancelTurn();
            IsPaused = true;
            GameUtils.LoadPreviousState();
            RaiseGameStateChange();
        }
        public bool AddPlayer(IPlayer player)
        {
            if (Players.Count < 2 && !Players.Contains(player))
            {
                player.MoveDone += Player_MoveDone;
                player.Game = this;
                Players.Add(player);
                return true;
            }
            return false;
        }
        public void Start()
        {
            Board.Start();
            if (CurrentPlayer == null)
                CurrentPlayer = Players.First(p => p.PlayerType == PlayerType.White);
            if (!IsPaused && !IsGameFinished)
                CurrentPlayer.DoTurn();
            RaiseGameStateChange();
        }

        private void Player_MoveDone(object sender, MoveDoneEventArgs e)
        {
            if (e.MoveCommand.CanExecute(this))
            {
                e.MoveCommand.Execute(this);
                _prevMoveCommand = e.MoveCommand;
                GameUtils.SaveState();
                Board.UpdatePiecesAndCells();
                ChangeTurn();
                //RaiseGameStateChange();
            }
        }
        private void RaiseGameStateChange()
        {
            if (GameStateChanged != null)
                GameStateChanged(this, new GameStateChangedEventArgs(this));
        }


        internal void ChangeTurn()
        {
            if (CurrentPlayer == Players[0])
                CurrentPlayer = Players[1];
            else
                CurrentPlayer = Players[0];
            CurrentPlayer.DoTurn();
            RaiseGameStateChange();

        }

        #region Memento
        public Memento<MakeMoveCommand> GetMemento()
        {
            return new Memento<MakeMoveCommand>(_prevMoveCommand);
        }

        public void SetMemento(Memento<MakeMoveCommand> value)
        {
            value.GetState().Undo(this);
            //_prevMoveCommand = null;
        }
        #endregion
    }
}
