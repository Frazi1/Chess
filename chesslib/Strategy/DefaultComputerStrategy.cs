using chesslib.Field;
using chesslib.Player;
using chesslib.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Strategy
{
    public class DefaultComputerStrategy : IStrategy
    {
        //private IPlayer _player;

        //private Cell[,] ChessBoard { get { return _player.Game.Board.ChessBoard; } }
        private List<Piece> AlivePieces { get; set; }

        public DefaultComputerStrategy()
        {
        }

        public Tuple<Piece, Cell> PrepareMove(IPlayer player, Board board)
        {
            AlivePieces = board
                            .AlivePieces
                            .Where(p => p.PlayerType == player.PlayerColor)
                            .ToList();
            Random r = new Random();
            AlivePieces = AlivePieces.OrderBy(p => r.Next()).ToList();
            Piece piece = AlivePieces.First(p => p.AllowedCells.Count > 0);
            Cell cell = piece.AllowedCells
                .OrderBy(p => r.Next()).First();


            return new Tuple<Piece, Cell>(piece, cell);
        }
    }
}
