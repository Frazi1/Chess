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

        public GameUtils GameUtils { get; private set; }

        public Board Board { get; private set; }
        public bool IsPaused
        {
            get { return Board.IsPaused; }
            set { Board.IsPaused = value; }
        }
        public bool IsGameFinished { get { return Board.IsGameFinished; } }
        public IPlayer CurrentPlayer { get { return Board.CurrentPlayer; } }

        public event EventsDelegates.GameStateChangedEventHandler GameStateChanged;

        public Game()
        {
            Board = new Board(SIZE);
            GameUtils = new GameUtils(this);
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
                _prevMoveCommand = e.MoveCommand;
                GameUtils.SaveState();
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
            Board.ChangeTurn();
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
