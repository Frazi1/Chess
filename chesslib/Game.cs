using chesslib.Command;
using chesslib.Events;
using chesslib.Field;
using chesslib.Memento;
using chesslib.Player;
using chesslib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace chesslib
{
    public class Game : IOriginator<MakeMoveCommand>
    {
        private const int SIZE = 8;
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
                        _currentPlayer.DoTurn();
                }
            }
        }
        public GameUtils GameUtils { get; private set; }

        public List<IPlayer> Players { get; private set; }
        public Board Board { get; private set; }

        public bool IsPaused { get; private set; }
        public bool IsGameFinished { get; private set; }

        public event EventsDelegates.GameStateChangedEventHandler GameStateChanged;

        public Game()
        {
            Players = new List<IPlayer>();
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
            
            Update();

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
            if (e.MoveCommand.CanExecute(this))
            {
                e.MoveCommand.Execute(this);
                _prevMoveCommand = e.MoveCommand;
                GameUtils.SaveState();
            }
        }

        public void Start()
        {
            IsPaused = false;
            if (CurrentPlayer == null)
                CurrentPlayer = Players.First(p => p.PlayerType == PlayerType.White);
            else
                CurrentPlayer.DoTurn();
            Update();
            
        }

        internal void ChangePlayers()
        {
            if (CurrentPlayer == Players[0])
                CurrentPlayer = Players[1];
            else
                CurrentPlayer = Players[0];
            Update();
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

        public void Update()
        {
            if (GameStateChanged != null)
                GameStateChanged(this, new GameStateChangedEventArgs(this));
        }


        #region Memento
        public Memento<MakeMoveCommand> GetMemento()
        {
            return new Memento<MakeMoveCommand>(_prevMoveCommand);
        }

        public void SetMemento(Memento<MakeMoveCommand> value)
        {
            value.GetState().Undo(this);
            //_prevMoveCommand = null;
        }
        #endregion
    }
}
