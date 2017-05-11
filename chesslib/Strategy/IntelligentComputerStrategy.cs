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

        public Tuple<Cell, Cell> PrepareMove(IPlayer player, Board board)
        {
            var move = GetBestMove(board, player.PlayerColor);
            return new Tuple<Cell, Cell>(move.Item1.CurrentCell, move.Item2);
        }

        private double EstimateMove(Board currentBoard, Tuple<Piece, Cell> move, int steps)
        {
            double estimation = 0;
            Piece piece = move.Item1;
            Cell cell = move.Item2;

            int allowedMovesCount = piece
                .AllowedCells
                .Count;

            //if (currentBoard == null)
            //{
            //    Board board = piece.Board.DeepCopy();
            //    piece = board.ChessBoard[piece.PosX, piece.PosY].Piece;
            //    cell = board.ChessBoard[cell.PosX, cell.PosY];
            //}

            if (cell.IsTaken && cell.Piece.PlayerType!= piece.PlayerType)
            {
                estimation += cell.Piece.GetPieceValue();
            }

            //Piece pieceCopy;
            //BoardUtils.VirtualMove(move, true, true, out pieceCopy);
            //if (allowedMovesCount < pieceCopy.AllowedCells.Count)
            //    estimation += 2 * pieceCopy.AllowedCells.Count / (double)allowedMovesCount;

            return estimation;
        }

        private Tuple<Piece, Cell> GetBestMove(Board currentBoard, PlayerColor playerColor)
        {
            var alivePieces = currentBoard.GetAlivePieces(playerColor);
            var estimatedMoves = new List<Tuple<Tuple<Piece, Cell>, double>>();
            foreach (var p in alivePieces)
            {
                foreach (var c in p.AllowedCells)
                {
                    var move = new Tuple<Piece, Cell>(p, c);
                    var estimatedMove = new Tuple<Tuple<Piece, Cell>, double>
                        (move, EstimateMove(currentBoard, move, 1));
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
