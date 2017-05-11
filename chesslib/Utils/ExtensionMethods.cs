using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace chesslib.Utils
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T input)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, input);
                ms.Seek(0, System.IO.SeekOrigin.Begin);

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
                    && c.Piece.PlayerType != inputPiece.PlayerType)
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
                    throw new System.Exception("no type");
            }
        }
    }
}
