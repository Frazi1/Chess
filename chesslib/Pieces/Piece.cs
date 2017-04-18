using chesslib.Field;
using chesslib.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chesslib
{
    public abstract class Piece : IObservable<Piece>
    {
        public Cell CurrentCell { get; set; }
        public PlayerType PlayerType { get; set; }
        public bool IsInGame { get; set; }

        public Piece(Cell currentCell, PlayerType playerType)
        {
            CurrentCell = currentCell;
            if (CurrentCell.Piece == null)
                CurrentCell.Piece = this;
            PlayerType = playerType;
            _observers = new List<IObserver<Piece>>();
            IsInGame = true;
        }

        protected int Direction
        {
            get
            {
                if (PlayerType == PlayerType.White)
                    return 1;
                return -1;
            }
        }
        public virtual bool CanMoveTo(Cell cell, IPlayer player)
        {
            if (IsInGame)
            {
                if (!CheckPlayer(player))
                    return false;

                var moves = GetAllowedMoves();
                if (moves.Contains(cell))
                    return true;
            }
            return false;
        }

        public virtual bool MoveTo(Cell nextCell, IPlayer player)
        {
            if (CanMoveTo(nextCell, player))
            {
                CurrentCell.Piece = null;
                CurrentCell = nextCell;
                nextCell.Piece = this;
                Update(this);
                return true;
            }
            return false;
        }

        protected bool CheckPlayer(IPlayer player)
        {
            if (player.PlayerType == PlayerType)
                return true;
            return false;
        }
        public void Destroy()
        {
            IsInGame = false;
            CurrentCell = null;
            //EndUpdates();
            Update(this);
        }
        public abstract List<Cell> GetAllowedMoves();

        public bool TryCell(List<Cell> allowedMoves, Cell[,] chessBoard, int x1, int y1)
        {
            if (x1 >= 0 &&
                x1 < chessBoard.GetLength(0) &&
                y1 >= 0 &&
                y1 < chessBoard.GetLength(0))
            {
                if (!chessBoard[x1, y1].IsTaken)
                {
                    allowedMoves.Add(chessBoard[x1, y1]);
                    return true;
                }
                else if (chessBoard[x1, y1].IsTaken &&
                    chessBoard[x1, y1].Piece.PlayerType != PlayerType)
                {
                    allowedMoves.Add(chessBoard[x1, y1]);
                    return false;
                }
            }
            return false;
        }

        #region IObservable

        private List<IObserver<Piece>> _observers;
        public IDisposable Subscribe(IObserver<Piece> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber<Piece>(_observers, observer);
        }

        public void Update(Piece loc)
        {
            foreach (var observer in _observers)
            {
                if (loc == null)
                    observer.OnError(new Exception("Null piece"));
                else
                    observer.OnNext(loc);
            }
        }

        public void EndUpdates()
        {
            foreach (var observer in _observers)
            {
                if (_observers.Contains(observer))
                    observer.OnCompleted();

                _observers.Clear();
            }
        }
        #endregion
    }
}
