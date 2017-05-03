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
    public class Game
    {
        private const int SIZE = 8;
        private MoveCommands _moveCommands;
        private IPlayer _currentPlayer;
        private List<IPlayer> _players;
        private Board _board;
        private GameUtils _gameUtils;

        public Board Board
        {
            get
            {
                return _board;
            }

            private set
            {
                _board = value;
            }
        }
        public bool IsPaused
        {
            get { return Board.IsPaused; }
            set { Board.IsPaused = value; }
        }
        public bool IsGameFinished { get { return Board.IsGameFinished; } }


        public IPlayer CurrentPlayer
        {
            get { return _currentPlayer; }
            internal set
            {
                if (_currentPlayer != value)
                {
                    _currentPlayer = value;
                }
            }
        }
        public MoveCommands MoveCommands
        {
            get
            {
                return _moveCommands;
            }

            internal set
            {
                _moveCommands = value;
            }
        }
        public List<IPlayer> Players
        {
            get
            {
                return _players;
            }

            private set
            {
                _players = value;
            }
        }
        public GameUtils GameUtils
        {
            get
            {
                return _gameUtils;
            }

            private set
            {
                _gameUtils = value;
            }
        }

        public event EventsDelegates.GameStateChangedEventHandler GameStateChanged;

        public Game()
        {
            Board = new Board(SIZE);
            GameUtils = new GameUtils(this);
            Players = new List<IPlayer>();
            _moveCommands = new MoveCommands();
        }

        public void AddPlayer(IPlayer player)
        {
            if (Players.Any(p => p.PlayerColor == player.PlayerColor))
                throw new ArgumentException("Игрок такого цвета уже добавлен");

            if (Players.Count < 2)
            {
                player.MoveDone += Player_MoveDone;
                //player.Game = this;
                Players.Add(player);
                return;
            }
            return;
        }
        public void Start()
        {
            Board.Start();
            if (CurrentPlayer == null)
                CurrentPlayer = Players.First(p => p.PlayerColor == PlayerColor.White);
            if (!IsPaused && !IsGameFinished)
                CurrentPlayer.DoTurn(this);
            RaiseGameStateChange();
        }
        public void LoadFromFile(string path)
        {
            IsPaused = true;
            GameUtils.LoadFromFile(path);
            foreach (var comm in MoveCommands)
            {
                comm.Execute(this);
                Board.UpdatePiecesAndCells();
                ChangeTurn();
            }
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

        public void RaiseGameStateChange()
        {
            if (GameStateChanged != null)
                GameStateChanged(this, new GameStateChangedEventArgs(this));
        }
        internal void ChangeTurn()
        {
            CurrentPlayer = CurrentPlayer == Players[0] ? Players[1] : Players[0];
            IsCheckMate();
            if (!Board.IsPaused)
                CurrentPlayer.DoTurn(this);
            RaiseGameStateChange();
        }

    }
}
