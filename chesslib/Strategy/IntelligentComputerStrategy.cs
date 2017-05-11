using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using chesslib.Field;
using chesslib.Player;

namespace chesslib.Strategy
{
    public class IntelligentComputerStrategy : IStrategy
    {
        private List<Piece> alivePieces;
        private int steps;

        public IntelligentComputerStrategy(int steps)
        {
            this.steps = steps;
        }

        public Tuple<Cell, Cell> PrepareMove(IPlayer player, Board board)
        {
            alivePieces = board.GetAlivePieces(player.PlayerColor);

            var estimadeMoves = new List<Tuple<Tuple<Piece, Cell>, double>>();

            return null;
        }

        private double EstimateMove(Tuple<Piece,Cell> move, int steps)
        {
            double estimation = 0;

        }
    }
}
