﻿using chesslib;
using chesslib.Player;
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

        public ObservableCollection<ChessPieceViewModel> ChessPiecesViewModels
        {
            get { return _chessPieces; }
            set { _chessPieces = value; }
        }
        public ObservableCollection<RealPlayerViewModel> RealPlayersViewModels { get; set; }

        public Game Game
        {
            get { return _game; }
            set { _game = value; }
        }
        public ChessPieceViewModel SelectedPiece { get; set; }
        public Cell NextCell { get; set; }
        public RealPlayerViewModel ActivePlayerViewModel { get; set; }

        public PlayerType PlayerType
        {
            get
            {
                if (Game.CurrentPlayer != null)
                    return Game.CurrentPlayer.PlayerType;
                return PlayerType.None;
            }
        }


        public GameViewModel()
        {
            ChessPiecesViewModels = new ObservableCollection<ChessPieceViewModel>();
            RealPlayersViewModels = new ObservableCollection<RealPlayerViewModel>();

            //TODO: передалать
            RealPlayer p1 = new RealPlayer(PlayerType.White);
            RealPlayer p2 = new RealPlayer(PlayerType.Black);
            Game = new Game();
            Game.AddPlayer(p1);
            Game.AddPlayer(p2);

            RealPlayersViewModels.Add(new RealPlayerViewModel(p1, this));
            RealPlayersViewModels.Add(new RealPlayerViewModel(p2,this));

            //

            InitializePieces();
            Subcribe(Game);
            Game.Start();

            //Commands
        }

        private void InitializePieces()
        {
            foreach (var item in Game.Pieces)
            {
                ChessPiecesViewModels.Add(new ChessPieceViewModel(item));
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
    }
}
