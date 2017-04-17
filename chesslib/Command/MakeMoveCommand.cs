using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Command
{
    public class MakeMoveCommand : ICommand
    {
        private Game _game;
        private Piece _piece;
        private Cell _nextCell;

        public MakeMoveCommand(Game game, Piece piece, Cell nextCell)
        {
            _game = game;
            _piece = piece;
            _nextCell = nextCell;
        }

        public void Execute()
        {
            _game.MakeMove(_piece, _nextCell);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
