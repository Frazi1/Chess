using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using chesslib.Board;
using chesslib.Utils;

namespace chesslib
{
    [Serializable]
    public abstract class Piece : IMovable
    {
        public Board.Board Board { get; private set; }

        public int PosX
        {
            get { return CurrentCell.PosX; }
        }
        public int PosY
        {
            get { return CurrentCell.PosY; }
        }
        public Cell CurrentCell { get; internal set; }
        public PlayerColor PlayerColor { get; internal set; }
        public PieceType PieceType { get; protected set; }
        public int MovesCounter { get; internal set; }
        public bool IsInGame { get; internal set; }
        public bool IsUnderAttack
        {
            get
            {
                return CurrentCell.AttackersList
                                  .Count(a => a.PlayerColor != PlayerColor) > 0;
            }
        }
        public bool IsUnderProtect
        {
            get
            {
                return CurrentCell.
                  AttackersList.Count(p => p.PlayerColor == PlayerColor) > 0;
            }
        }
        public bool HasAlreadyMoved { get { return MovesCounter > 0; } }
        public List<Cell> AllowedCells { get; protected set; }
        public List<Cell> AttackedCells { get; protected set; }

        protected Piece(Cell currentCell, PlayerColor playerColor, Board.Board board)
        {
            CurrentCell = currentCell;
            if (CurrentCell.Piece == null)
                CurrentCell.Piece = this;
            PlayerColor = playerColor;
            IsInGame = true;
            Board = board;
            MovesCounter = 0;
            AllowedCells = new List<Cell>();
            AttackedCells = new List<Cell>();
        }

        public bool CanMoveTo(Cell cell)
        {
            if (IsInGame)
            {
                var moves = AllowedCells;
                if (moves.Contains(cell))
                    return true;
            }
            return false;
        }
        public bool MoveTo(Cell nextCell)
        {
            CurrentCell.Piece = null;
            CurrentCell = nextCell;
            nextCell.Piece = this;
            ++MovesCounter;
            return true;
        }

        public virtual void SetAttackedCells()
        {
            AttackedCells.Clear();
            foreach (var cell in GetAttackPattern())
            {
                TryAttackCell(cell.PosX, cell.PosY);
            }
        }
        public virtual void SetAllowedMoves()
        {
            AllowedCells.Clear();

            IEnumerable<Cell> e = GetMovePattern();

            foreach (var cell in e)
            {
                TryMoveToCell(cell.PosX, cell.PosY);
            }
        }

        protected void TryMoveToCell(int x, int y)
        {
            Move move = new Move(PosX, PosY, x, y);
            Cell cell = Board.GetCell(x, y);
            
            if (!BoardUtils.IsCheckOnNextTurn(Board, move, PlayerColor))
                AllowedCells.Add(cell);
        }
        protected void TryAttackCell(int x, int y)
        {
            Cell cell = Board.GetCell(x, y);
            AttackedCells.Add(cell);
            cell.AttackersList.Add(this);
        }

        public override string ToString()
        {
            return GetType().Name + " - " + PlayerColor.ToString();
        }

        public abstract IEnumerable<Cell> GetAttackPattern();
        public abstract IEnumerable<Cell> GetMovePattern();
    }
}
