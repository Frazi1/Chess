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
        public Pawn(Cell currentCell, PlayerColor playerType, Board board) : base(currentCell, playerType, board)
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
            if (PlayerType == PlayerColor.White)
            {
                //Движение вперед
                if (y > 0)
                {
                    if (!chessBoard[x, y - 1].IsTaken)
                    {
                        TryMoveToCell(x, y - 1);
                        if (!HasAlreadyMoved && !chessBoard[x, y - 2].IsTaken)
                            TryMoveToCell(x, y - 2);
                    }

                    //Атака вперед влево
                    if (y > 0 && x > 0)
                    {
                        if (chessBoard[x - 1, y - 1].IsTaken &&
                            chessBoard[x - 1, y - 1].Piece.PlayerType != this.PlayerType)
                        {
                            TryMoveToCell(x - 1, y - 1);
                        }
                    }

                    //Атака вперед вправо
                    if (y > 0 && x < size - 1)
                    {
                        if (chessBoard[x + 1, y - 1].IsTaken &&
                            chessBoard[x + 1, y - 1].Piece.PlayerType != this.PlayerType)
                        {
                            TryMoveToCell(x + 1, y - 1);
                        }
                    }
                }
            }
            //Черные
            else if (PlayerType == PlayerColor.Black)
            {
                //Движение вперед
                if (y < size - 1)
                {
                    if (!chessBoard[x, y + 1].IsTaken)
                    {
                        TryMoveToCell(x, y + 1);
                        if (!HasAlreadyMoved && !chessBoard[x, y + 2].IsTaken)
                            TryMoveToCell(x, y + 2);
                    }

                    //Атака вперед влево
                    if (y > 0 && x > 0)
                    {
                        if (chessBoard[x - 1, y + 1].IsTaken &&
                            chessBoard[x - 1, y + 1].Piece.PlayerType != this.PlayerType)
                        {
                            TryMoveToCell(x - 1, y + 1);
                        }
                    }

                    //Атака вперед вправо
                    if (y > 0 && x < size - 1)
                    {
                        if (chessBoard[x + 1, y + 1].IsTaken &&
                            chessBoard[x + 1, y + 1].Piece.PlayerType != this.PlayerType)
                        {
                            TryMoveToCell(x + 1, y + 1);
                        }
                    }

                }
            }

        }

        public override void GetAttackedCells()
        {
            base.GetAttackedCells();

            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            Cell[,] chessBoard = Board.ChessBoard;

            int size = Board.ChessBoard.GetLength(0);

            if (PlayerType == PlayerColor.White)
            {
                //Атака вперед влево
                if (y > 0 && x > 0)
                {
                    TryAttackCell(x - 1, y - 1);
                }

                //Атака вперед вправо
                if (y > 0 && x < size - 1)
                {
                    TryAttackCell(x + 1, y - 1);
                }
            }
            else if (PlayerType == PlayerColor.Black)
            {
                
                //Атака вперед влево
                if (y > 0 && x > 0)
                {
                    TryAttackCell(x - 1, y + 1);
                }

                //Атака вперед вправо
                if (y > 0 && x < size - 1)
                {
                    TryAttackCell(x + 1, y + 1);
                }
            }

        }

    }
}
