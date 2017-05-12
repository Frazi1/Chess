using System;
using chesslib.Field;

namespace chesslib.Figures
{
    [Serializable]
    public class King : Piece
    {
        public King(Cell currentCell, PlayerColor playerColor, Board board) : base(currentCell, playerColor, board)
        {
            PieceType = PieceType.King;
        }

        public override void SetAllowedMoves()
        {
            base.SetAllowedMoves();
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            Cell[,] chessBoard = Board.ChessBoard;

            int size = Board.ChessBoard.GetLength(0);

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    TryMoveToCell(i, j);
                }
            }
        }

        public override void GetAttackedCells()
        {
            base.GetAttackedCells();
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            Cell[,] chessBoard = Board.ChessBoard;

            int size = Board.ChessBoard.GetLength(0);

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    TryAttackCell(i, j);
                }
            }
        }

    }
}
