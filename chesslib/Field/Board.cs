using chesslib.Figures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace chesslib.Field
{
    [Serializable]
    public class Board
    {
        private readonly int _size;

        public Cell[,] ChessBoard { get; private set; }
        public List<Piece> AlivePieces { get; private set; }

        public bool IsPaused { get; internal set; }
        public bool IsGameFinished { get; internal set; }

        public Board(int size)
        {
            _size = size;
            ChessBoard = new Cell[size, size];
            AlivePieces = new List<Piece>();
            IsGameFinished = false;
            IsPaused = true;
            Initialize();
            SetUpPieces();
        }

        private void Initialize()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    ChessBoard[i, j] = new Cell(i, j);
                }
            }
        }
        private void SetUpPieces()
        {

            //black
            AlivePieces.Add(new Rook(ChessBoard[0, 0], PlayerColor.Black, this));
            AlivePieces.Add(new Knight(ChessBoard[1, 0], PlayerColor.Black, this));
            AlivePieces.Add(new Bishop(ChessBoard[2, 0], PlayerColor.Black, this));
            AlivePieces.Add(new King(ChessBoard[3, 0], PlayerColor.Black, this));
            AlivePieces.Add(new Queen(ChessBoard[4, 0], PlayerColor.Black, this));
            AlivePieces.Add(new Bishop(ChessBoard[5, 0], PlayerColor.Black, this));
            AlivePieces.Add(new Knight(ChessBoard[6, 0], PlayerColor.Black, this));
            AlivePieces.Add(new Rook(ChessBoard[7, 0], PlayerColor.Black, this));

            for (int i = 0; i < _size; i++)
            {
                AlivePieces.Add(new Pawn(ChessBoard[i, 1], PlayerColor.Black, this));
            }

            //white
            AlivePieces.Add(new Rook(ChessBoard[0, 7], PlayerColor.White, this));
            AlivePieces.Add(new Knight(ChessBoard[1, 7], PlayerColor.White, this));
            AlivePieces.Add(new Bishop(ChessBoard[2, 7], PlayerColor.White, this));
            AlivePieces.Add(new King(ChessBoard[4, 7], PlayerColor.White, this));
            AlivePieces.Add(new Queen(ChessBoard[3, 7], PlayerColor.White, this));
            AlivePieces.Add(new Bishop(ChessBoard[5, 7], PlayerColor.White, this));
            AlivePieces.Add(new Knight(ChessBoard[6, 7], PlayerColor.White, this));
            AlivePieces.Add(new Rook(ChessBoard[7, 7], PlayerColor.White, this));

            for (int i = 0; i < _size; i++)
            {
                AlivePieces.Add(new Pawn(ChessBoard[i, 6], PlayerColor.White, this));
            }

        }

        internal void Start()
        {
            UpdatePiecesAndCells();
            IsPaused = false;
        }

        private void UpdatePiecesAndCells()
        {
            UpdateAttackedCells();
            UpdatePiecesMoves();
        }
        public void UpdateAttackedCells()
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
                p.GetAttackedCells();
                p.AttackedCells.ForEach(x => x.AttackersList.Add(p));
            }
        }
        public void UpdatePiecesMoves()
        {
            foreach (var p in AlivePieces)
            {
                p.SetAllowedMoves();
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
        //public Piece MovePiece(Piece piece, Cell nextCell, bool updateMoves, bool updateAttacked)
        //{
        //    Piece destroyedPiece = nextCell.Piece;
        //    if (destroyedPiece != null)
        //        DestroyPiece(destroyedPiece);
        //    piece.MoveTo(nextCell);
        //    if (updateMoves)
        //        UpdatePiecesMoves();
        //    if (updateAttacked)
        //        UpdateAttackedCells();
        //    return destroyedPiece;
        //}
        /// <summary>
        /// Moves the piece from its current cell to the nextCell, updates lists of attacked cells and allowed moves (accordingly to specified moveFlags)
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="nextCell"></param>
        /// <param name="moveFlags"></param>
        /// <returns>Return the piece that was destroyed after move (can be null if no piece destroyed)</returns>
        public Piece MovePiece(Piece piece, Cell nextCell, MoveFlags moveFlags)
        {
            Piece destroyedPiece = nextCell.Piece;
            if (destroyedPiece != null)
                DestroyPiece(destroyedPiece);
            piece.MoveTo(nextCell);
            if (moveFlags.HasFlag(MoveFlags.UpdateMoves))
                UpdatePiecesMoves();
            if (moveFlags.HasFlag(MoveFlags.UpdateAttacked))
                UpdateAttackedCells();
            return destroyedPiece;
        }

        public List<Piece> GetAlivePieces(PlayerColor playerColor)
        {
            return AlivePieces
                .Where(p => p.PlayerType == playerColor)
                .ToList();
        }
        public Cell GetCell(int x, int y)
        {
            return ChessBoard[x, y];
        }
        public Piece GetPiece(int x, int y)
        {
            return GetCell(x, y).Piece;
        }
    }

    [Flags]
    public enum MoveFlags
    {
        None = 1,
        UpdateAttacked = 2,
        UpdateMoves = 4
    }
}
