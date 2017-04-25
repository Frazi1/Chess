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
        private int _posY;
        private Piece _piece;

        public int PosX
        {
            get { return _posX; }
            set { _posX = value; }
        }
        public int PosY
        {
            get { return _posY; }
            set { _posY = value; }
        }
        public Piece Piece
        {
            get { return _piece; }
            set { _piece = value; }
        }
        public bool IsTaken {get{return _piece != null;}}
        public bool IsAttacked { get { return AttackersList.Count > 0; } }
        public List<Piece> AttackersList { get; set; }

        public Cell(int x, int y)
        {
            PosX = x;
            PosY = y;
            AttackersList = new List<Piece>();
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", PosX, PosY);
        }
    }
}
