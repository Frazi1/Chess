using chesslib.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Player
{
    public class RealPlayer : IPlayer
    {
        public RealPlayer(PlayerType playerType)
        {
            PlayerType = playerType;
            _pieces = Board.Instance.Pieces
                .Where(p => p.PlayerType == this.PlayerType)
                .ToList();
        }

        private List<Piece> _pieces;

        public PlayerType PlayerType { get; set; }

        public void MovePiece(Piece piece, Cell nextCell)
        {
            piece.MoveTo(nextCell);
        }
    }
}
