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
    public class ChessPieceViewModel : ViewModelBase, IObserver<Piece>
    {
        private Piece _piece;
        public Piece Piece { get { return _piece; } }
        public ChessPieceViewModel(Piece piece)
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
            Subcribe(_piece);
        }

        public int PosX
        {
            get { return IsInGame ? _piece.CurrentCell.PosX : -1; }
        }
        public int PosY
        {
            get { return IsInGame ? _piece.CurrentCell.PosY : -1; }
        }

        private PieceType _pieceType;
        public PieceType PieceType
        {
            get { return this._pieceType; }
            set { this._pieceType = value; RaisePropertyChanged(() => this.PieceType); }
        }

        public PlayerType PlayerType
        {
            get { return _piece.PlayerType; }
            set { _piece.PlayerType = value; RaisePropertyChanged(() => _piece.PlayerType); }
        }
        public bool IsInGame { get { return _piece.IsInGame; } }


        #region IObserver

        private IDisposable unsubscriber;
        public void Subcribe(IObservable<Piece> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }
        public void OnNext(Piece value)
        {
            RaisePropertyChanged(() => _piece.CurrentCell.PosY);
            RaisePropertyChanged(() => _piece.CurrentCell.PosX);
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
