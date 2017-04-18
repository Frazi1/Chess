using chesslib.Field;
using chesslib.Figures;
using chesslib.Player;
using System;
using System.Collections.Generic;
using System.Linq;

namespace chesslib
{
    public class Game : IObservable<Game>
    {
        private const int SIZE = 8;

        public List<IPlayer> Players { get; set; }
        public Board Board
        {
            get { return Board.Instance; }
        }

        private Cell[,] ChessBoard { get { return Board.ChessBoard; } }
        public List<Piece> Pieces { get { return Board.AlivePieces; } }
        public bool IsGameFinished { get; private set; }

        private IPlayer _currentPlayer;
        public IPlayer CurrentPlayer
        {
            get { return _currentPlayer; }
            private set
            {
                if (_currentPlayer != value)
                {
                    _currentPlayer = value;
                    _currentPlayer.OnNext(true);
                }
            }
        }

        public Game()
        {
            Players = new List<IPlayer>();
            _observers = new List<IObserver<Game>>();
            CreatePieces();
            IsGameFinished = false;
        }

        public bool MakeMove(Piece piece, Cell nextCell, IPlayer player)
        {
            if (CurrentPlayer != player)
                return false;

            DestroyPiece(piece, nextCell);
            bool moved = piece.MoveTo(nextCell, player);

            if (!moved)
                return false;
            Update(this);
            ChangePlayers();
            return true;
        }

        public bool AddPlayer(IPlayer player)
        {
            if (Players.Count < 2 && !Players.Contains(player))
            {
                Players.Add(player);
                player.Game = this;
                return true;
            }
            return false;
        }


        private void ChangePlayers()
        {
            if (CurrentPlayer == Players[0])
                CurrentPlayer = Players[1];
            else
                CurrentPlayer = Players[0];
            Update(this);
        }
        public void Start()
        {
            CurrentPlayer = Players.First(p => p.PlayerType == PlayerType.White);
            Update(this);
        }
        private void CreatePieces()
        {

            //black
            Pieces.Add(new Rook(ChessBoard[0, 0], PlayerType.Black));
            Pieces.Add(new Knight(ChessBoard[1, 0], PlayerType.Black));
            Pieces.Add(new Bishop(ChessBoard[2, 0], PlayerType.Black));
            Pieces.Add(new King(ChessBoard[3, 0], PlayerType.Black));
            Pieces.Add(new Queen(ChessBoard[4, 0], PlayerType.Black));
            Pieces.Add(new Bishop(ChessBoard[5, 0], PlayerType.Black));
            Pieces.Add(new Knight(ChessBoard[6, 0], PlayerType.Black));
            Pieces.Add(new Rook(ChessBoard[7, 0], PlayerType.Black));

            for (int i = 0; i < SIZE; i++)
            {
                Pieces.Add(new Pawn(ChessBoard[i, 1], PlayerType.Black));
            }

            //white
            Pieces.Add(new Rook(ChessBoard[0, 7], PlayerType.White));
            Pieces.Add(new Knight(ChessBoard[1, 7], PlayerType.White));
            Pieces.Add(new Bishop(ChessBoard[2, 7], PlayerType.White));
            Pieces.Add(new King(ChessBoard[4, 7], PlayerType.White));
            Pieces.Add(new Queen(ChessBoard[3, 7], PlayerType.White));
            Pieces.Add(new Bishop(ChessBoard[5, 7], PlayerType.White));
            Pieces.Add(new Knight(ChessBoard[6, 7], PlayerType.White));
            Pieces.Add(new Rook(ChessBoard[7, 7], PlayerType.White));

            for (int i = 0; i < SIZE; i++)
            {
                Pieces.Add(new Pawn(ChessBoard[i, 6], PlayerType.White));
            }

        }
        private void DestroyPiece(Piece piece, Cell nextCell)
        {
            Piece pieceToDestroy = null;
            if (nextCell.Piece != null &&
                nextCell.Piece.PlayerType != CurrentPlayer.PlayerType)
            {
                pieceToDestroy = nextCell.Piece;
            }
            bool canMoveTo = piece.CanMoveTo(nextCell, CurrentPlayer);
            if (pieceToDestroy != null && canMoveTo)
            {
                Board.Instance.DestroyPiece(pieceToDestroy);
            }
        }

        #region IObservable
        private List<IObserver<Game>> _observers;
        public IDisposable Subscribe(IObserver<Game> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber<Game>(_observers, observer);
        }

        public void Update(Game loc)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(loc);
            }
        }
        public void EndUpdates()
        {
            foreach (var observer in _observers)
            {
                if (_observers.Contains(observer))
                    observer.OnCompleted();

                _observers.Clear();
            }
        }
        #endregion
    }
}
