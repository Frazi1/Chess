using chesslib.Player;
using System;

namespace chesslib.Command
{
    public class MakeMoveCommand : ICommand
    {
        private Piece _piece;
        private IPlayer _player;
        private Game _game;
        private Cell _nextCell;

        public MakeMoveCommand(IPlayer player,
            Piece piece,
            Cell nextCell,
            Game game)
        {
            _piece = piece;
            _player = player;
            _game = game;
            _nextCell = nextCell;
        }

        public bool CanExecute
        {
            get
            {
                return (_piece != null &&
                    _player != null &&
                    _game != null &&
                    _nextCell != null);
            }
        }

        public void Execute()
        {
            if (CanExecute)
                _game.MakeMove(_piece, _nextCell, _player);
        }
    }
}
