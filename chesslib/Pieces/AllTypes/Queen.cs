using System;
using chesslib.Field;

namespace chesslib.Figures
{
    [Serializable]
    public class Queen : Piece
    {
        public Queen(Cell currentCell, PlayerColor playerColor, Board board) : base(currentCell, playerColor, board)
        {
            PieceType = PieceType.Queen;
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
                if (!_continue)
                    break;
            }
            //Влево
            for (int i = x - 1, j = y; i >= 0; i--)
            {
                _continue = TryMoveToCell(i, j);
                if (!_continue)
                    break;
            }
            //Вниз
            for (int i = x, j = y + 1; j < size; j++)
            {
                _continue = TryMoveToCell(i, j);
                if (!_continue)
                    break;
            }
            //Вверх
            for (int i = x, j = y - 1; j >= 0; j--)
            {
                _continue = TryMoveToCell(i, j);
                if (!_continue)
                    break;
            }
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
            //Вправо
            for (int i = x + 1, j = y; i < size; i++)
            {
                _continue = TryAttackCell(i, j);
                if (!_continue)
                    break;
            }
            //Влево
            for (int i = x - 1, j = y; i >= 0; i--)
            {
                _continue = TryAttackCell(i, j);
                if (!_continue)
                    break;
            }
            //Вниз
            for (int i = x, j = y + 1; j < size; j++)
            {
                _continue = TryAttackCell(i, j);
                if (!_continue)
                    break;
            }
            //Вверх
            for (int i = x, j = y - 1; j >= 0; j--)
            {
                _continue = TryAttackCell(i, j);
                if (!_continue)
                    break;
            }
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
