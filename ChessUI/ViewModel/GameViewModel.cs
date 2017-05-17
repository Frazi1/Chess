using chesslib;
using chesslib.Command;
using chesslib.Player;
using chesslib.Strategy;
using chesslib.Utils;
using ChessUI.Command;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using chesslib.Board;

namespace ChessUI.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        private ObservableCollection<ChessPieceViewModel> _chessPieces;
        private Game _game;
        private string _path;

        public GameViewModel()
        {
            ChessPiecesViewModels = new ObservableCollection<ChessPieceViewModel>();
            PlayersViewModels = new ObservableCollection<PlayerViewModel>();
            MoveCommands = new ObservableCollection<MakeMoveCommand>();
            Commands = new Commands(this);
        }

        public Commands Commands { get; set; }
        public string Path
        {
            get
            {
                return _path;
            }

            set
            {
                _path = value;
                RaisePropertyChanged(() => Path);
            }
        }

        public ObservableCollection<ChessPieceViewModel> ChessPiecesViewModels
        {
            get { return _chessPieces; }
            set { _chessPieces = value; }
        }
        public ObservableCollection<PlayerViewModel> PlayersViewModels { get; set; }
        public ObservableCollection<MakeMoveCommand> MoveCommands { get; set; }
        public PlayerViewModel ActivePlayerViewModel { get; set; }
        public ChessPieceViewModel SelectedPiece { get; set; }

        public Cell NextCell { get; set; }
        public PlayerColor PlayerType
        {
            get
            {
                
                if (Game!=null && Game.CurrentPlayer != null)
                    return Game.CurrentPlayer.PlayerColor;
                return PlayerColor.None;
            }
        }
        public Game Game
        {
            get { return _game; }
            set { _game = value; RaisePropertyChanged(() => Game); }
        }
        public bool CanUndo { get { return Game != null ? Game.MoveCommands.Count > 0 : false; } }
        public bool IsPaused { get { return Game != null ? Game.IsPaused : true; } }

        private void Game_GameStateChanged(object sender, chesslib.Events.GameStateChangedEventArgs e)
        {
            RaisePropertyChanged(() => PlayerType);
            RaisePropertyChanged(() => CanUndo);
            RaisePropertyChanged(() => IsPaused);

            if (PlayersViewModels.Count > 0)
                ActivePlayerViewModel = PlayersViewModels
                    .FirstOrDefault(p => p.Player.PlayerColor == Game.CurrentPlayer.PlayerColor);
            if (e.IsCheckMate)
                MessageBox.Show("Checkmate");
            else if (e.IsCheck)
                MessageBox.Show("Check");
        }

        public void Initialize(Game game)
        {
            Reset();
            Game = game;
            Game.Players.ForEach(p => PlayersViewModels.Add(new PlayerViewModel(p, this)));

            InitializePieces();
            Game.GameStateChanged += Game_GameStateChanged;
            Game.Start();
        }

        private void Reset()
        {
            Game = null;
            ChessPiecesViewModels.Clear();
            PlayersViewModels.Clear();
            MoveCommands.Clear();
        }
        private void InitializePieces()
        {
            ChessPiecesViewModels.Clear();
            foreach (var item in Game.Board.AlivePieces)
            {
                var chessPieceViewModel = new ChessPieceViewModel(item, Game);
                ChessPiecesViewModels.Add(chessPieceViewModel);
                //chessPieceViewModel.UpdatePiece();
            }
        }
    }
}

