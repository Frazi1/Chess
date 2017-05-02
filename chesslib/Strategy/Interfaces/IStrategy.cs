using chesslib.Field;
using chesslib.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Strategy
{
    public interface IStrategy
    {
        Tuple<Piece,Cell> PrepareMove(IPlayer player, Board board);
    }
}
