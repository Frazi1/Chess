using chesslib.Field;
using chesslib.Player;

namespace chesslib.Strategy
{
    public interface IStrategy
    {
        Move PrepareMove(IPlayer player, Board board);
    }
}
