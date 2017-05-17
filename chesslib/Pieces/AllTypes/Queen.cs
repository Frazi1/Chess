using System;
using System.Collections.Generic;
using chesslib.Board;
using chesslib.Utils;

namespace chesslib.Figures
{
    [Serializable]
    public class Queen : Piece
    {
        public Queen(Cell currentCell, PlayerColor playerColor, Board.Board board) : base(currentCell, playerColor, board)
        {
            PieceType = PieceType.Queen;
        }

        public override IEnumerable<Cell> GetAttackPattern()
        {
            return GetPattern((p, c) => true);
        }

        public override IEnumerable<Cell> GetMovePattern()
        {
            return GetPattern(BoardUtils.PieceCanMoveTo);
        }

        private IEnumerable<Cell> GetPattern(Func<Piece, Cell, bool> checker)
        {
            int x = PosX;
            int y = PosY;
            int size = Board.Size;

            Cell cell;
            //Вправо вверх
            for (int i = x + 1, j = y - 1; i < size && j >= 0; i++, j--)
            {
                cell = Board.GetCell(i, j);
                if (checker(this,cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }
            //Влево вверх
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                cell = Board.GetCell(i, j);
                if (checker(this,cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }

            //Вправо вниз
            for (int i = x + 1, j = y + 1; i < size && j < size; i++, j++)
            {
                cell = Board.GetCell(i, j);
                if (checker(this,cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }

            //Влево вниз
            for (int i = x - 1, j = y + 1; i >= 0 && j < size; i--, j++)
            {
                cell = Board.GetCell(i, j);
                if (checker(this,cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }

            //Вправо
            for (int i = x + 1, j = y; i < size; i++)
            {
                cell = Board.GetCell(i, j);
                if (checker(this,cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }
            //Влево
            for (int i = x - 1, j = y; i >= 0; i--)
            {
                cell = Board.GetCell(i, j);
                if (checker(this,cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }
            //Вниз
            for (int i = x, j = y + 1; j < size; j++)
            {
                cell = Board.GetCell(i, j);
                if (checker(this,cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }
            //Вверх
            for (int i = x, j = y - 1; j >= 0; j--)
            {
                cell = Board.GetCell(i, j);
                if (checker(this,cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }

        }
    }
}
