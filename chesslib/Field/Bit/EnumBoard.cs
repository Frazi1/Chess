using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace chesslib.Field.BitBoard
{
    [Serializable]
    public class EnumBoard : IBoard<EnumCell>
    {
        public byte Size { get; }
        public EnumCell[,] Board { get; set; }

        public EnumCell this[int index0, int index1]
        {
            get { return Board[index0, index1]; }
            private set { Board[index0, index1] = value; }
        }

        public EnumBoard(byte size)
        {
            Size = size;
            Board = new EnumCell[Size, Size];
        }

        public void Initialize()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    this[i, j] = EnumCell.Empty;
                }
            }
        }

        public void SetUpPieces()
        {
            throw new System.NotImplementedException();
        }

        public EnumCell GetCell(int index0, int index1)
        {
            return this[index0, index1];
        }

        public IBoard<EnumCell> GetCopy()
        {
            EnumBoard copyBoard = new EnumBoard(Size);
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    copyBoard[i, j] = this[i, j];
            return copyBoard;
        }

        public List<EnumCell> GetAlivePiecesList()
        {
            return this.Where(enumCell => enumCell != EnumCell.Empty).ToList();
        }

        public IEnumerator<EnumCell> GetEnumerator()
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