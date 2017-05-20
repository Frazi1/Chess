using System;
using System.Collections.Generic;
using System.Linq;
using chesslib.Figures;

namespace chesslib.Field
{
    [Serializable]
    public class Board
    {
        public int Size { get; }

        protected Cell[,] ChessBoard { get; }
        public List<Piece> AlivePieces { get; }

        public bool IsPaused { get; internal set; }
        public bool IsGameFinished { get; internal set; }

        public Board(int size)
        {
            Size = size;
            ChessBoard = new Cell[size, size];
            AlivePieces = new List<Piece>();
            IsGameFinished = false;
            IsPaused = true;
            Initialize();
            SetUpPieces();
        }

        private void Initialize()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
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

            for (int i = 0; i < Size; i++)
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

            for (int i = 0; i < Size; i++)
            {
                AlivePieces.Add(new Pawn(ChessBoard[i, 6], PlayerColor.White, this));
            }

        }

        internal void Start()
        {
            UpdateAll(MoveFlags.UpdateAttacked | MoveFlags.UpdateMoves);
            IsPaused = false;
        }

        public void Update(Move move, MoveFlags moveFlags)
        {
            if (moveFlags.HasFlag(MoveFlags.None)) return;

            Cell from = GetCell(move.FromX, move.FromY);
            Cell to = GetCell(move.ToX, move.ToY);
            Piece[] piecesToUpdate1 = new Piece[from.AttackersList.Count];
            Piece[] piecesToUpdate2 = new Piece[to.AttackersList.Count];
            from.AttackersList.CopyTo(piecesToUpdate1, 0);
            to.AttackersList.CopyTo(piecesToUpdate2, 0);
            Piece piece = to.Piece;

            piece.AttackedCells.ForEach(c => c.AttackersList.Remove(piece));
            piece.AttackedCells.Clear();

            if (moveFlags.HasFlag(MoveFlags.UpdateAttacked))
            {
                foreach (Piece itemPiece in piecesToUpdate1)
                    itemPiece.SetAttackedCells();
                foreach (Piece itemPiece in piecesToUpdate2)
                    itemPiece.SetAttackedCells();
                piece.SetAttackedCells();
            }
            if (moveFlags.HasFlag(MoveFlags.UpdateMoves))
            {
                foreach (Piece itemPiece in piecesToUpdate1)
                    itemPiece.SetAllowedMoves();
                foreach (Piece itemPiece in piecesToUpdate2)
                    itemPiece.SetAllowedMoves();
                piece.SetAllowedMoves();
            }
        }
        public void UpdateAll(MoveFlags moveFlags)
        {
            if (moveFlags.HasFlag(MoveFlags.UpdateAttacked))
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
                    p.SetAttackedCells();
                    //p.AttackedCells.ForEach(x => x.AttackersList.Add(p));
                }
            }
            if (moveFlags.HasFlag(MoveFlags.UpdateMoves))
            {
                foreach (var p in AlivePieces)
                {
                    p.SetAllowedMoves();
                }
            }
        }



        public void DestroyPiece(Piece piece)
        {
            if (AlivePieces.Contains(piece))
            {
                piece.AttackedCells.ForEach(c => c.AttackersList.Remove(piece));
                AlivePieces.Remove(piece);
                piece.IsInGame = false;
                piece.CurrentCell = null;
            }
        }
        public void RestorePiece(Piece destroyedPiece, Cell cell)
        {
            destroyedPiece.IsInGame = true;
            cell.Piece = destroyedPiece;
            destroyedPiece.CurrentCell = cell;
            AlivePieces.Add(destroyedPiece);

            //destroyedPiece.SetAllowedMoves();
            //destroyedPiece.SetAttackedCells();
        }

        /// <summary>
        /// Moves the piece from its current cell to the nextCell, updates lists of attacked cells and allowed moves (accordingly to specified moveFlags)
        /// </summary>
        /// <param name="move"></param>
        /// <param name="moveFlags"></param>
        /// <returns>Return the piece that was destroyed after move (can be null if no piece destroyed)</returns>
        public Piece MovePiece(Move move, MoveFlags moveFlags)
        {
            Cell nextCell = GetCell(move.ToX, move.ToY);
            Cell prevCell = GetCell(move.FromX, move.FromY);
            Piece piece = prevCell.Piece;

            Piece destroyedPiece = nextCell.Piece;
            if (destroyedPiece != null)
                DestroyPiece(destroyedPiece);
            piece.MoveTo(nextCell);

            //Update(move,moveFlags);
            UpdateAll(moveFlags);

            return destroyedPiece;
        }

        public void UndoMove(Move move, Piece destroyedPiece, MoveFlags moveFlags)
        {
            Cell prevCell = GetCell(move.FromX, move.FromY);
            Cell nextCell = GetCell(move.ToX, move.ToY);
            Piece piece = nextCell.Piece;
            nextCell.Piece = null;
            //Ставим фигуру на предудущую клетку
            piece.CurrentCell = prevCell;
            prevCell.Piece = piece;
            piece.MovesCounter--;
            if (destroyedPiece != null)
            {
                RestorePiece(destroyedPiece, nextCell);
            }
            Move reverseMove = new Move(move.ToX, move.ToY, move.FromX, move.FromY);
            //Update(reverseMove, moveFlags);
            UpdateAll(moveFlags);
        }

        public List<Piece> GetAlivePieces(PlayerColor playerColor)
        {
            return AlivePieces
                .Where(p => p.PlayerColor == playerColor)
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
        public int GetAllowedMovesNumber(PlayerColor playerColor)
        {
            return GetAlivePieces(playerColor).Sum(p => p.AllowedCells.Count);
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
