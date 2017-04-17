using chesslib;
using ChessUI;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ChessUI.ViewModel
{
    public class GameViewModel : ViewModelBase, IObserver<Game>
    {
        private ObservableCollection<ChessPieceViewModel> _chessPieces;
        private Game _game;

        public ObservableCollection<ChessPieceViewModel> ChessPieces
        {
            get { return _chessPieces; }
            set { _chessPieces = value; }
        }
        public Game Game
        {
            get { return _game; }
            set { _game = value; }
        }
        public ChessPieceViewModel SelectedPiece { get; set; }
        public PlayerType PlayerType
        {
            get { return Game.CurrentPlayer.PlayerType; }
        }

        public GameViewModel()
        {
            Game = new Game();
            ChessPieces = new ObservableCollection<ChessPieceViewModel>();
            InitializePieces();
            Subcribe(Game);

            //Commands
        }

        private void InitializePieces()
        {
            foreach (var item in Game.Pieces)
            {
                ChessPieces.Add(new ChessPieceViewModel(item));
            }
        }

        #region IObserver
        private IDisposable unsubscriber;
        public void Subcribe(IObservable<Game> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }
        public void OnNext(Game value)
        {
            RaisePropertyChanged(() => PlayerType);
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Commands
        public ICommand MakeMoveCommand { get; private set; }
        #endregion
    }
}

