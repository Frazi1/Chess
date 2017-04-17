using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Strategy
{
    public class RealPlayerStrategy : IStrategy
    {
        public RealPlayerStrategy(Piece piece, Cell nextCell)
        {
            Piece = piece;
            NextCell = nextCell;
        }

        public Piece Piece { get; set; }
        public Cell NextCell { get; set; }

        public Tuple<Piece, Cell> PrepareMove()
        {
            return new Tuple<Piece, Cell>(Piece, NextCell);
        }
    }
}
