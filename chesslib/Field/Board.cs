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


        public Board(int size)
        {
            SIZE = size;
            ChessBoard = new Cell[size, size];
            AlivePieces = new List<Piece>();
            DestroyedPieces = new List<Piece>();

            Initialize();
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
