﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Player
{
    public interface IPlayer
    {
        PlayerType PlayerType { get; set; }
        void MovePiece(Piece piece, Cell nextCell);
    }
}
