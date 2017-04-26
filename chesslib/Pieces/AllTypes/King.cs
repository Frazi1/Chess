using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using chesslib.Player;
using chesslib.Field;

namespace chesslib.Figures
{
    [Serializable]
    public class King : Piece
    {
        public King(Cell currentCell, PlayerType playerType, Board board) : base(currentCell, playerType, board)
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
                    TryAttackCell(i, j);
                }
            }
        }

        public override bool MoveTo(Cell cell)
        {
            return base.MoveTo(cell);
        }

        public override bool CanMoveTo(Cell cell)
        {
            return base.CanMoveTo(cell) && !cell.IsAttacked(this.PlayerType);
        }
    }
}
