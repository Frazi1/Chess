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

        public static bool IsCheck(Board board, PlayerColor playerType)
        {
            return board
                    .AlivePieces
                    .Count(p => p.PieceType == PieceType.King && p.PlayerType == playerType && p.IsUnderAttack) > 0;
        }

        public static bool IsCheckMate(Board board, PlayerColor currentPlayerPlayerColor)
        {
            return board.AlivePieces.Where(p => p.PlayerType == currentPlayerPlayerColor)
                .All(p => p.AllowedCells.Count == 0);
        }

        public static bool IsCheckOnNextTurn(Tuple<Piece, Cell> move)
        {
            //Piece oldPiece = move.Item1;
            //Cell oldCell = move.Item2;
            //Board board = oldPiece.Board.DeepCopy();
            //int px = oldPiece.CurrentCell.PosX;
            //int py = oldPiece.CurrentCell.PosY;

            //int cx = oldCell.PosX;
            //int cy = oldCell.PosY;

            ////Piece and cell from copied board:
            //Cell newCell = board.ChessBoard[cx, cy];
            //Piece newPiece = board.ChessBoard[px, py].Piece;

            //board.DestroyPiece(newCell.Piece);
            //newPiece.MoveTo(newCell);
            //board.UpdateAttackedCells();


            var virtualBoard = VirtualMove(move, MoveFlags.UpdateAttacked);
            return IsCheck(virtualBoard, move.Item1.PlayerType);
        }

        public static Board VirtualMove(Tuple<Piece, Cell> move, MoveFlags moveFlags)
        {
            Piece p;
            return VirtualMove(move, moveFlags, out p);
        }

        public static Board VirtualMove(Tuple<Piece, Cell> move, MoveFlags moveFlags, out Piece piece)
        {
            Piece oldPiece = move.Item1;
            Cell oldCell = move.Item2;
            Board board = oldPiece.Board.DeepCopy();
            int px = oldPiece.CurrentCell.PosX;
            int py = oldPiece.CurrentCell.PosY;

            int cx = oldCell.PosX;
            int cy = oldCell.PosY;

            //Piece and cell from copied board:
            Cell newCell = board.ChessBoard[cx, cy];
            Piece newPiece = board.ChessBoard[px, py].Piece;

            board.MovePiece(newPiece, newCell, moveFlags);
            piece = newPiece;
            return board;
        }

    }
}
