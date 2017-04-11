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
        public Rook(Cell currentCell, PlayerType playerType) : base(currentCell, playerType)
        {
        }

        public bool HasAlreadyMoved { get; set; } = false;
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
