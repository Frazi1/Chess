using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib
{
    [Serializable]
    public class Cell
    {
        private int _posX;

        public int PosX
        {
            get { return _posX; }
            set { _posX = value; }
        }
        private int _posY;

        public int PosY
        {
            get { return _posY; }
            set { _posY = value; }
        }

        private Piece _piece;

        public Piece Piece
        {
            get { return _piece; }
            set { _piece = value; }
        }

        public bool IsTaken => Piece != null;

        public Cell(int x, int y)
        {
            PosX = x;
            PosY = y;
        }
        public override string ToString()
        {
            return $"{PosX}, {PosY}";
        }
    }
}
