using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace chesslib.Field.Bit
{
    [Serializable]
    public class EnumBoard : IBoard<EnumPiece>
    {
        public byte Size { get; }
        public EnumPiece[,] Board { get; set; }

        public EnumPiece this[int index0, int index1]
        {
            get { return Board[index0, index1]; }
            private set { Board[index0, index1] = value; }
        }

        public EnumBoard(byte size)
        {
            Size = size;
            Board = new EnumPiece[Size, Size];
        }

        public void Initialize()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    this[i, j] = EnumPiece.Empty;
                }
            }
        }

        public void SetUpPieces()
        {
            throw new System.NotImplementedException();
        }

        public EnumPiece GetCell(int index0, int index1)
        {
            return this[index0, index1];
        }

        public IBoard<EnumPiece> GetCopy()
        {
            EnumBoard copyBoard = new EnumBoard(Size);
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    copyBoard[i, j] = this[i, j];
            return copyBoard;
        }

        public List<EnumPiece> GetAlivePiecesList()
        {
            return this.Where(enumCell => enumCell != EnumPiece.Empty).ToList();
        }

        public IEnumerator<EnumPiece> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    yield return this[i, j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}