using System;
using System.Collections.Generic;
using chesslib.Field;
using chesslib.Utils;

namespace chesslib.Figures
{
    [Serializable]
    public class Knight : Piece
    {
        public Knight(Cell currentCell, PlayerColor playerColor, Board board) : base(currentCell, playerColor, board)
        {
            PieceType = PieceType.Knight;
        }

        public override IEnumerable<Cell> GetAttackPattern()
        {
            return GetPattern((p, c)=>true);
        }

        private IEnumerable<Cell> GetPattern(Func<Piece, Cell, bool> checker)
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
                    Cell cell = Board.GetCell(i, j);
                    if (checker(this, cell))
                        yield return cell;
                }
            }
        }

        public override IEnumerable<Cell> GetMovePattern()
        {
            return GetPattern(BoardUtils.PieceCanMoveTo);
        }
    }
}
