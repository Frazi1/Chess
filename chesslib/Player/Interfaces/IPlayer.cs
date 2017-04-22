using chesslib.Command;
using chesslib.Events;
using chesslib.Strategy;
using System;

namespace chesslib.Player
{
    public interface IPlayer
    {
        PlayerType PlayerType { get; set; }
        MakeMoveCommand MakeMoveCommand { get; set; }

        Game Game { get; set; }

        void DoTurn();

        event EventsDelegates.MoveDoneEventHandler MoveDone;
    }
}
