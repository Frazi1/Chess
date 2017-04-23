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
    [Serializable]
    public abstract class Piece
    {
        protected Board Board { get; private set; }

        public Cell CurrentCell { get; set; }
        public PlayerType PlayerType { get; set; }
        public bool IsInGame { get; set; }
        public bool IsAttacked
        {
            get { return CurrentCell.IsAttacked; }
        }
        public int MovesCounter { get; set; }


        public Piece(Cell currentCell, PlayerType playerType, Board board)
        {
            CurrentCell = currentCell;
            if (CurrentCell.Piece == null)
                CurrentCell.Piece = this;
            PlayerType = playerType;
            IsInGame = true;
            Board = board;
            MovesCounter = 0;
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
                ++MovesCounter;
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
        public abstract List<Cell> GetAllowedMoves();
        public virtual List<Cell> GetAttackedCells()
        {
            return GetAllowedMoves();
        }
        public bool TryMoveToCell(List<Cell> allowedMoves, Cell[,] chessBoard, int x1, int y1)
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
        public bool TryAttackCell(Cell[,] chessBoard, int x, int y)
        {
            if (x >= 0 &&
                x < chessBoard.GetLength(0) &&
                y >= 0 &&
                y < chessBoard.GetLength(0))
            {
                return true;
            }
            return false;

        }

        public override string ToString()
        {
            return GetType().Name + " - " + PlayerType.ToString();
        }
    }
}
