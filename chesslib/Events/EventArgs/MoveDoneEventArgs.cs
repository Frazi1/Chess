using System;
using chesslib.Command;

namespace chesslib.Events
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