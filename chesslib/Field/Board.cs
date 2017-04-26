using chesslib.Figures;
using chesslib.Memento;
using chesslib.Player;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace chesslib.Field
{
    [Serializable]
    public class Board
    {
        private readonly int SIZE;
        private IPlayer _currentPlayer;

        public Cell[,] ChessBoard { get; set; }
        public List<Piece> AlivePieces { get; private set; }
        public List<IPlayer> Players { get; private set; }
        public IPlayer CurrentPlayer
        {
            get { return _currentPlayer; }
            private set
            {
                if (_currentPlayer != value)
                {
                    _currentPlayer = value;
                    if (!IsPaused && !IsGameFinished)
                        _currentPlayer.DoTurn();
                }
            }
        }
        public bool IsPaused { get; set; }
        public bool IsGameFinished { get; private set; }

        public Board(int size)
        {
            SIZE = size;
            ChessBoard = new Cell[size, size];
            Players = new List<IPlayer>();
            AlivePieces = new List<Piece>();
            IsGameFinished = false;
            IsPaused = true;
            Initialize();
            SetUpPieces();
        }

        private void Initialize()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    ChessBoard[i, j] = new Cell(i, j);
                }
            }
        }

        public void SetUpPieces()
        {

            //black
            AlivePieces.Add(new Rook(ChessBoard[0, 0], PlayerType.Black, this));
            AlivePieces.Add(new Knight(ChessBoard[1, 0], PlayerType.Black, this));
            AlivePieces.Add(new Bishop(ChessBoard[2, 0], PlayerType.Black, this));
            AlivePieces.Add(new King(ChessBoard[3, 0], PlayerType.Black, this));
            AlivePieces.Add(new Queen(ChessBoard[4, 0], PlayerType.Black, this));
            AlivePieces.Add(new Bishop(ChessBoard[5, 0], PlayerType.Black, this));
            AlivePieces.Add(new Knight(ChessBoard[6, 0], PlayerType.Black, this));
            AlivePieces.Add(new Rook(ChessBoard[7, 0], PlayerType.Black, this));

            for (int i = 0; i < SIZE; i++)
            {
                AlivePieces.Add(new Pawn(ChessBoard[i, 1], PlayerType.Black, this));
            }

            //white
            AlivePieces.Add(new Rook(ChessBoard[0, 7], PlayerType.White, this));
            AlivePieces.Add(new Knight(ChessBoard[1, 7], PlayerType.White, this));
            AlivePieces.Add(new Bishop(ChessBoard[2, 7], PlayerType.White, this));
            AlivePieces.Add(new King(ChessBoard[4, 7], PlayerType.White, this));
            AlivePieces.Add(new Queen(ChessBoard[3, 7], PlayerType.White, this));
            AlivePieces.Add(new Bishop(ChessBoard[5, 7], PlayerType.White, this));
            AlivePieces.Add(new Knight(ChessBoard[6, 7], PlayerType.White, this));
            AlivePieces.Add(new Rook(ChessBoard[7, 7], PlayerType.White, this));

            for (int i = 0; i < SIZE; i++)
            {
                AlivePieces.Add(new Pawn(ChessBoard[i, 6], PlayerType.White, this));
            }

        }
        public void UpdatePiecesAndCells()
        {
            //Clear attackers lists
            for (int i = 0; i < ChessBoard.GetLength(0); i++)
            {
                for (int j = 0; j < ChessBoard.GetLength(1); j++)
                {
                    var cell = ChessBoard[i, j];
                    cell.AttackersList.Clear();
                }
            }
            foreach (var p in AlivePieces)
            {
                p.SetAllowedMoves();
                p.AttackedCells.ForEach(x => x.AttackersList.Add(p));
            }
        }
        public void DestroyPiece(Piece piece)
        {
            if (AlivePieces.Contains(piece))
            {
                AlivePieces.Remove(piece);
                piece.IsInGame = false;
                piece.CurrentCell = null;
            }
        }

        internal void ChangeTurn()
        {
            if (CurrentPlayer == Players[0])
                CurrentPlayer = Players[1];
            else
                CurrentPlayer = Players[0];
            UpdatePiecesAndCells();
        }
        internal void Start()
        {
            UpdatePiecesAndCells();
            IsPaused = false;
            if (CurrentPlayer == null)
                CurrentPlayer = Players.First(p => p.PlayerType == PlayerType.White);
            else
                CurrentPlayer.DoTurn();
        }
        internal bool AddPlayer(IPlayer player)
        {
            if (Players.Count < 2 && !Players.Contains(player))
            {
                Players.Add(player);
                return true;
            }
            return false;
        }
    }
}
