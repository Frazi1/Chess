using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Strategy
{
    public interface IStrategy
    {
        Tuple<Piece,Cell> PrepareMove();
    }
}
