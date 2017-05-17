using System.Collections;
using System.Collections.Generic;
using chesslib.Board;

namespace chesslib
{
    public interface IMovable
    {
        IEnumerable<Cell> GetAttackPattern();
        IEnumerable<Cell> GetMovePattern();
    }
}