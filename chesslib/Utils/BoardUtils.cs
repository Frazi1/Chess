using chesslib.Field;
using chesslib.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Utils
{
    public static class BoardUtils
    {
        public static bool IsValidCell(Cell[,] chessBoard, int x, int y)
        {
            return x >= 0 &&
                            x < chessBoard.GetLength(0) &&
                            y >= 0 &&
                            y < chessBoard.GetLength(0);
        }

        public static Piece IsCheck(Board board, PlayerType playerType)
        {
            return board
                    .AlivePieces
                    .FirstOrDefault(p => p.PieceType == PieceType.King && p.PlayerType == playerType && p.IsUnderAttack);
        }

        public static bool IsCheckOnNextTurn(Piece piece, Cell nextCell)
        {
            Cell prevCell = piece.CurrentCell;

            if (!prevCell.IsAttacked(piece.PlayerType))
                return false;

            Piece nextPiece = nextCell.Piece;
            piece.MoveTo(nextCell);
            prevCell
                .AttackersList
                .Where(a => a.PlayerType != piece.PlayerType)
                .ToList()
                .ForEach(a => a.SetAllowedMoves());
            Piece underCheck = BoardUtils.IsCheck(piece.Board, piece.PlayerType);
            piece.MoveTo(prevCell);
            piece.MovesCounter -= 2;
            nextCell.Piece = nextPiece;
            prevCell.AttackersList.ForEach(a => a.SetAllowedMoves());
            if (underCheck != null)
                return true;

            return false;
        }
    }
}
