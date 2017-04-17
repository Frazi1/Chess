using chesslib.Field;
using chesslib.Figures.Interfaces;
using chesslib.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chesslib.Figures
{
    public class Pawn : Piece, IMoved
    {
        public Pawn(Cell currentCell, PlayerType playerType) : base(currentCell, playerType)
        {
            HasAlreadyMoved = false;
            IsPromoted = false;
        }

        public bool HasAlreadyMoved { get; set; }
        public bool IsPromoted { get; set; }

        //TODO: Переделать GetAllowedMoves
        public override List<Cell> GetAllowedMoves()
        {
            List<Cell> allowedMoves = new List<Cell>();
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            Cell[,] chessBoard = Board.Instance.ChessBoard;

            int size = Board.Instance.ChessBoard.GetLength(0);

            //Белые
            if (PlayerType == PlayerType.White)
            {
                //Движение вперед
                if (y > 0)
                {
                    if (!chessBoard[x, y - 1].IsTaken)
                        allowedMoves.Add(chessBoard[x, y - 1]);
                    if (!HasAlreadyMoved && !chessBoard[x, y - 2].IsTaken)
                        allowedMoves.Add(chessBoard[x, y - 2]);

                    //Атака вперед влево
                    if (y > 0 && x > 0)
                    {
                        if (chessBoard[x - 1, y - 1].IsTaken &&
                            chessBoard[x - 1, y - 1].Piece.PlayerType != this.PlayerType)
                            allowedMoves.Add(chessBoard[x - 1, y - 1]);
                    }

                    //Атака вперед и вправо
                    if (y > 0 && x < size - 1)
                    {
                        if (chessBoard[x + 1, y - 1].IsTaken &&
                            chessBoard[x + 1, y - 1].Piece.PlayerType != this.PlayerType)
                            allowedMoves.Add(chessBoard[x + 1, y - 1]);
                    }
                }
            }
            //Черные
            else if (PlayerType == PlayerType.Black)
            {
                //Движение вперед
                if (y < size)
                {
                    if (!chessBoard[x, y + 1].IsTaken)
                        allowedMoves.Add(chessBoard[x, y + 1]);
                    if (!HasAlreadyMoved && !chessBoard[x, y + 2].IsTaken)
                        allowedMoves.Add(chessBoard[x, y + 2]);

                    //Атака вперед влево
                    if (y > 0 && x > 0)
                    {
                        if (chessBoard[x - 1, y + 1].IsTaken &&
                            chessBoard[x - 1, y + 1].Piece.PlayerType != this.PlayerType)
                            allowedMoves.Add(chessBoard[x - 1, y + 1]);
                    }

                    //Атака вперед и вправо
                    if (y > 0 && x < size-1)
                    {
                        if (chessBoard[x + 1, y + 1].IsTaken &&
                            chessBoard[x + 1, y + 1].Piece.PlayerType != this.PlayerType)
                            allowedMoves.Add(chessBoard[x + 1, y + 1]);
                    }
                }
            }

            return allowedMoves;
        }
        public override bool MoveTo(Cell nextCell, IPlayer player)
        {

            bool moved = base.MoveTo(nextCell, player);
            if (moved)
                HasAlreadyMoved = true;
            return moved;
        }
    }
}
