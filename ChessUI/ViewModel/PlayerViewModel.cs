using chesslib.Command;
using chesslib.Field;
using chesslib.Player;
using GalaSoft.MvvmLight;

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
                Player.MakeMoveCommand = new MakeMoveCommand(Player.PlayerColor, 
                    new Move(_gameViewModel.SelectedPiece.PosX,
                    _gameViewModel.SelectedPiece.PosY,
                    _gameViewModel.NextCell.PosX,
                    _gameViewModel.NextCell.PosY));
        }

    }
}
