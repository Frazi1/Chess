using System.Drawing;
using System.Linq;
using chesslib.Field;
using chesslib.Field.Bit;
using chesslib.Field.Smart;
using chesslib.Field.Smart.Pieces;

namespace chesslib.Utils
{
    public static class BoardUtils
    {
        private static int size = 8;
        public static bool IsValidCell(int x, int y)
        {
            return x >= 0 &&
                            x < size &&
                            y >= 0 &&
                            y < size;
        }

        public static bool PieceCanMoveTo(Piece piece, SmartCell smartCell)
        {
            return !smartCell.IsTaken || smartCell.IsTaken && smartCell.Piece.PlayerColor != piece.PlayerColor;
        }

        public static bool Continue(SmartCell smartCell)
        {
            return !smartCell.IsTaken;
        }

        public static bool IsCheck(Board board, PlayerColor playerColor)
        {
            EnumPiece king = playerColor == PlayerColor.Black ? EnumPiece.BKing : EnumPiece.WKing;
            return board
                    .AlivePieces
                    .Count(p => p.PieceType == king && p.IsUnderAttack) > 0;
        }

        public static bool IsCheckMate(Board board, PlayerColor currentPlayerPlayerColor)
        {
            return board.AlivePieces
                .Where(p => p.PlayerColor == currentPlayerPlayerColor)
                .All(p => p.AllowedCells.Count == 0);
        }

        public static bool IsCheckOnNextTurn(Board currentBoard, Move move, PlayerColor playerColor)
        {
            var virtualBoard = VirtualMove(currentBoard, true, move, MoveFlags.UpdateAttacked);
            return IsCheck(virtualBoard, playerColor);
        }

        public static Board VirtualMove(Board currentBoard, bool copy, Move move, MoveFlags moveFlags)
        {
            Piece movedPiece, destroyedPiece;
            return VirtualMove(currentBoard, copy, move, moveFlags, out movedPiece, out destroyedPiece);
        }

        public static Board VirtualMove(Board currentBoard, bool copy, Move move, MoveFlags moveFlags, out Piece movedPiece, out Piece destroyedPiece)
        {
            Board board = currentBoard;
            if (copy)
                board = currentBoard.DeepCopy();

            destroyedPiece = board.MovePiece(move, moveFlags);
            movedPiece = board.GetPiece(move.ToX, move.ToY);
            return board;
        }
    }
}
