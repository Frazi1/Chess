using System;
using System.Collections.Generic;
using chesslib.Field.Bit;
using chesslib.Utils;

namespace chesslib.Field.Smart.Pieces.AllTypes
{
    [Serializable]
    public class Rook : Piece
    {
        public Rook(SmartCell  currentCell, PlayerColor playerColor, Board board) : base(currentCell, playerColor, board)
        {
            PieceType = playerColor == PlayerColor.Black ? EnumPiece.BRook : EnumPiece.WRook;
        }

        public override IEnumerable<SmartCell > GetAttackPattern()
        {
            return GetPattern((p, c) => true);
        }

        public override IEnumerable<SmartCell > GetMovePattern()
        {
            return GetPattern(BoardUtils.PieceCanMoveTo);
        }

        private IEnumerable<SmartCell > GetPattern(Func<Piece, SmartCell , bool> checker)
        {
            int x = PosX;
            int y = PosY;
            int size = Board.Size;

            SmartCell  cell;
            
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
