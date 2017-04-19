using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using chesslib.Player;
using chesslib.Field;

namespace chesslib.Figures
{
    [Serializable]
    public class Knight : Piece
    {
        public Knight(Cell currentCell, PlayerType playerType, Board board) : base(currentCell, playerType, board)
        {
        }

        public override List<Cell> GetAllowedMoves()
        {
            List<Cell> allowedMoves = new List<Cell>();
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            Cell[,] chessBoard = Board.ChessBoard;

            int size = Board.ChessBoard.GetLength(0);

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

            foreach (var item in toCheck)
            {
                TryCell(allowedMoves, chessBoard, item.Item1, item.Item2);
            }
            return allowedMoves;

        }

        public override bool MoveTo(Cell cell, IPlayer player)
        {
            return base.MoveTo(cell, player);
        }
    }
}
