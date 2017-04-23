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
        void CancelTurn();

        event EventsDelegates.MoveDoneEventHandler MoveDone;
    }
}
