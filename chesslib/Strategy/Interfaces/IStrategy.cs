using chesslib.Field;
using chesslib.Player;
using System;

namespace chesslib.Strategy
{
    public interface IStrategy
    {
        Tuple<Cell,Cell> PrepareMove(IPlayer player, Board board);
    }
}
