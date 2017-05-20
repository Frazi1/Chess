using System.Collections.Generic;
using chesslib.Field;

namespace chesslib
{
    public interface IMovable
    {
        IEnumerable<Cell> GetAttackPattern();
        IEnumerable<Cell> GetMovePattern();
    }
}