using chesslib.Field;
using chesslib.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chesslib.Figures
{
    [Serializable]
    public class Pawn : Piece
    {
        public Pawn(Cell currentCell, PlayerType playerType, Board board) : base(currentCell, playerType, board)
        {
            IsPromoted = false;
            PieceType = PieceType.Pawn;
        }

        public bool IsPromoted { get; set; }

        //TODO: Переделать GetAllowedMoves
        public override void SetAllowedMoves()
        {
            base.SetAllowedMoves();
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            Cell[,] chessBoard = Board.ChessBoard;

            int size = Board.ChessBoard.GetLength(0);

            //Белые
            if (PlayerType == PlayerType.White)
            {
                //Движение вперед
                if (y > 0)
                {
                    if (!chessBoard[x, y - 1].IsTaken)
                    {
                        AllowedCells.Add(chessBoard[x, y - 1]);
                        if (!HasAlreadyMoved && !chessBoard[x, y - 2].IsTaken)
                            AllowedCells.Add(chessBoard[x, y - 2]);
                    }
                    //Атака вперед влево
                    if (y > 0 && x > 0)
                    {
                        TryAttackCell(x - 1, y - 1);
                        if (chessBoard[x - 1, y - 1].IsTaken &&
                            chessBoard[x - 1, y - 1].Piece.PlayerType != this.PlayerType)
                        {
                            AllowedCells.Add(chessBoard[x - 1, y - 1]);
                        }
                    }

                    //Атака вперед и вправо
                    if (y > 0 && x < size - 1)
                    {
                        TryAttackCell(x + 1, y - 1);
                        if (chessBoard[x + 1, y - 1].IsTaken &&
                            chessBoard[x + 1, y - 1].Piece.PlayerType != this.PlayerType)
                        {
                            AllowedCells.Add(chessBoard[x + 1, y - 1]);
                        }
                    }
                }
            }
            //Черные
            else if (PlayerType == PlayerType.Black)
            {
                //Движение вперед
                if (y < size - 1)
                {
                    if (!chessBoard[x, y + 1].IsTaken)
                    {
                        AllowedCells.Add(chessBoard[x, y + 1]);
                        if (!HasAlreadyMoved && !chessBoard[x, y + 2].IsTaken)
                            AllowedCells.Add(chessBoard[x, y + 2]);
                    }
                    //Атака вперед влево
                    if (y > 0 && x > 0)
                    {
                        TryAttackCell(x - 1, y + 1);
                        if (chessBoard[x - 1, y + 1].IsTaken &&
                            chessBoard[x - 1, y + 1].Piece.PlayerType != this.PlayerType)
                        {
                            AllowedCells.Add(chessBoard[x - 1, y + 1]);
                        }
                    }

                    //Атака вперед и вправо
                    if (y > 0 && x < size - 1)
                    {
                        TryAttackCell(x + 1, y + 1);
                        if (chessBoard[x + 1, y + 1].IsTaken &&
                            chessBoard[x + 1, y + 1].Piece.PlayerType != this.PlayerType)
                        {
                            AllowedCells.Add(chessBoard[x + 1, y + 1]);
                        }
                    }
                }
            }

        }
        public override bool MoveTo(Cell nextCell, IPlayer player)
        {
            bool moved = base.MoveTo(nextCell, player);
            return moved;
        }
        //public override List<Cell> GetAttackedCells()
        //{
        //    int y = PlayerType == PlayerType.White ? CurrentCell.PosY - 1 : CurrentCell.PosY + 1;
        //    int x1 = CurrentCell.PosX - 1;
        //    int x2 = CurrentCell.PosX + 1;
        //    List<Cell> attacked = new List<Cell>();

        //    if (TryAttackCell(Board.ChessBoard, x1, y))
        //        attacked.Add(Board.ChessBoard[x1, y]);
        //    if (TryAttackCell(Board.ChessBoard, x2, y))
        //        attacked.Add(Board.ChessBoard[x2, y]);
        //    return attacked;
        //}
    }
}
