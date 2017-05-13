using System;
using System.Collections.Generic;
using chesslib.Field;
using chesslib.Utils;

namespace chesslib.Figures
{
    [Serializable]
    public class Rook : Piece
    {
        public Rook(Cell currentCell, PlayerColor playerColor, Board board) : base(currentCell, playerColor, board)
        {
            PieceType = PieceType.Rook;
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
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            int size = Board.ChessBoard.GetLength(0);

            Cell cell;
            
            //Вправо
            for (int i = x + 1, j = y; i < size; i++)
            {
                cell = Board.GetCell(i, j);
                if (checker(this, cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }
            //Влево
            for (int i = x - 1, j = y; i >= 0; i--)
            {
                cell = Board.GetCell(i, j);
                if (checker(this, cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }
            //Вниз
            for (int i = x, j = y + 1; j < size; j++)
            {
                cell = Board.GetCell(i, j);
                if (checker(this, cell))
                    yield return cell;
                if (!BoardUtils.Continue(cell))
                    break;
            }
            //Вверх
            for (int i = x, j = y - 1; j >= 0; j--)
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
