using chesslib.Field;
using chesslib.Player;
using System;

namespace chesslib.Strategy
{
    public interface IStrategy
    {
        Move PrepareMove(IPlayer player, Board board);
    }
}
