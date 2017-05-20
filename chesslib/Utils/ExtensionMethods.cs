using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace chesslib.Utils
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T input)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, input);
                ms.Seek(0, SeekOrigin.Begin);

                return (T) formatter.Deserialize(ms);
            }
        }

        public static List<Piece> GetAttackedPieces(this Piece inputPiece)
        {
            var list = new List<Piece>();
            inputPiece
                .AttackedCells
                .ForEach(c =>
                {
                    if (c.IsTaken
                    && c.Piece.PlayerColor != inputPiece.PlayerColor)
                        list.Add(c.Piece);
                });
            return list;
        }

        public static double GetPieceValue(this Piece inputPiece)
        {
            switch (inputPiece.PieceType)
            {
                case PieceType.Pawn:
                    return 2;
                case PieceType.Rook:
                    return 6;
                case PieceType.Knight:
                    return 5;
                case PieceType.Bishop:
                    return 5;
                case PieceType.Queen:
                    return 10;
                case PieceType.King:
                    return 8;
                default:
                    throw new Exception("no type");
            }
        }
    }
}
