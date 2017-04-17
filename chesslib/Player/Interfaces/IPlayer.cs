using chesslib.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Player
{
    public interface IPlayer
    {
        PlayerType PlayerType { get; set; }
        MakeMoveCommand MakeMoveCommand { get; set; }

        void PrepareMove(Piece p, Cell nextCell);
        void MakeMove();
    }
}
