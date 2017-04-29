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
    public class Bishop : Piece
    {
        public Bishop(Cell currentCell, PlayerType playerType, Board board) : base(currentCell, playerType, board)
        {
            PieceType = PieceType.Bishop;
        }

        public override void SetAllowedMoves()
        {
            base.SetAllowedMoves();
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            Cell[,] chessBoard = Board.ChessBoard;

            int size = Board.ChessBoard.GetLength(0);

            bool _continue;
            //Вправо вверх
            for (int i = x + 1, j = y - 1; i < size && j >= 0; i++, j--)
            {
                _continue = TryMoveToCell(i, j);
                if (!_continue)
                    break;
            }
            //Влево вверх
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                _continue = TryMoveToCell(i, j);
                if (!_continue)
                    break;
            }

            //Вправо вниз
            for (int i = x + 1, j = y + 1; i < size && j < size; i++, j++)
            {
                _continue = TryMoveToCell(i, j);
                if (!_continue)
                    break;
            }

            //Влево вниз
            for (int i = x - 1, j = y + 1; i >= 0 && j < size; i--, j++)
            {
                _continue = TryMoveToCell(i, j);
                if (!_continue)
                    break;
            }
        }

        public override void GetAttackedCells()
        {
            base.GetAttackedCells();
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            Cell[,] chessBoard = Board.ChessBoard;

            int size = Board.ChessBoard.GetLength(0);

            bool _continue;
            //Вправо вверх
            for (int i = x + 1, j = y - 1; i < size && j >= 0; i++, j--)
            {
                _continue = TryAttackCell(i, j);
                if (!_continue)
                    break;
            }
            //Влево вверх
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                _continue = TryAttackCell(i, j);
                if (!_continue)
                    break;
            }

            //Вправо вниз
            for (int i = x + 1, j = y + 1; i < size && j < size; i++, j++)
            {
                _continue = TryAttackCell(i, j);
                if (!_continue)
                    break;
            }

            //Влево вниз
            for (int i = x - 1, j = y + 1; i >= 0 && j < size; i--, j++)
            {
                _continue = TryAttackCell(i, j);
                if (!_continue)
                    break;
            }
        }
    }
}
