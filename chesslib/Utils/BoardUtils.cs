﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Utils
{
    public static class BoardUtils
    {
        public static bool IsValidCell(Cell[,] chessBoard, int x, int y)
        {
            return x >= 0 &&
                            x < chessBoard.GetLength(0) &&
                            y >= 0 &&
                            y < chessBoard.GetLength(0);
        }
    }
}