﻿using chesslib.Player;
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

        public Cell NextCell
        {
            get
            {
                return _nextCell;
            }

            private set
            {
                _nextCell = value;
            }
        }
        public Cell PrevCell
        {
            get
            {
                return _prevCell;
            }

            private set
            {
                _prevCell = value;
            }
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
                //game.Update(game);
                //game.ChangeTurn();
            }
        }

        public void Undo(object parameter)
        {
            Game game = (Game) parameter;
            game.ChangeTurn();
            //_piece.MoveTo(_prevCell, _player);

            //Удаляем фигуру из текущей клетки
            _nextCell.Piece = null;
            //Ставим фигуру на предудущую клетку
            _piece.CurrentCell = _prevCell;
            _prevCell.Piece = _piece;
            _piece.MovesCounter--;
            if (_destroyedPiece != null)
            {
                _destroyedPiece.IsInGame = true;
                _nextCell.Piece = _destroyedPiece;
                _destroyedPiece.CurrentCell = _nextCell;
                game.Board.AlivePieces.Add(_destroyedPiece);
            }

        }
        public override string ToString()
        {
            return string.Format("from {0} to {1}",_prevCell, NextCell);
        }
    }
}
