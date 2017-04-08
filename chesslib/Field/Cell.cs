using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib
{
    public class Cell
    {
        public char Letter { get; set; }
        public int Number { get; set; }

        private Piece _figure;

        public Piece Figure
        {
            get { return _figure; }
            set { _figure = value; }
        }

        public bool IsTaken => Figure != null;

        public Cell(char letter, int number)
        {
            Letter = letter;
            Number = number;
        }
    }
}
