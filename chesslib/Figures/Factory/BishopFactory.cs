using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chesslib.Figures.Factory
{
    public class BishopFactory : FigureFactory
    {
        public override Piece GetFigure(Cell cell, Color color)
        {
            return new Bishop(cell, color);
        }
    }
}
