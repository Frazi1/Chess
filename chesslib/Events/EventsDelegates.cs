using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Events
{
    public static class EventsDelegates
    {
        public delegate void MoveDoneEventHandler(object sender, MoveDoneEventArgs e);
        public delegate void GameStateChangedEventHandler(object sender, GameStateChangedEventArgs e);
    }
}
