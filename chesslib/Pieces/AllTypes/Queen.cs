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
    public class Queen : Piece
    {
        public Queen(Cell currentCell, PlayerType playerType, Board board) : base(currentCell, playerType, board)
        {
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
            //Вправо вверх
            for (int i = x + 1, j = y - 1; i < size && j >= 0; i++, j--)
            {
                _continue = TryCell(allowedMoves, chessBoard, i, j);
                if (!_continue)
                    break;
            }
            //Влево вверх
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                _continue = TryCell(allowedMoves, chessBoard, i, j);
                if (!_continue)
                    break;
            }
            //Вправо вниз
            for (int i = x + 1, j = y + 1; i < size && j < size; i++, j++)
            {
                _continue = TryCell(allowedMoves, chessBoard, i, j);
                if (!_continue)
                    break;
            }
            //Влево вниз
            for (int i = x - 1, j = y + 1; i >= 0 && j < size; i--, j++)
            {
                _continue = TryCell(allowedMoves, chessBoard, i, j);
                if (!_continue)
                    break;
            }

            return allowedMoves;
        }

        public override bool MoveTo(Cell cell, IPlayer player)
        {
            return base.MoveTo(cell, player);
        }
    }
}
