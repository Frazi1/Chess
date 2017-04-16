using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace chesslib.Field
{
    public class Board
    {
        private readonly int SIZE;

        public Cell[,] ChessBoard { get; set; }
        public List<Piece> AlivePieces { get; set; }
        public List<Piece> DestroyedPieces { get; set; }


        private Board(int size)
        {
            SIZE = size;
            ChessBoard = new Cell[size, size];
            AlivePieces = new List<Piece>();
            DestroyedPieces = new List<Piece>();

            InitializeBoard();
        }

        private static Board _instance;
        public static Board Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Board(8);
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    ChessBoard[i, j] = new Cell(i, j);
                }
            }
        }

        public void DestroyPiece(Piece piece)
        {
            if(AlivePieces.Contains(piece))
            {
                AlivePieces.Remove(piece);
                piece.Destroy();
                DestroyedPieces.Add(piece);
            }
        }
        
    }
}
