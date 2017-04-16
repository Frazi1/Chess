using chesslib.Figures.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using chesslib.Player;

namespace chesslib.Figures
{
    public class King : Piece, IMoved
    {
        public King(Cell currentCell, PlayerType playerType) : base(currentCell, playerType)
        {
        }

        public bool HasAlreadyMoved { get; set; } = false;

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
