using chesslib.Field;
using System;
using System.Collections.Generic;

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

        public override IEnumerable<Cell> GetAttackPattern()
        {
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;

            int size = Board.Size;

            if (PlayerColor == PlayerColor.White)
            {
                //Атака вперед влево
                if (y > 0 && x > 0)
                {
                    yield return Board.GetCell(x - 1, y - 1);
                }

                //Атака вперед вправо
                if (y > 0 && x < size - 1)
                {
                    yield return Board.GetCell(x + 1, y - 1);
                }
            }
            else if (PlayerColor == PlayerColor.Black)
            {

                //Атака вперед влево
                if (y <size-1 && x > 0)
                {
                    yield return Board.GetCell(x - 1, y + 1);
                }

                //Атака вперед вправо
                if (y > 0 && x < size - 1)
                {
                    yield return Board.GetCell(x + 1, y + 1);
                }
            }
        }

        public override IEnumerable<Cell> GetMovePattern()
        {
            int x = PosX;
            int y = PosY;
            int size = Board.Size;



            if (PlayerColor == PlayerColor.White)
            {
                if (y > 0)
                {
                    Cell cell1 = Board.GetCell(x, y - 1);
                    if (!cell1.IsTaken)
                    {
                        yield return cell1;
                        Cell cell2 = Board.GetCell(x, y - 2);
                        if (!HasAlreadyMoved && !cell2.IsTaken)
                            yield return cell2;
                    }

                    //Атака вперед влево
                    if (y > 0 && x > 0)
                    {
                        Cell cell = Board.GetCell(x - 1, y - 1);
                        if (cell.IsTaken &&
                            cell.Piece.PlayerColor != PlayerColor)
                        {
                            yield return cell;
                        }
                    }

                    //Атака вперед вправо
                    if (y > 0 && x < size - 1)
                    {
                        Cell cell = Board.GetCell(x + 1, y - 1);
                        if (cell.IsTaken &&
                            cell.Piece.PlayerColor != PlayerColor)
                        {
                            yield return cell;
                        }
                    }
                }
            }
            else if (PlayerColor == PlayerColor.Black)
            {
                if (y < size - 1)
                {
                    Cell cell1 = Board.GetCell(x, y + 1);
                    if (!cell1.IsTaken)
                    {
                        yield return cell1;
                        Cell cell2 = Board.GetCell(x, y + 2);
                        if (!HasAlreadyMoved && !cell2.IsTaken)
                            yield return cell2;
                    }

                    //Атака вперед влево
                    if (y > 0 && x > 0)
                    {
                        Cell cell = Board.GetCell(x - 1, y + 1);
                        if (cell.IsTaken &&
                            cell.Piece.PlayerColor != PlayerColor)
                        {
                            yield return cell;
                        }
                    }

                    //Атака вперед вправо
                    if (y > 0 && x < size - 1)
                    {
                        Cell cell = Board.GetCell(x + 1, y + 1);
                        if (cell.IsTaken &&
                            cell.Piece.PlayerColor != PlayerColor)
                        {
                            yield return cell;
                        }
                    }
                }
            }

        }
    }
}
