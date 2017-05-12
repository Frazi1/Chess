using chesslib;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessUI.ViewModel
{
    public class ChessPieceViewModel : ViewModelBase
    {
        private Piece _piece;
        private PieceType _pieceType;

        public ChessPieceViewModel(Piece piece, Game game)
        {
            _piece = piece;
            PieceType = piece.PieceType;
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
        public PieceType PieceType
        {
            get { return this._pieceType; }
            set { this._pieceType = value; RaisePropertyChanged(() => this.PieceType); }
        }
        public PlayerColor PlayerType
        {
            get { return _piece.PlayerColor; }
            //set { _piece.PlayerColor = value; RaisePropertyChanged(() => _piece.PlayerColor); }
        }
        public bool IsInGame { get { return _piece.IsInGame; } }

        private void Game_GameStateChanged(object sender, chesslib.Events.GameStateChangedEventArgs e)
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
