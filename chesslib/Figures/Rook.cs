using chesslib.Figures.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chesslib.Figures
{
    public class Rook : Piece, IMoved
    {
        public bool HasAlreadyMoved { get; set; } = false;

        public Rook(Cell currentCell, Color color) : base(currentCell, color)
        {
        }

        public override bool MoveTo(Cell cell)
        {
            throw new NotImplementedException();
        }

        public override List<Cell> GetAllowedMoves()
        {
            throw new NotImplementedException();
        }
    }
}
