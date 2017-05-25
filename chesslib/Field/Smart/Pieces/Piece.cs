using System;
using System.Collections.Generic;
using System.Linq;
using chesslib.Field.Bit;
using chesslib.Utils;

namespace chesslib.Field.Smart.Pieces
{
    [Serializable]
    public abstract class Piece
    {
        public Board Board { get; }

        public int PosX
        {
            get { return CurrentCell.PosX; }
        }
        public int PosY
        {
            get { return CurrentCell.PosY; }
        }
        public SmartCell CurrentCell { get; internal set; }
        public PlayerColor PlayerColor { get; }
        public EnumPiece PieceType { get; protected set; }
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
        public List<SmartCell> AllowedCells { get; }
        public List<SmartCell> AttackedCells { get; }

        protected Piece(SmartCell currentCell, PlayerColor playerColor, Board board)
        {
            CurrentCell = currentCell;
            if (CurrentCell.Piece == null)
                CurrentCell.Piece = this;
            PlayerColor = playerColor;
            IsInGame = true;
            Board = board;
            MovesCounter = 0;
            AllowedCells = new List<SmartCell>();
            AttackedCells = new List<SmartCell>();
        }

        public bool CanMoveTo(SmartCell smartCell)
        {
            if (IsInGame)
            {
                var moves = AllowedCells;
                if (moves.Contains(smartCell))
                    return true;
            }
            return false;
        }
        public void MoveTo(SmartCell nextSmartCell)
        {
            CurrentCell.Piece = null;
            CurrentCell = nextSmartCell;
            nextSmartCell.Piece = this;
            ++MovesCounter;
        }

        public void SetAttackedCells()
        {
            AttackedCells.Clear();
            foreach (var smartCell in GetAttackPattern())
            {
                TryAttackCell(smartCell.PosX, smartCell.PosY);
            }
        }
        public void SetAllowedMoves()
        {
            AllowedCells.Clear();

            IEnumerable<SmartCell> e = GetMovePattern();

            foreach (var smartCell in e)
            {
                TryMoveToCell(smartCell.PosX, smartCell.PosY);
            }
        }

        protected void TryMoveToCell(int x, int y)
        {
            Move move = new Move(PosX, PosY, x, y);
            SmartCell smartCell = Board.GetCell(x, y);
            
            if (!BoardUtils.IsCheckOnNextTurn(Board, move, PlayerColor))
                AllowedCells.Add(smartCell);
        }
        protected void TryAttackCell(int x, int y)
        {
            SmartCell smartCell = Board.GetCell(x, y);
            AttackedCells.Add(smartCell);
            smartCell.AttackersList.Add(this);
        }

        public override string ToString()
        {
            return GetType().Name + " - " + PlayerColor;
        }

        public abstract IEnumerable<SmartCell> GetAttackPattern();
        public abstract IEnumerable<SmartCell> GetMovePattern();
    }
}
