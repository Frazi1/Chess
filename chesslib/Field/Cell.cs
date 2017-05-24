using System;
using System.Collections.Generic;
using System.Linq;

namespace chesslib.Field
{
    [Serializable]
    public class Cell
    {
        private Piece _piece;

        public int PosX
        {
            get;
        }
        public int PosY
        {
            get;
        }
        public Piece Piece
        {
            get { return _piece; }
            set { _piece = value; }
        }
        public bool IsTaken { get { return _piece != null; } }
        public List<Piece> AttackersList { get; }

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

        public bool IsAttacked(PlayerColor playerType)
        {
            return AttackersList
                    .Count(a => a.PlayerColor != playerType)
                    > 0;
        }
    }
}
