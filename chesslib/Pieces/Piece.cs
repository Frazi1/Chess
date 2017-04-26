using chesslib.Field;
using chesslib.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using chesslib.Utils;

namespace chesslib
{
    [Serializable]
    public abstract class Piece
    {
        protected Board Board { get; private set; }

        public Cell CurrentCell { get; internal set; }
        public PlayerType PlayerType { get; internal set; }
        public PieceType PieceType { get; protected set; }
        public int MovesCounter { get; internal set; }
        public bool IsInGame { get; internal set; }
        public bool IsUnderAttack
        {
            get
            {
                return CurrentCell.AttackersList
                                  .Where(a => a.PlayerType != PlayerType)
                                  .Count() > 0;
            }
        }
        public bool HasAlreadyMoved { get { return MovesCounter > 0; } }
        public List<Cell> AllowedCells { get; protected set; }
        public List<Cell> AttackedCells { get; protected set; }


        public Piece(Cell currentCell, PlayerType playerType, Board board)
        {
            CurrentCell = currentCell;
            if (CurrentCell.Piece == null)
                CurrentCell.Piece = this;
            PlayerType = playerType;
            IsInGame = true;
            Board = board;
            MovesCounter = 0;
            AllowedCells = new List<Cell>();
            AttackedCells = new List<Cell>();
        }

        public virtual bool CanMoveTo(Cell cell, IPlayer player)
        {
            if (IsInGame)
            {
                if (!CheckPlayer(player))
                    return false;

                var moves = AllowedCells;
                if (moves.Contains(cell))
                    return true;
            }
            return false;
        }
        public virtual bool MoveTo(Cell nextCell, IPlayer player)
        {
            CurrentCell.Piece = null;
            CurrentCell = nextCell;
            nextCell.Piece = this;
            ++MovesCounter;
            return true;
        }

        public virtual void SetAllowedMoves()
        {
            AllowedCells.Clear();
            AttackedCells.Clear();
        }
        /// <summary>
        /// If cell at x,y is free, it will be added to allowedMoves list and TRUE will be returned
        /// If cell is occupied by an enemy piece, it will be added to allowedMoves and FALSE will be returned
        /// Otherwise, FALSE returned
        /// </summary>
        /// <param name="allowedMoves"></param>
        /// <param name="chessBoard"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>True or fasle</returns>
        public virtual bool TryMoveToCell(int x, int y)
        {
            var chessBoard = Board.ChessBoard;
            if (BoardUtils.IsValidCell(chessBoard, x, y))
            {
                if (!chessBoard[x, y].IsTaken)
                {
                    AllowedCells.Add(chessBoard[x, y]);
                    return true;
                }
                else if (chessBoard[x, y].IsTaken &&
                    chessBoard[x, y].Piece.PlayerType != PlayerType)
                {
                    AllowedCells.Add(chessBoard[x, y]);
                    return false;
                }
            }
            return false;
        }

        public virtual bool TryAttackCell(int x, int y)
        {
            var chessBoard = Board.ChessBoard;
            if (BoardUtils.IsValidCell(chessBoard, x, y))
            {
                if (!chessBoard[x, y].IsTaken)
                {
                    AttackedCells.Add(chessBoard[x, y]);
                    return true;
                }
                else if (chessBoard[x, y].IsTaken)
                {
                    AttackedCells.Add(chessBoard[x, y]);
                    return false;
                }
            }
            return false;

        }

        public override string ToString()
        {
            return GetType().Name + " - " + PlayerType.ToString();
        }

        protected bool CheckPlayer(IPlayer player)
        {
            if (player.PlayerType == PlayerType)
                return true;
            return false;
        }

    }
}
