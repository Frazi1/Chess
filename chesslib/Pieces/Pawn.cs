using chesslib.Figures.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chesslib.Figures
{
    public class Pawn : Piece, IMoved
    {
        public Pawn(Cell currentCell, PlayerType playerType) : base(currentCell, playerType)
        {

        }

        public bool HasAlreadyMoved { get; set; } = false;
        public bool IsPromoted { get; set; } = false;

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
