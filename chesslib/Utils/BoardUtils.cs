using chesslib.Field;
using System;
using System.Linq;

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

        public static bool Continue(Cell cell)
        {
            return !cell.IsTaken;
        }

        public static bool IsCheck(Board board, PlayerColor playerType)
        {
            return board
                    .AlivePieces
                    .Count(p => p.PieceType == PieceType.King && p.PlayerColor == playerType && p.IsUnderAttack) > 0;
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
