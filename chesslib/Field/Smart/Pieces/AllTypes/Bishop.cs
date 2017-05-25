using System;
using System.Collections.Generic;
using chesslib.Field;
using chesslib.Field.Bit;
using chesslib.Field.Smart;
using chesslib.Field.Smart.Pieces;
using chesslib.Utils;

namespace chesslib.Figures
{
    [Serializable]
    public class Bishop : Piece
    {
        public Bishop(SmartCell currentCell, PlayerColor playerColor, Board board) : base(currentCell, playerColor, board)
        {
            PieceType = playerColor == PlayerColor.Black ? EnumPiece.BBishop : EnumPiece.WBishop;
        }


        public override IEnumerable<SmartCell> GetAttackPattern()
        {
            return GetPattern((p, c) => true);
        }

        public override IEnumerable<SmartCell> GetMovePattern()
        {
            return GetPattern(BoardUtils.PieceCanMoveTo);
        }

        private IEnumerable<SmartCell> GetPattern(Func<Piece, SmartCell, bool> checker)
        {
            int x = PosX;
            int y = PosY;
            int size = Board.Size;

            SmartCell smartCell;
            //Вправо вверх
            for (int i = x + 1, j = y - 1; i < size && j >= 0; i++, j--)
            {
                smartCell = Board.GetCell(i, j);
                if (checker(this, smartCell))
                    yield return smartCell;
                if (!BoardUtils.Continue(smartCell))
                    break;
            }
            //Влево вверх
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                smartCell = Board.GetCell(i, j);
                if (checker(this, smartCell))
                    yield return smartCell;
                if (!BoardUtils.Continue(smartCell))
                    break;
            }

            //Вправо вниз
            for (int i = x + 1, j = y + 1; i < size && j < size; i++, j++)
            {
                smartCell = Board.GetCell(i, j);
                if (checker(this, smartCell))
                    yield return smartCell;
                if (!BoardUtils.Continue(smartCell))
                    break;
            }

            //Влево вниз
            for (int i = x - 1, j = y + 1; i >= 0 && j < size; i--, j++)
            {
                smartCell = Board.GetCell(i, j);
                if (checker(this, smartCell))
                    yield return smartCell;
                if (!BoardUtils.Continue(smartCell))
                    break;
            }
        }
    }
}
