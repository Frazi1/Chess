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
    public class Rook : Piece, IMoved
    {
        public Rook(Cell currentCell, PlayerType playerType) : base(currentCell, playerType)
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
            Cell[,] chessBoard = Board.Instance.ChessBoard;

            int size = Board.Instance.ChessBoard.GetLength(0);

            //Вправо
            for (int i = x; i < size; i++)
            {
                if (!chessBoard[i, y].IsTaken)
                    allowedMoves.Add(chessBoard[i, y]);
                else
                {
                    if (chessBoard[i, y].Piece.PlayerType != PlayerType)
                        allowedMoves.Add(chessBoard[i, y]);
                    if (chessBoard[i, y].Piece != this)
                        break;
                }
            }
            //Влево
            for (int i = x; i > 0; i--)
            {
                if (!chessBoard[i, y].IsTaken)
                    allowedMoves.Add(chessBoard[i, y]);
                else
                {
                    if (chessBoard[i, y].Piece.PlayerType != PlayerType)
                        allowedMoves.Add(chessBoard[i, y]);
                    if (chessBoard[i, y].Piece != this)
                        break;
                }
            }
            //Вниз
            for (int i = y; i < size; i++)
            {
                if (!chessBoard[x, i].IsTaken)
                    allowedMoves.Add(chessBoard[x, i]);
                else
                {
                    if (chessBoard[x, i].Piece.PlayerType != PlayerType)
                        allowedMoves.Add(chessBoard[x, i]);
                    if (chessBoard[x, i].Piece != this)
                        break;
                }
            }
            //Вверх
            for (int i = y; i > 0; i--)
            {
                if (!chessBoard[x, i].IsTaken)
                    allowedMoves.Add(chessBoard[x, i]);
                else
                {
                    if (chessBoard[x, i].Piece.PlayerType != PlayerType)
                        allowedMoves.Add(chessBoard[x, i]);
                    if (chessBoard[x, i].Piece != this)
                        break;
                }
            }
            return allowedMoves;
        }

        //TODO: Дописать и переделать GetAllowedMoves
        private bool TryCell(List<Cell> allowedMoves, Cell[,] chessBoard, int x1, int y1)
        {
            bool _continue = true;
            if (x1 >= 0 &&
                x1 < chessBoard.GetLength(0) &&
                y1 >= 0 &&
                y1 < chessBoard.GetLength(0))
            {
                if (!chessBoard[x1, y1].IsTaken ||
                    chessBoard[x1, y1].Piece.PlayerType != PlayerType)
                    allowedMoves.Add(chessBoard[x1, y1]);
                _continue = false;
            }
            return _continue;
        }
    }
}
