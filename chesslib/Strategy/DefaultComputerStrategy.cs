using System;
using System.Collections.Generic;
using System.Linq;
using chesslib.Field;
using chesslib.Field.Smart;
using chesslib.Field.Smart.Pieces;
using chesslib.Player;

namespace chesslib.Strategy
{
    public class DefaultComputerStrategy : IStrategy
    {
        //private IPlayer _player;

        //private Cell[,] ChessBoard { get { return _player.Game.Board.ChessBoard; } }
        private List<Piece> AlivePieces { get; set; }

        public Move PrepareMove(IPlayer player, Board board)
        {
            AlivePieces = board
                            .AlivePieces
                            .Where(p => p.PlayerColor == player.PlayerColor)
                            .ToList();
            Random r = new Random();
            AlivePieces = AlivePieces.OrderBy(p => r.Next()).ToList();
            Piece piece = AlivePieces.First(p => p.AllowedCells.Count > 0);
            SmartCell nextCell = piece.AllowedCells
                .OrderBy(p => r.Next()).First();


            return new Move(piece.PosX,piece.PosY, nextCell.PosX,nextCell.PosY);
        }
    }
}
