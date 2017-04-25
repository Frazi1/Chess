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
        public List<Cell> AllowedMoves { get; private set; }

        public Piece(Cell currentCell, PlayerType playerType, Board board)
        {
            CurrentCell = currentCell;
            if (CurrentCell.Piece == null)
                CurrentCell.Piece = this;
            PlayerType = playerType;
            IsInGame = true;
            Board = board;
            MovesCounter = 0;
            AllowedMoves = new List<Cell>();
        }

        public virtual bool CanMoveTo(Cell cell, IPlayer player)
        {
            if (IsInGame)
            {
                if (!CheckPlayer(player))
                    return false;

                var moves = AllowedMoves;
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
            AllowedMoves =new List<Cell>();
        }
        public bool TryMoveToCell(List<Cell> allowedMoves, Cell[,] chessBoard, int x, int y)
        {
            if (BoardUtils.IsValidCell(chessBoard, x, y))
            {
                if (!chessBoard[x, y].IsTaken)
                {
                    allowedMoves.Add(chessBoard[x, y]);
                    return true;
                }
                else if (chessBoard[x, y].IsTaken &&
                    chessBoard[x, y].Piece.PlayerType != PlayerType)
                {
                    allowedMoves.Add(chessBoard[x, y]);
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

        protected bool CheckPlayer(IPlayer player)
        {
            if (player.PlayerType == PlayerType)
                return true;
            return false;
        }

    }
}
