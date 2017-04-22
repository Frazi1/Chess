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
    public class RealPlayerViewModel : ViewModelBase
    {
        private GameViewModel _gameViewModel;
        public RealPlayer Player { get; set; }
        public RealPlayerViewModel(RealPlayer player, GameViewModel gvm)
        {
            _gameViewModel = gvm;
            Player = player;
            Player.MovingInProcess += Player_MovingInProcess;
        }

        private void Player_MovingInProcess(object sender, chesslib.Events.MovingInProcessEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void PushStrategy()
        {
            Player.Strategy = new RealPlayerStrategy(_gameViewModel.SelectedPiece.Piece,
                _gameViewModel.NextCell);
            Player.MakeMove();
        }

    }
}
