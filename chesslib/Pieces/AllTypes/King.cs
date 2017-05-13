using System;
using System.Collections.Generic;
using chesslib.Field;
using chesslib.Utils;

namespace chesslib.Figures
{
    [Serializable]
    public class King : Piece
    {
        public King(Cell currentCell, PlayerColor playerColor, Board board) : base(currentCell, playerColor, board)
        {
            PieceType = PieceType.King;
        }



        public override IEnumerable<Cell> GetAttackPattern()
        {
            return GetPattern((Piece p, Cell c)=>true);
        }

        private IEnumerable<Cell> GetPattern(Func<Piece, Cell, bool> checker)
        {
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (BoardUtils.IsValidCell(Board.ChessBoard, i, j))
                    {
                        if (i == PosX && j == PosX)
                            continue;

                        Cell cell = Board.GetCell(i, j);
                        if (checker(this, cell))
                            yield return cell;
                    }
                }
            }
        }

        public override IEnumerable<Cell> GetMovePattern()
        {
            return GetPattern(BoardUtils.PieceCanMoveTo);
        }

    }
}
