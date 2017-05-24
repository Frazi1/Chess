using System;
using System.Collections.Generic;
using System.Linq;
using chesslib.Field.Smart.Pieces;

namespace chesslib.Field.Smart
{
    [Serializable]
    public class SmartCell
    {
        public byte PosX { get; }
        public byte PosY { get; }
        public Piece Piece { get; set; }
        public bool IsTaken
        {
            get { return Piece != null; }
        }
        public List<Piece> AttackersList { get; set; }

        public SmartCell(byte posX, byte posY)
        {
            PosX = posX;
            PosY = posY;
            AttackersList = new List<Piece>();
        }

        public List<Piece> GetAttackersList(PlayerColor forPlayerColor)
        {
            return AttackersList.Where(p => p.PlayerColor != forPlayerColor).ToList();
        }
        public bool IsAttacked(PlayerColor forPlayerColor)
        {
            return GetAttackersList(forPlayerColor).Count > 0;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", PosX, PosY);
        }
    }
}