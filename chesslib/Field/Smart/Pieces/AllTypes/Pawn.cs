using System;
using System.Collections.Generic;
using chesslib.Field.Bit;
using chesslib.Utils;

namespace chesslib.Field.Smart.Pieces.AllTypes
{
    [Serializable]
    public class Pawn : Piece
    {
        public int Direction { get; }
        public Pawn(SmartCell currentCell, PlayerColor playerColor, Board board) : base(currentCell, playerColor, board)
        {
            IsPromoted = false;
            PieceType = playerColor == PlayerColor.Black ? EnumPiece.BPawn : EnumPiece.WPawn;
            Direction = playerColor == PlayerColor.Black ? 1 : -1;
        }

        public bool IsPromoted { get; set; }

        public override IEnumerable<SmartCell> GetAttackPattern()
        {
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;

            //Атака вперед влево
            if (BoardUtils.IsValidCell(x - 1, y + Direction))
            {
                yield return Board.GetCell(x - 1, y + Direction);
            }

            //Атака вперед вправо
            if (BoardUtils.IsValidCell(x + 1, y + Direction))
            {
                yield return Board.GetCell(x + 1, y + Direction);
            }
        }

        public override IEnumerable<SmartCell> GetMovePattern()
        {
            int x = PosX;
            int y = PosY;

            if (!BoardUtils.IsValidCell(x, y + Direction))
                yield break;

            SmartCell cell1 = Board.GetCell(x, y + Direction);
            if (!cell1.IsTaken)
            {
                yield return cell1;
                SmartCell cell2 = Board.GetCell(x, y + 2 * Direction);
                if (!HasAlreadyMoved && !cell2.IsTaken)
                    yield return cell2;
            }

            //Атака вперед влево
            if (BoardUtils.IsValidCell(x-1,y+Direction))
            {
                SmartCell cell = Board.GetCell(x - 1, y + Direction);
                if (cell.IsTaken &&
                    cell.Piece.PlayerColor != PlayerColor)
                {
                    yield return cell;
                }
            }

            //Атака вперед вправо
            if (BoardUtils.IsValidCell(x + 1, y + Direction))
            {
                SmartCell cell = Board.GetCell(x + 1, y + Direction);
                if (cell.IsTaken &&
                    cell.Piece.PlayerColor != PlayerColor)
                {
                    yield return cell;
                }
            }
        }
    }
}

