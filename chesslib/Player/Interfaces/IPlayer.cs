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

        IStrategy Strategy { get; set; }
        Game Game { get; set; }

        void MakeMove();
        void DoTurn();

        event EventsDelegates.MoveDoneEventHandler MoveDone;
    }
}
