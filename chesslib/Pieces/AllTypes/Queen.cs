using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using chesslib.Player;

namespace chesslib.Figures
{
    public class Queen : Piece
    {
        public Queen(Cell currentCell, PlayerType playerType) : base(currentCell, playerType)
        {
        }

        public override List<Cell> GetAllowedMoves()
        {
            throw new NotImplementedException();
        }

        public override bool MoveTo(Cell cell, IPlayer player)
        {
            return base.MoveTo(cell, player);
        }
    }
}
