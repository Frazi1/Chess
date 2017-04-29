using chesslib;
using chesslib.Command;
using chesslib.Memento;
using chesslib.Player;
using chesslib.Strategy;
using ChessUI;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ChessUI.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        private ObservableCollection<ChessPieceViewModel> _chessPieces;
        private Game _game;

        public GameViewModel()
        {
            ChessPiecesViewModels = new ObservableCollection<ChessPieceViewModel>();
            RealPlayersViewModels = new ObservableCollection<RealPlayerViewModel>();
            MementoStates = new ObservableCollection<Memento<MakeMoveCommand>>();
            Game = new Game();

            //TODO: передалать
            RealPlayer p1 = new RealPlayer(PlayerType.White);
            RealPlayer p2 = new RealPlayer(PlayerType.Black);
            //ComputerPlayer p1 = new ComputerPlayer(PlayerType.White);
            //ComputerPlayer p2 = new ComputerPlayer(PlayerType.Black);
            Game.AddPlayer(p1);
            Game.AddPlayer(p2);
            //p1.Strategy = new DefaultComputerStrategy(p1);
            //p2.Strategy = new DefaultComputerStrategy(p2);


            RealPlayersViewModels.Add(new RealPlayerViewModel(p1, this));
            RealPlayersViewModels.Add(new RealPlayerViewModel(p2, this));

            //

            InitializePieces();
            Game.GameStateChanged += Game_GameStateChanged;

            //Commands
        }

        public ObservableCollection<ChessPieceViewModel> ChessPiecesViewModels
        {
            get { return _chessPieces; }
            set { _chessPieces = value; }
        }
        public ObservableCollection<RealPlayerViewModel> RealPlayersViewModels { get; set; }
        public ObservableCollection<Memento<MakeMoveCommand>> MementoStates { get; set; }
        public RealPlayerViewModel ActivePlayerViewModel { get; set; }
        public ChessPieceViewModel SelectedPiece { get; set; }

        public Cell NextCell { get; set; }
        public PlayerType PlayerType
        {
            get
            {
                if (Game.CurrentPlayer != null)
                    return Game.CurrentPlayer.PlayerType;
                return PlayerType.None;
            }
        }
        public Game Game
        {
            get { return _game; }
            set { _game = value; }
        }
        public bool CanUndo { get { return Game.GameUtils.Memento.MementoList.Count > 0; } }
        public bool IsPaused { get { return Game.IsPaused; } }

        public void InitializePieces()
        {
            ChessPiecesViewModels.Clear();
            foreach (var item in Game.Board.AlivePieces)
            {
                ChessPiecesViewModels.Add(new ChessPieceViewModel(item, Game));
            }
        }

        private void Game_GameStateChanged(object sender, chesslib.Events.GameStateChangedEventArgs e)
        {
            RaisePropertyChanged(() => PlayerType);
            RaisePropertyChanged(() => CanUndo);
            RaisePropertyChanged(() => IsPaused);

            if (RealPlayersViewModels.Count > 0)
                ActivePlayerViewModel = RealPlayersViewModels
                    .FirstOrDefault(p => p.Player.PlayerType == Game.CurrentPlayer.PlayerType);
            if (e.IsCheckMate)
                MessageBox.Show("Checkmate");
            else if (e.IsCheck)
                MessageBox.Show("Check");
        }
    }
}

