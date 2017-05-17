using chesslib.Player;
using System;
using chesslib.Board;

namespace chesslib.Strategy
{
    public interface IStrategy
    {
        Move PrepareMove(IPlayer player, Board.Board board);
    }
}
