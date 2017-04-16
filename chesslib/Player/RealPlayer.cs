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
            _pieces = Board.Instance.AlivePieces
                .Where(p => p.PlayerType == this.PlayerType)
                .ToList();
        }

        private List<Piece> _pieces;

        public PlayerType PlayerType { get; set; }

        public bool MovePiece(Piece piece, Cell nextCell)
        {
            Piece pieceToDestroy = null;
            if (nextCell.Piece != null &&
                nextCell.Piece.PlayerType != PlayerType)
            {
                pieceToDestroy = nextCell.Piece;
            }
            bool canMoveTo = piece.CanMoveTo(nextCell, this);
            if (pieceToDestroy != null && canMoveTo)
            {
                Board.Instance.DestroyPiece(pieceToDestroy);
            }
            bool moved = piece.MoveTo(nextCell, this);

            return moved;
        }
    }
}
