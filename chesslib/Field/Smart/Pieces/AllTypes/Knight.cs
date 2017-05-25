using System;
using System.Collections.Generic;
using chesslib.Utils;

namespace chesslib.Field.Smart.Pieces.AllTypes
{
    [Serializable]
    public class Knight : Piece
    {
        public Knight(SmartCell currentCell, PlayerColor playerColor, Board board) : base(currentCell, playerColor, board)
        {
            PieceType = playerColor == PlayerColor.Black ? Field.Bit.EnumPiece.BKnight : Field.Bit.EnumPiece.WKnight;
        }

        public override IEnumerable<SmartCell> GetAttackPattern()
        {
            return GetPattern((p, c)=>true);
        }

        private IEnumerable<SmartCell> GetPattern(Func<Piece, SmartCell, bool> checker)
        {
            int x = PosX;
            int y = PosY;


            List<Tuple<int, int>> toCheck = new List<Tuple<int, int>>
            {
                //влево
                new Tuple<int, int>(x-2,y-1),
                new Tuple<int, int>(x-2,y+1),
                new Tuple<int, int>(x-1,y+2),
                new Tuple<int, int>(x-1,y-2),
                //вправо
                new Tuple<int, int>(x+2,y-1),
                new Tuple<int, int>(x+2,y+1),
                new Tuple<int, int>(x+1,y+2),
                new Tuple<int, int>(x+1,y-2)
            };

            foreach (var move in toCheck)
            {
                int i = move.Item1;
                int j = move.Item2;
                if (BoardUtils.IsValidCell(i, j))
                {
                    SmartCell cell = Board.GetCell(i, j);
                    if (checker(this, cell))
                        yield return cell;
                }
            }
        }

        public override IEnumerable<SmartCell> GetMovePattern()
        {
            return GetPattern(BoardUtils.PieceCanMoveTo);
        }
    }
}
