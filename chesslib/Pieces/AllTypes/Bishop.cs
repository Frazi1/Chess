using System;
using System.Collections.Generic;
using chesslib.Field;
using chesslib.Utils;

namespace chesslib.Figures
{
    [Serializable]
    public class Bishop : Piece
    {
        public Bishop(Cell currentCell, PlayerColor playerColor, Board board) : base(currentCell, playerColor, board)
        {
            PieceType = PieceType.Bishop;
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
                if (checker(this, cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }
            //Влево вверх
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                cell = Board.GetCell(i, j);
                if (checker(this, cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }

            //Вправо вниз
            for (int i = x + 1, j = y + 1; i < size && j < size; i++, j++)
            {
                cell = Board.GetCell(i, j);
                if (checker(this, cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }

            //Влево вниз
            for (int i = x - 1, j = y + 1; i >= 0 && j < size; i--, j++)
            {
                cell = Board.GetCell(i, j);
                if (checker(this, cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }
        }
    }
}
