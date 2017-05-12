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
            return GetMovePattern();
        }

        public override IEnumerable<Cell> GetMovePattern()
        {
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;


            List<Tuple<int, int>> toCheck = new List<Tuple<int, int>>()
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
                new Tuple<int, int>(x+1,y-2),
            };

            foreach (var move in toCheck)
            {
                int i = move.Item1;
                int j = move.Item2;
                if (BoardUtils.IsValidCell(Board.ChessBoard, i, j))
                    yield return Board.GetCell(i, j);
            }
        }
    }
}
