using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chesslib
{
    public abstract class Piece
    {
        public Cell CurrentCell { get; set; }
        public PlayerType PlayerType { get; set; }
        public PieceType PieceType { get; protected set; }

        public Piece(Cell currentCell, PlayerType playerType)
        {
            CurrentCell = currentCell;
            PlayerType = playerType;
        }

        public abstract bool MoveTo(Cell cell);
        public abstract List<Cell> GetAllowedMoves();
    }
}
