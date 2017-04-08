using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chesslib.Figures
{
    public class Knight : Piece
    {
        public Knight(Cell currentCell, Color color) : base(currentCell, color)
        {
        }

        public override List<Cell> GetAllowedMoves()
        {
            throw new NotImplementedException();
        }

        public override bool MoveTo(Cell cell)
        {
            throw new NotImplementedException();
        }
    }
}
