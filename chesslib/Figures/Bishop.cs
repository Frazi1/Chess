using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chesslib.Figures
{
    public class Bishop : Piece
    {
        public Bishop(Cell currentCell, Color color) : base(currentCell, color)
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
