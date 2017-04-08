using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chesslib
{
    public abstract class Piece
    {
        public Cell CurrentCell { get; set; }
        public Color Color { get; private set; }

        public Piece(Cell currentCell, Color color)
        {
            CurrentCell = currentCell;
            Color = color;
        }

        public abstract bool MoveTo(Cell cell);
        public abstract List<Cell> GetAllowedMoves();
    }
}
