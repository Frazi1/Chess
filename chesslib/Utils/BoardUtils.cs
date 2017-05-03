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

        public static bool IsCheck(Board board, PlayerColor playerType)
        {
            return board
                    .AlivePieces
                    .Where(p => p.PieceType == PieceType.King && p.PlayerType == playerType && p.IsUnderAttack)
                    .Count() > 0;
        }

        //public static bool IsCheckOnNextTurn(Piece piece, Cell nextCell)
        //{
        //    Cell prevCell = piece.CurrentCell;

        //    if (!prevCell.IsAttacked(piece.PlayerType))
        //        return false;

        //    Piece nextPiece = nextCell.Piece;
        //    piece.MoveTo(nextCell);
        //    prevCell
        //        .AttackersList
        //        .Where(a => a.PlayerType != piece.PlayerType)
        //        .ToList()
        //        .ForEach(a => a.SetAllowedMoves());
        //    Piece underCheck = BoardUtils.IsCheck(piece.Board, piece.PlayerType);
        //    piece.MoveTo(prevCell);
        //    piece.MovesCounter -= 2;
        //    nextCell.Piece = nextPiece;
        //    prevCell.AttackersList.ForEach(a => a.SetAllowedMoves());
        //    if (underCheck != null)
        //        return true;

        //    return false;
        //}

        public static bool IsCheckOnNextTurn(Tuple<Piece,Cell> move)
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

            board.DestroyPiece(newCell.Piece);
            newPiece.MoveTo(newCell);
            board.UpdateAttackedCells();

            if (IsCheck(board, newPiece.PlayerType))
                return true;
            return false;

        }
    }
}
