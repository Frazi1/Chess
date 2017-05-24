using System;
using System.Collections;
using System.Collections.Generic;

namespace chesslib.Field.Smart
{
    [Serializable]
    public class SmartBoard : IBoard<SmartCell>
    {
        public IEnumerator<SmartCell> GetEnumerator()
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

        public SmartCell[,] Board { get; set; }

        public byte Size { get; }

        public SmartCell this[int index0, int index1]
        {
            get { return Board[index0, index1]; }
            private set { Board[index0, index1] = value; }
        }

        public void Initialize()
        {
            for (byte i = 0; i < Size; i++)
            {
                for (byte j = 0; j < Size; j++)
                {
                    this[i,j] = new SmartCell(i,j);
                }
            }
        }

        public void SetUpPieces()
        {
            throw new System.NotImplementedException();
        }

        public List<SmartCell> GetAlivePiecesList()
        {
            throw new System.NotImplementedException();
        }

        public SmartCell GetCell(int index0, int index1)
        {
            throw new System.NotImplementedException();
        }

        public IBoard<SmartCell> GetCopy()
        {
            throw new System.NotImplementedException();
        }
    }
}