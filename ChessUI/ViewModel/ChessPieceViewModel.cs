using chesslib;
using chesslib.Events;
using chesslib.Field.Bit;
using chesslib.Field.Smart.Pieces;
using GalaSoft.MvvmLight;

namespace ChessUI.ViewModel
{
    public class ChessPieceViewModel : ViewModelBase
    {
        private Piece _piece;
        private EnumPiece _pieceType;

        public ChessPieceViewModel(Piece piece, Game game)
        {
            _piece = piece;
            EnumPiece = piece.PieceType;
            game.GameStateChanged += Game_GameStateChanged;
        }

        public int PosX
        {
            get { return IsInGame ? _piece.CurrentCell.PosX : -1; }
        }
        public int PosY
        {
            get { return IsInGame ? _piece.CurrentCell.PosY : -1; }
        }
        public Piece Piece { get { return _piece; } }
        public EnumPiece EnumPiece
        {
            get { return _pieceType; }
            set { _pieceType = value; RaisePropertyChanged(() => EnumPiece); }
        }
        public PlayerColor PlayerType
        {
            get { return _piece.PlayerColor; }
            //set { _piece.PlayerColor = value; RaisePropertyChanged(() => _piece.PlayerColor); }
        }
        public bool IsInGame { get { return _piece.IsInGame; } }

        private void Game_GameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            UpdatePiece();
        }

        public void UpdatePiece()
        {
            RaisePropertyChanged(() => _piece.CurrentCell.PosY);
            RaisePropertyChanged(() => _piece.CurrentCell.PosX);
        }
    }
}
