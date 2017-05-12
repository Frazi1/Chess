using System.Collections;
using System.Collections.Generic;

namespace chesslib
{
    public interface IMovable
    {
        IEnumerable<Cell> GetAttackPattern();
        IEnumerable<Cell> GetMovePattern();
    }
}