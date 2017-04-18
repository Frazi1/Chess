using chesslib;
using chesslib.Player;
using chesslib.Strategy;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessUI.ViewModel
{
    public class RealPlayerViewModel : ViewModelBase, IObserver<RealPlayer>
    {
        private GameViewModel _gameViewModel;
        public RealPlayer Player { get; set; }
        public RealPlayerViewModel(RealPlayer player, GameViewModel gvm)
        {
            _gameViewModel = gvm;
            Player = player;
            Subcribe(Player);
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(RealPlayer value)
        {
            _gameViewModel.ActivePlayerViewModel = this;
        }

        public void PushStrategy()
        {
            Player.Strategy = new RealPlayerStrategy(_gameViewModel.SelectedPiece.Piece,
                _gameViewModel.NextCell);
            Player.MakeMove();
        }
        #region IObserver
        private IDisposable unsubscriber;
        public void Subcribe(IObservable<RealPlayer> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }
        #endregion
    }
}
