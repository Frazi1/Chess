using chesslib.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using chesslib.Command;

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

        public void PrepareMove(Piece piece, Cell nextCell)
        {
            MakeMoveCommand = new MakeMoveCommand(this, piece, nextCell, _game);
        }

        public void MakeMove()
        {
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
