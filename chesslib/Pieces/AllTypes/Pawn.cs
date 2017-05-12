using chesslib.Field;
using System;

namespace chesslib.Figures
{
    [Serializable]
    public class Pawn : Piece
    {
        public Pawn(Cell currentCell, PlayerColor playerColor, Board board) : base(currentCell, playerColor, board)
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
            if (PlayerColor == PlayerColor.White)
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
                            chessBoard[x - 1, y - 1].Piece.PlayerColor != PlayerColor)
                        {
                            TryMoveToCell(x - 1, y - 1);
                        }
                    }

                    //Атака вперед вправо
                    if (y > 0 && x < size - 1)
                    {
                        if (chessBoard[x + 1, y - 1].IsTaken &&
                            chessBoard[x + 1, y - 1].Piece.PlayerColor != PlayerColor)
                        {
                            TryMoveToCell(x + 1, y - 1);
                        }
                    }
                }
            }
            //Черные
            else if (PlayerColor == PlayerColor.Black)
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
                            chessBoard[x - 1, y + 1].Piece.PlayerColor != PlayerColor)
                        {
                            TryMoveToCell(x - 1, y + 1);
                        }
                    }

                    //Атака вперед вправо
                    if (y > 0 && x < size - 1)
                    {
                        if (chessBoard[x + 1, y + 1].IsTaken &&
                            chessBoard[x + 1, y + 1].Piece.PlayerColor != PlayerColor)
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

            if (PlayerColor == PlayerColor.White)
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
            else if (PlayerColor == PlayerColor.Black)
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
