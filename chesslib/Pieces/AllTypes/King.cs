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
            return GetMovePattern();
        }

        public override IEnumerable<Cell> GetMovePattern()
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

                        var cell = Board.GetCell(i, j);
                        yield return cell;
                        if (!BoardUtils.Continue(cell))
                            break;
                    }
                }
            }
        }

    }
}
