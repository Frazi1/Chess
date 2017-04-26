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
    public class Rook : Piece
    {
        public Rook(Cell currentCell, PlayerType playerType, Board board) : base(currentCell, playerType, board)
        {
            PieceType = PieceType.Rook;
        }

        public override bool MoveTo(Cell cell)
        {
            bool moved = base.MoveTo(cell);
            return moved;
        }

        public override void SetAllowedMoves()
        {
            base.SetAllowedMoves();
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            Cell[,] chessBoard = Board.ChessBoard;

            int size = Board.ChessBoard.GetLength(0);

            bool _continue;
            //Вправо
            for (int i = x + 1, j = y; i < size; i++)
            {
                _continue = TryMoveToCell(i, j);
                TryAttackCell(i, j);
                if (!_continue)
                    break;
            }
            //Влево
            for (int i = x - 1, j = y; i >= 0; i--)
            {
                _continue = TryMoveToCell(i, j);
                TryAttackCell(i, j);
                if (!_continue)
                    break;
            }
            //Вниз
            for (int i = x, j = y + 1; j < size; j++)
            {
                _continue = TryMoveToCell(i, j);
                TryAttackCell(i, j);
                if (!_continue)
                    break;
            }
            //Вверх
            for (int i = x, j = y - 1; j >= 0; j--)
            {
                _continue = TryMoveToCell(i, j);
                TryAttackCell(i, j);
                if (!_continue)
                    break;
            }
        }
    }
}
