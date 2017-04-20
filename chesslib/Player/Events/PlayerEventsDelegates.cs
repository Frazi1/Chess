using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Player
{
    public static class PlayerEventsDelegates
    {
        public delegate void MoveDoneEventHandler(object sender, MoveDoneEventArgs e);
    }
}
