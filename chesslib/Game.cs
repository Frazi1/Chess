﻿using chesslib.Field;
using chesslib.Figures;
using chesslib.Player;
using System;
using System.Collections.Generic;

namespace chesslib
{
    public class Game : IObservable<Game>
    {
        private const int SIZE = 8;

        public IPlayer Player1 { get; set; }
        public IPlayer Player2 { get; set; }
        public Board Board
        {
            get { return Board.Instance; }
        }

        private Cell[,] ChessBoard { get { return Board.ChessBoard; } }
        public List<Piece> Pieces { get { return Board.AlivePieces; } }
        public bool IsGameFinished { get; private set; }
        public IPlayer CurrentPlayer { get; private set; }

        public Game()
        {
            _observers = new List<IObserver<Game>>();
            CreatePieces();
            Player1 = new RealPlayer(PlayerType.White, this);
            Player2 = new RealPlayer(PlayerType.Black, this);
            IsGameFinished = false;
            Start();
        }

        public bool MakeMove(Piece piece, Cell nextCell, IPlayer player)
        {
            if (CurrentPlayer != player)
                return false;

            DestroyPiece(piece, nextCell);
            bool moved = piece.MoveTo(nextCell, player);

            if (!moved)
                return false;
            ChangePlayers();
            Update(this);
            return true;
        }



        private void ChangePlayers()
        {
            if (CurrentPlayer == Player1)
                CurrentPlayer = Player2;
            else
                CurrentPlayer = Player1;
        }
        private void Start()
        {
            CurrentPlayer = Player1;
        }
        private void CreatePieces()
        {

            //black
            Pieces.Add(new Rook(ChessBoard[0, 0], PlayerType.Black));
            Pieces.Add(new Knight(ChessBoard[1, 0], PlayerType.Black));
            Pieces.Add(new Bishop(ChessBoard[2, 0], PlayerType.Black));
            Pieces.Add(new King(ChessBoard[3, 0], PlayerType.Black));
            Pieces.Add(new Queen(ChessBoard[4, 0], PlayerType.Black));
            Pieces.Add(new Bishop(ChessBoard[5, 0], PlayerType.Black));
            Pieces.Add(new Knight(ChessBoard[6, 0], PlayerType.Black));
            Pieces.Add(new Rook(ChessBoard[7, 0], PlayerType.Black));

            for (int i = 0; i < SIZE; i++)
            {
                Pieces.Add(new Pawn(ChessBoard[i, 1], PlayerType.Black));
            }

            //white
            Pieces.Add(new Rook(ChessBoard[0, 7], PlayerType.White));
            Pieces.Add(new Knight(ChessBoard[1, 7], PlayerType.White));
            Pieces.Add(new Bishop(ChessBoard[2, 7], PlayerType.White));
            Pieces.Add(new King(ChessBoard[4, 7], PlayerType.White));
            Pieces.Add(new Queen(ChessBoard[3, 7], PlayerType.White));
            Pieces.Add(new Bishop(ChessBoard[5, 7], PlayerType.White));
            Pieces.Add(new Knight(ChessBoard[6, 7], PlayerType.White));
            Pieces.Add(new Rook(ChessBoard[7, 7], PlayerType.White));

            for (int i = 0; i < SIZE; i++)
            {
                Pieces.Add(new Pawn(ChessBoard[i, 6], PlayerType.White));
            }

        }
        private void DestroyPiece(Piece piece, Cell nextCell)
        {
            Piece pieceToDestroy = null;
            if (nextCell.Piece != null &&
                nextCell.Piece.PlayerType != CurrentPlayer.PlayerType)
            {
                pieceToDestroy = nextCell.Piece;
            }
            bool canMoveTo = piece.CanMoveTo(nextCell, CurrentPlayer);
            if (pieceToDestroy != null && canMoveTo)
            {
                Board.Instance.DestroyPiece(pieceToDestroy);
            }
        }

        #region IObservable
        private List<IObserver<Game>> _observers;
        public IDisposable Subscribe(IObserver<Game> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber<Game>(_observers, observer);
        }

        public void Update(Game loc)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(loc);
            }
        }
        public void EndUpdates()
        {
            foreach (var observer in _observers)
            {
                if (_observers.Contains(observer))
                    observer.OnCompleted();

                _observers.Clear();
            }
        }
        #endregion
    }
}
