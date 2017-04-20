using chesslib.Command;
using System;

namespace chesslib.Player
{
    public class MoveDoneEventArgs : EventArgs
    {
        public MoveDoneEventArgs(MakeMoveCommand moveCommand)
        {
            MoveCommand = moveCommand;
        }

        public MakeMoveCommand MoveCommand { get; set; }
    }
}