using System;
using System.Collections.Generic;
using chesslib.Field;
using chesslib.Utils;

namespace chesslib.Figures
{
    [Serializable]
    public class Queen : Piece
    {
        public Queen(Cell currentCell, PlayerColor playerColor, Board board) : base(currentCell, playerColor, board)
        {
            PieceType = PieceType.Queen;
        }

        public override IEnumerable<Cell> GetAttackPattern()
        {
            return GetMovePattern();
        }

        public override IEnumerable<Cell> GetMovePattern()
        {
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            int size = Board.ChessBoard.GetLength(0);

            Cell cell;
            //Вправо вверх
            for (int i = x + 1, j = y - 1; i < size && j >= 0; i++, j--)
            {
                if (BoardUtils.IsValidCell(Board.ChessBoard, i, j))
                {
                    if (i == PosX && j == PosX)
                        continue;

                    cell = Board.GetCell(i, j);
                    yield return cell;
                    if (!BoardUtils.Continue(cell))
                        break;
                }
            }
            //Влево вверх
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (BoardUtils.IsValidCell(Board.ChessBoard, i, j))
                {
                    if (i == PosX && j == PosX)
                        continue;

                    cell = Board.GetCell(i, j);
                    yield return cell;
                    if (!BoardUtils.Continue(cell))
                        break;
                }
            }

            //Вправо вниз
            for (int i = x + 1, j = y + 1; i < size && j < size; i++, j++)
            {
                if (BoardUtils.IsValidCell(Board.ChessBoard, i, j))
                {
                    if (i == PosX && j == PosX)
                        continue;

                    cell = Board.GetCell(i, j);
                    yield return cell;
                    if (!BoardUtils.Continue(cell))
                        break;
                }
            }

            //Влево вниз
            for (int i = x - 1, j = y + 1; i >= 0 && j < size; i--, j++)
            {
                if (BoardUtils.IsValidCell(Board.ChessBoard, i, j))
                {
                    if (i == PosX && j == PosX)
                        continue;

                    cell = Board.GetCell(i, j);
                    yield return cell;
                    if (!BoardUtils.Continue(cell))
                        break;
                }
            }

            //Вправо
            for (int i = x + 1, j = y; i < size; i++)
            {
                if (BoardUtils.IsValidCell(Board.ChessBoard, i, j))
                {
                    if (i == PosX && j == PosX)
                        continue;

                    cell = Board.GetCell(i, j);
                    yield return cell;
                    if (!BoardUtils.Continue(cell))
                        break;
                }
            }
            //Влево
            for (int i = x - 1, j = y; i >= 0; i--)
            {
                if (BoardUtils.IsValidCell(Board.ChessBoard, i, j))
                {
                    if (i == PosX && j == PosX)
                        continue;

                    cell = Board.GetCell(i, j);
                    yield return cell;
                    if (!BoardUtils.Continue(cell))
                        break;
                }
            }
            //Вниз
            for (int i = x, j = y + 1; j < size; j++)
            {
                if (BoardUtils.IsValidCell(Board.ChessBoard, i, j))
                {
                    if (i == PosX && j == PosX)
                        continue;

                    cell = Board.GetCell(i, j);
                    yield return cell;
                    if (!BoardUtils.Continue(cell))
                        break;
                }
            }
            //Вверх
            for (int i = x, j = y - 1; j >= 0; j--)
            {
                if (BoardUtils.IsValidCell(Board.ChessBoard, i, j))
                {
                    if (i == PosX && j == PosX)
                        continue;

                    cell = Board.GetCell(i, j);
                    yield return cell;
                    if (!BoardUtils.Continue(cell))
                        break;
                }
            }
        }
    }
}
