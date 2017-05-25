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
    public class King : Piece
    {
        public King(SmartCell currentCell, PlayerColor playerColor, Board board) : base(currentCell, playerColor, board)
        {
            PieceType = playerColor == PlayerColor.Black ? EnumPiece.BKing : EnumPiece.WKing;
        }



        public override IEnumerable<SmartCell> GetAttackPattern()
        {
            return GetPattern((p, c)=>true);
        }

        private IEnumerable<SmartCell> GetPattern(Func<Piece, SmartCell, bool> checker)
        {
            int x = PosX;
            int y = PosY;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (BoardUtils.IsValidCell(i, j))
                    {
                        if (i == PosX && j == PosX)
                            continue;

                        SmartCell smartCell = Board.GetCell(i, j);
                        if (checker(this, smartCell))
                            yield return smartCell;
                    }
                }
            }
        }

        public override IEnumerable<SmartCell> GetMovePattern()
        {
            return GetPattern(BoardUtils.PieceCanMoveTo);
        }

    }
}
