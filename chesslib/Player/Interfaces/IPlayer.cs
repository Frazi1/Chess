﻿using chesslib.Command;
using chesslib.Events;

namespace chesslib.Player
{
    public interface IPlayer
    {
        PlayerColor PlayerColor { get; set; }
        PlayerType PlayerType { get; }
        MakeMoveCommand MakeMoveCommand { get; set; }

        void DoTurn(Game game);
        void CancelTurn();

        event EventsDelegates.MoveDoneEventHandler MoveDone;
    }
}
