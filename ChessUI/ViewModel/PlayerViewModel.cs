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
    public class PlayerViewModel : ViewModelBase
    {
        private readonly GameViewModel _gameViewModel;
        public IPlayer Player { get; private set; }
        public PlayerViewModel(IPlayer player, GameViewModel gvm)
        {
            _gameViewModel = gvm;
            Player = player;
        }
        public void PushCommand()
        {
            //if (!_gameViewModel.IsPaused)
                Player.MakeMoveCommand = new chesslib.Command.MakeMoveCommand(Player.PlayerColor, 
                    _gameViewModel.SelectedPiece.PosX,
                    _gameViewModel.SelectedPiece.PosY,
                    _gameViewModel.NextCell.PosX,
                    _gameViewModel.NextCell.PosY);
        }

    }
}
