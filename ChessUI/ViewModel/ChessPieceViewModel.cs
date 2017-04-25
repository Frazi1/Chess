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
            switch (piece.GetType().Name)
            {
                case "Pawn":
                    PieceType = PieceType.Pawn;
                    break;
                case "Knight":
                    PieceType = PieceType.Knight;
                    break;
                case "Bishop":
                    PieceType = PieceType.Bishop;
                    break;
                case "Rook":
                    PieceType = PieceType.Rook;
                    break;
                case "Queen":
                    PieceType = PieceType.Queen;
                    break;
                case "King":
                    PieceType = PieceType.King;
                    break;
                default:
                    break;
            }
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
        public PlayerType PlayerType
        {
            get { return _piece.PlayerType; }
            //set { _piece.PlayerType = value; RaisePropertyChanged(() => _piece.PlayerType); }
        }
        public bool IsInGame { get { return _piece.IsInGame; } }

        private void Game_GameStateChanged(object sender, chesslib.Events.GameStateChangedEventArgs e)
        {
            RaisePropertyChanged(() => _piece.CurrentCell.PosY);
            RaisePropertyChanged(() => _piece.CurrentCell.PosX);
        }

    }
}
