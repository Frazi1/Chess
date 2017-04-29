using chesslib.Command;
using chesslib.Events;
using chesslib.Field;
using chesslib.Player;
using chesslib.Utils;
using System.Collections.Generic;
using System.Linq;
using System;

namespace chesslib
{
    [Serializable]
    public class Game
    {
        private const int SIZE = 8;
        private List<MakeMoveCommand> _moveCommands;
        private IPlayer _currentPlayer;

        public GameUtils GameUtils { get; private set; }

        public Board Board { get; private set; }
        public bool IsPaused
        {
            get { return Board.IsPaused; }
            set { Board.IsPaused = value; }
        }
        public bool IsGameFinished { get { return Board.IsGameFinished; } }
        public List<IPlayer> Players { get; private set; }
        public IPlayer CurrentPlayer
        {
            get { return _currentPlayer; }
            private set
            {
                if (_currentPlayer != value)
                {
                    _currentPlayer = value;
                }
            }
        }
        public List<MakeMoveCommand> MoveCommands
        {
            get
            {
                return _moveCommands;
            }

            set
            {
                _moveCommands = value;
            }
        }

        public event EventsDelegates.GameStateChangedEventHandler GameStateChanged;

        public Game()
        {
            Board = new Board(SIZE);
            GameUtils = new GameUtils(this);
            Players = new List<IPlayer>();
            MoveCommands = new List<MakeMoveCommand>();
        }

        public bool AddPlayer(IPlayer player)
        {
            if (Players.Count < 2 && !Players.Contains(player))
            {
                player.MoveDone += Player_MoveDone;
                player.Game = this;
                Players.Add(player);
                return true;
            }
            return false;
        }
        public void Start()
        {
            Board.Start();
            if (CurrentPlayer == null)
                CurrentPlayer = Players.First(p => p.PlayerColor == PlayerColor.White);
            if (!IsPaused && !IsGameFinished)
                CurrentPlayer.DoTurn();
            RaiseGameStateChange();
        }

        private void Player_MoveDone(object sender, MoveDoneEventArgs e)
        {
            if (e.MoveCommand.CanExecute(this))
            {
                e.MoveCommand.Execute(this);
                GameUtils.SaveState(e.MoveCommand);
                Board.UpdatePiecesAndCells();
                ChangeTurn();
            }
        }
        private bool IsCheckMate()
        {
            bool checkMate = Board.AlivePieces.Where(p => p.PlayerType == CurrentPlayer.PlayerColor)
                   .All(p => p.AllowedCells.Count == 0);
            if (checkMate)
            {
                Board.IsGameFinished = true;
                Board.IsPaused = true;
            }
            else
            {
                Board.IsGameFinished = false;
            }
            return checkMate;

        }

        internal void RaiseGameStateChange()
        {
            if (GameStateChanged != null)
                GameStateChanged(this, new GameStateChangedEventArgs(this));
        }
        internal void ChangeTurn()
        {
            if (CurrentPlayer == Players[0])
                CurrentPlayer = Players[1];
            else
                CurrentPlayer = Players[0];
            IsCheckMate();
            if (!Board.IsPaused)
                CurrentPlayer.DoTurn();
            RaiseGameStateChange();
        }
    }
}
