using System.Collections.Generic;

namespace chesslib.Field
{
    public interface IBoard<T> : IEnumerable<T>
    {
        T[,] Board { get; set; }
        byte Size { get; }

        T this[int index0, int index1] { get; }

        void Initialize();
        void SetUpPieces();

        List<T> GetAlivePiecesList();

        T GetCell(int index0, int index1);
        IBoard<T> GetCopy();

    }
}