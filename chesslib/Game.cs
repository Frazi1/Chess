using chesslib.Field;
using chesslib.Figures;
using chesslib.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace chesslib
{
    public class Game : IObservable<Game>
    {
        private const int SIZE = 8;

        public List<IPlayer> Players { get; set; }
        public Board Board { get; set; }

        public bool IsGameStarted { get; private set; }
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
                    if (IsGameStarted && !IsGameFinished)
                        _currentPlayer.OnNext(true);
                }
            }
        }

        public Game()
        {
            Players = new List<IPlayer>();
            _observers = new List<IObserver<Game>>();
            Board = new Board(SIZE);
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
            //Thread.Sleep(500);
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
        public void Start()
        {
            IsGameStarted = true;
            CurrentPlayer = Players.First(p => p.PlayerType == PlayerType.White);
            Update(this);
        }

        private void ChangePlayers()
        {
            if (CurrentPlayer == Players[0])
                CurrentPlayer = Players[1];
            else
                CurrentPlayer = Players[0];
            Update(this);
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
                Board.DestroyPiece(pieceToDestroy);
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
