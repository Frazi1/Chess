using chesslib.Command;
using chesslib.Field;
using chesslib.Memento;
using chesslib.Player;
using chesslib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace chesslib
{
    public class Game : IObservable<Game>, IOriginator<MakeMoveCommand>
    {
        private const int SIZE = 8;

        public GameUtils GameUtils { get; private set; }

        public List<IPlayer> Players { get; private set; }
        public Board Board { get; private set; }

        public bool IsPaused { get; private set; }
        public bool IsGameFinished { get; private set; }

        private MakeMoveCommand _prevMoveCommand;

        private IPlayer _currentPlayer;
        public IPlayer CurrentPlayer
        {
            get { return _currentPlayer; }
            private set
            {
                if (_currentPlayer != value)
                {
                    _currentPlayer = value;
                    Board.CurrentPlayerType = value.PlayerType;
                    if (!IsPaused && !IsGameFinished)
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
            IsPaused = true;
            GameUtils = new GameUtils(this);
        }

        //public bool MakeMove(Piece piece, Cell nextCell, IPlayer player)
        //{
        //    if (IsPaused)
        //        return false;
        //    if (CurrentPlayer != player)
        //        return false;
        //    GameUtils.SaveState();
        //    DestroyPiece(piece, nextCell);
        //    bool moved = piece.MoveTo(nextCell, player);
        //    if (!moved)
        //        return false;

        //    Update(this);
        //    ChangePlayers();
        //    return true;
        //}
        public void LoadPreviousState()
        {
            IsPaused = true;
            GameUtils.LoadPreviousState();
            //CurrentPlayer = Players.First(p => p.PlayerType == Board.CurrentPlayerType);
            
            Update(this);

        }
        public bool AddPlayer(IPlayer player)
        {
            if (Players.Count < 2 && !Players.Contains(player))
            {
                Players.Add(player);
                player.Game = this;
                player.MoveDone += Player_MoveDone;
                return true;
            }
            return false;
        }

        private void Player_MoveDone(object sender, MoveDoneEventArgs e)
        {
            e.MoveCommand.Execute(this);
            _prevMoveCommand = e.MoveCommand;
            GameUtils.SaveState();
        }

        public void Start()
        {
            IsPaused = false;
            if (CurrentPlayer == null)
                CurrentPlayer = Players.First(p => p.PlayerType == PlayerType.White);
            else
                CurrentPlayer.OnNext(true);
            Update(this);
            
        }

        internal void ChangePlayers()
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

        #region Memento
        public Memento<MakeMoveCommand> GetMemento()
        {
            return new Memento<MakeMoveCommand>(_prevMoveCommand);
        }

        public void SetMemento(Memento<MakeMoveCommand> value)
        {
            value.GetState().Undo(this);
            IsPaused = true;
        }
        #endregion
    }
}
