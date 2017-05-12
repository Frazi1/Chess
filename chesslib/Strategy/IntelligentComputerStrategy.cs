using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using chesslib.Field;
using chesslib.Player;
using chesslib.Utils;

namespace chesslib.Strategy
{
    public class IntelligentComputerStrategy : IStrategy
    {
        private List<Piece> alivePieces;
        private int steps;

        public IntelligentComputerStrategy(int steps)
        {
            this.steps = steps;
        }

        public Move PrepareMove(IPlayer player, Board board)
        {
            var move = GetBestMove(board, player.PlayerColor);
            return move;
        }

        private double EstimateMove(Board virtualBoard, Move move, int steps)
        {
            double estimation = 0;
            Piece piece = virtualBoard.GetPiece(move.FromX, move.FromY);
            Cell cell = virtualBoard.GetCell(move.ToX, move.ToY);

            int allowedMovesCount = piece
                .AllowedCells
                .Count;

            if (cell.IsTaken && cell.Piece.PlayerColor != piece.PlayerColor)
            {
                estimation += cell.Piece.GetPieceValue();
            }

            Piece pieceCopy, destroyedPieceCopy;
            MoveFlags moveFlags = MoveFlags.UpdateMoves;
            BoardUtils.VirtualMove(virtualBoard, false, move, moveFlags, out pieceCopy, out destroyedPieceCopy);
            if (allowedMovesCount < pieceCopy.AllowedCells.Count)
                estimation += 1;
            virtualBoard.UndoMove(move, destroyedPieceCopy, moveFlags);

            return estimation;
        }

        private Move GetBestMove(Board currentBoard, PlayerColor playerColor)
        {
            List<Piece> alivePieces = currentBoard.GetAlivePieces(playerColor);
            Board virtualBoard = currentBoard.DeepCopy();
            List<Tuple<Move, double>> estimatedMoves = new List<Tuple<Move, double>>();
            foreach (Piece p in alivePieces)
            {
                foreach (Cell c in p.AllowedCells)
                {
                    //Tuple<Piece, Cell> move = new Tuple<Piece, Cell>(p, c);
                    Move move = new Move(p.PosX, p.PosY, c.PosX, c.PosY);
                    var estimatedMove = new Tuple<Move, double>
                        (move, EstimateMove(virtualBoard, move, 1));
                    estimatedMoves.Add(estimatedMove);
                }
            }
            var orderedEnumerable = estimatedMoves
                .OrderByDescending(x => x.Item2)
                .ToList();
            return orderedEnumerable
                .First()
                .Item1;

        }
    }
}
