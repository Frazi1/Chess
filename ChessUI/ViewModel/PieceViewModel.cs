using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ChessUI
{
    public enum PlayerType
    {
        Black,
        White
    }
    public enum PieceType
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King
    }
    public class PieceViewModel : ViewModelBase
    {
        private Point _pos;

        public Point Pos
        {
            get { return _pos; }
            set { _pos = value; RaisePropertyChanged(() => this.Pos); }
        }

        private PieceType _pieceType;

        public PieceType PieceType
        {
            get { return _pieceType; }
            set { _pieceType = value; RaisePropertyChanged(() => this.PieceType); }
        }

        private PlayerType _playerType;

        public PlayerType PlayerType
        {
            get { return _playerType; }
            set { _playerType = value; RaisePropertyChanged(() => this.PlayerType); }
        }



    }
}
