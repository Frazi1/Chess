﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using chesslib.Player;
using chesslib.Field;

namespace chesslib.Figures
{
    public class Bishop : Piece
    {
        public Bishop(Cell currentCell, PlayerType playerType) : base(currentCell, playerType)
        {
        }

        public override List<Cell> GetAllowedMoves()
        {
            List<Cell> allowedMoves = new List<Cell>();
            int x = CurrentCell.PosX;
            int y = CurrentCell.PosY;
            Cell[,] chessBoard = Board.Instance.ChessBoard;

            int size = Board.Instance.ChessBoard.GetLength(0);

            bool _continue;
            //Вправо вверх
            for (int i = x + 1, j = y - 1; i < size && j >= 0; i++, j--)
            {
                _continue = TryCell(allowedMoves, chessBoard, i, j);
                if (!_continue)
                    break;
            }
            //Влево вверх
            for (int i = x - 1, j = y - 1; i >= 0 && j > 0; i--, j--)
            {
                _continue = TryCell(allowedMoves, chessBoard, i, j);
                if (!_continue)
                    break;
            }

            //Вправо вниз
            for (int i = x + 1, j = y + 1; i < size && j < size; i++, j++)
            {
                _continue = TryCell(allowedMoves, chessBoard, i, j);
                if (!_continue)
                    break;
            }

            //Влево вниз
            for (int i = x - 1, j = y + 1; i >= 0 && j < size; i--, j++)
            {
                _continue = TryCell(allowedMoves, chessBoard, i, j);
                if (!_continue)
                    break;
            }


            return allowedMoves;
        }

        public override bool MoveTo(Cell cell, IPlayer player)
        {
            return base.MoveTo(cell, player);
        }
    }
}
