using chesslib.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using chesslib.Command;
using chesslib.Strategy;

namespace chesslib.Player
{
    public class RealPlayer : IPlayer
    {
        private Game _game;

        public RealPlayer(PlayerType playerType, Game game)
        {
            PlayerType = playerType;
            _game = game;
        }

        public PlayerType PlayerType { get; set; }
        public MakeMoveCommand MakeMoveCommand { get; set; }
        public IStrategy Strategy { get; set; }

        public void PrepareMove()
        {
            var move = Strategy.PrepareMove();
            MakeMoveCommand = new MakeMoveCommand(this, move.Item1, move.Item2, _game);
        }

        public void MakeMove()
        {
            PrepareMove();
            if (MakeMoveCommand != null)
            {
                MakeMoveCommand.Execute();
                OnMove();
            }
        }

        private void OnMove()
        {
            MakeMoveCommand = null;
        }
    }
}
