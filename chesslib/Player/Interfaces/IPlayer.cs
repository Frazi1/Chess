﻿using chesslib.Command;
using chesslib.Strategy;
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

        IStrategy Strategy { get; set; }
        void MakeMove();
    }
}
