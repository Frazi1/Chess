using System;
using chesslib.Field;

namespace chesslib.Command
{
    [Serializable]
    public class MakeMoveCommand : ICommand
    {
        private readonly PlayerColor _playerColor;
        private Piece _destroyedPiece;
        private Move _move;

        public int NextX { get { return Move.ToX; } }
        public int NextY { get { return Move.ToY; } }
        public int PrevX { get { return Move.FromX; } }
        public int PrevY { get { return Move.FromY; } }

        public Move Move
        {
            get
            {
                return _move;
            }

            set
            {
                _move = value;
            }
        }

        public MakeMoveCommand(PlayerColor playerColor, Move move)
        {
            _playerColor = playerColor;
            Move = move;
        }


        public bool CanExecute(object parameter)
        {
            Game game = (Game)parameter;
            //Cell[,] chessBoard = game.Board.ChessBoard;
            Cell nextCell = /*chessBoard[NextX, NextY];*/ game.GetCell(NextX, NextY);
            Piece piece = /*chessBoard[PrevX, PrevY].Piece;*/ game.GetPiece(PrevX, PrevY);

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
            Game game = (Game)parameter;
            //Cell[,] chessBoard = game.Board.ChessBoard;
            Cell nextCell = game.GetCell(NextX, NextY); /*chessBoard[NextX, NextY];*/
            Piece piece = /*chessBoard[PrevX, PrevY].Piece;*/ game.GetPiece(PrevX, PrevY);

            _destroyedPiece = game.Board.MovePiece(piece, nextCell, MoveFlags.UpdateAttacked | MoveFlags.UpdateMoves);
        }
        public void Undo(object parameter)
        {
            Game game = (Game)parameter;
            //Cell[,] chessBoard = game.Board.ChessBoard;
            Cell nextCell = /*chessBoard[NextX, NextY];*/ game.GetCell(NextX, NextY);
            Cell prevCell = /*chessBoard[PrevX, PrevY];*/ game.GetCell(PrevX, PrevY);
            //var piece = chessBoard[NextX, NextY].Piece;
            game.ChangeTurn();
            game.Board.UndoMove(prevCell,
                nextCell,
                _destroyedPiece,
                MoveFlags.UpdateAttacked | MoveFlags.UpdateMoves);
        }

        public override string ToString()
        {
            return string.Format("from {0}, {1} to {2}, {3}", PrevX, PrevY, NextX, NextY);
        }
    }
}

