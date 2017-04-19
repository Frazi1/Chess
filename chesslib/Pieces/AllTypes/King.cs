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
    public class King : Piece, IMoved
    {
        public King(Cell currentCell, PlayerType playerType, Board board) : base(currentCell, playerType, board)
        {
            HasAlreadyMoved = false;
            IsUnderAttack = false;
        }

        public bool HasAlreadyMoved { get; set; }
        public bool IsUnderAttack { get; set; }

        public override List<Cell> GetAllowedMoves()
        {
            List<Cell> allowedMoves = new List<Cell>();
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            Cell[,] chessBoard = Board.ChessBoard;

            int size = Board.ChessBoard.GetLength(0);

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    TryCell(allowedMoves, chessBoard, i, j);
                }
            }

            return allowedMoves;
        }

        public override bool MoveTo(Cell cell, IPlayer player)
        {
            return base.MoveTo(cell, player);
        }
    }
}
