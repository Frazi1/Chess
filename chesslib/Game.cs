using chesslib.Player;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace chesslib
{
    public class Game
    {
        public IPlayer Player1 { get; set; }
        public IPlayer Player2 { get; set; }
        public Cell[,] ChessBoard { get; set; }
    }
}
