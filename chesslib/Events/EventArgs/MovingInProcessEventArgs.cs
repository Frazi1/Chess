using chesslib.Player;
using System;

namespace chesslib.Events
{
    public class MovingInProcessEventArgs : EventArgs
    {
        public MovingInProcessEventArgs(IPlayer player)
        {
            Player = player;
        }

        public IPlayer Player { get; set; }
    }
}