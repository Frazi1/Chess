using chesslib.Figures.Interfaces;
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
    public class Rook : Piece, IMoved
    {
        public Rook(Cell currentCell, PlayerType playerType, Board board) : base(currentCell, playerType, board)
        {
            HasAlreadyMoved = false;
        }

        public bool HasAlreadyMoved { get; set; }

        public override bool MoveTo(Cell cell, IPlayer player)
        {
            bool moved = base.MoveTo(cell, player);
            if (moved)
                HasAlreadyMoved = true;
            return moved;
        }

        public override List<Cell> GetAllowedMoves()
        {
            List<Cell> allowedMoves = new List<Cell>();
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            Cell[,] chessBoard = Board.ChessBoard;

            int size = Board.ChessBoard.GetLength(0);

            bool _continue;
            //Вправо
            for (int i = x + 1, j = y; i < size; i++)
            {
                _continue = TryCell(allowedMoves, chessBoard, i, j);
                if (!_continue)
                    break;
            }
            //Влево
            for (int i = x - 1, j = y; i >= 0; i--)
            {
                _continue = TryCell(allowedMoves, chessBoard, i, j);
                if (!_continue)
                    break;
            }
            //Вниз
            for (int i = x, j = y + 1; j < size; j++)
            {
                _continue = TryCell(allowedMoves, chessBoard, i, j);
                if (!_continue)
                    break;
            }
            //Вверх
            for (int i = x, j = y - 1; j >= 0; j--)
            {
                _continue = TryCell(allowedMoves, chessBoard, i, j);
                if (!_continue)
                    break;
            }

            return allowedMoves;
        }
    }
}
