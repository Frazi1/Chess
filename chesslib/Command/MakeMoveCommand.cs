using chesslib.Player;
using System;

namespace chesslib.Command
{
    public class MakeMoveCommand : ICommand
    {
        private Piece _piece;
        private IPlayer _player;
        private Cell _nextCell;
        private Cell _prevCell;
        private Piece _destroyedPiece;

        public MakeMoveCommand(IPlayer player,
            Piece piece,
            Cell nextCell)
        {
            _piece = piece;
            _player = player;
            _nextCell = nextCell;
            _prevCell = _piece.CurrentCell;
        }

        public bool CanExecute(object parameter)
        {
            Game game = (Game) parameter;
            if (game.IsPaused || game.IsGameFinished)
                return false;
            if (_player != game.CurrentPlayer)
                return false;
            return _piece.CanMoveTo(_nextCell, _player);
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                Game game = (Game) parameter;
                //game.GameUtils.SaveState();
                _destroyedPiece = _nextCell.Piece;
                if (_destroyedPiece != null)
                    game.Board.DestroyPiece(_destroyedPiece);
                _piece.MoveTo(_nextCell, _player);
                game.Update(game);
                game.ChangePlayers();
            }
        }

        public void Undo(object parameter)
        {
            Game game = (Game) parameter;
              //_piece.MoveTo(_prevCell, _player);
            _piece.CurrentCell = _prevCell;
            _prevCell.Piece = _piece;
            if (_destroyedPiece != null)
            {
                _destroyedPiece.IsInGame = true;
                _nextCell.Piece = _destroyedPiece;
                _destroyedPiece.CurrentCell = _nextCell;
                game.Board.AlivePieces.Add(_destroyedPiece);
            }
            game.ChangePlayers();
        }
    }
}
