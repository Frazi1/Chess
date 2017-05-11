using System;

namespace chesslib.Command
{
    [Serializable]
    public class MakeMoveCommand : ICommand
    {
        //private Piece _piece;
        private PlayerColor _playerColor;
        //private Cell _nextCell;
        //private Cell _prevCell;
        private Piece _destroyedPiece;

        private int prevX;
        private int prevY;
        private int nextX;
        private int nextY;

        public MakeMoveCommand(PlayerColor playerColor, int prevX, int prevY, int nextX, int nextY)
        {
            _playerColor = playerColor;
            PrevX = prevX;
            PrevY = prevY;
            NextX = nextX;
            NextY = nextY;
        }

        public int PrevX
        {
            get
            {
                return prevX;
            }

            set
            {
                prevX = value;
            }
        }
        public int PrevY
        {
            get
            {
                return prevY;
            }

            set
            {
                prevY = value;
            }
        }
        public int NextX
        {
            get
            {
                return nextX;
            }

            set
            {
                nextX = value;
            }
        }
        public int NextY
        {
            get
            {
                return nextY;
            }

            set
            {
                nextY = value;
            }
        }

        public bool CanExecute(object parameter)
        {
            Game game = (Game) parameter;
            Cell[,] chessBoard = game.Board.ChessBoard;
            var nextCell = chessBoard[NextX, NextY];
            var piece = chessBoard[PrevX, PrevY].Piece;

            if (game.IsPaused || game.IsGameFinished)
                return false;
            if (_playerColor != game.CurrentPlayer.PlayerColor)
                return false;
            if (_playerColor != piece.PlayerType)
                return false;
            return piece.CanMoveTo(nextCell);
        }

        public void Execute(object parameter)
        {
            Game game = (Game) parameter;
            Cell[,] chessBoard = game.Board.ChessBoard;
            var nextCell = chessBoard[NextX, NextY];
            var piece = chessBoard[PrevX, PrevY].Piece;

            _destroyedPiece = game.Board.MovePiece(piece, nextCell, true, true);
        }

        public void Undo(object parameter)
        {
            Game game = (Game) parameter;
            Cell[,] chessBoard = game.Board.ChessBoard;
            var nextCell = chessBoard[NextX, NextY];
            var prevCell = chessBoard[PrevX, PrevY];
            var piece = chessBoard[NextX, NextY].Piece;
            game.ChangeTurn();
            //_piece.MoveTo(_prevCell, _player);

            //Удаляем фигуру из текущей клетки
            nextCell.Piece = null;
            //Ставим фигуру на предудущую клетку
            piece.CurrentCell = prevCell;
            prevCell.Piece = piece;
            piece.MovesCounter--;
            if (_destroyedPiece != null)
            {
                _destroyedPiece.IsInGame = true;
                nextCell.Piece = _destroyedPiece;
                _destroyedPiece.CurrentCell = nextCell;
                game.Board.AlivePieces.Add(_destroyedPiece);
            }
  
        }
        public override string ToString()
        {
            return string.Format("from {0}, {1} to {2}, {3}", PrevX, PrevY, NextX, NextY);
        }
    }
}
