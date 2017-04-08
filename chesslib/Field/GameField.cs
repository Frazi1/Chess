using chesslib.Figures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chesslib.Field
{
    public class GameField
    {
        private readonly Dictionary<char, Dictionary<int, Cell>> _field;
        private char[] _chars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
        private const int size = 8;

        public Dictionary<char, Dictionary<int, Cell>> Field { get { return _field; } }



        public Cell this[char letter, int index]
        {
            get { return Field[letter][index]; }
        }

        public GameField()
        {
            _field = new Dictionary<char, Dictionary<int, Cell>>();
            SetUpCells();
        }

        private void SetUpCells()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    char c = _chars[i];
                    Field[c][j] = new Cell(c, j);
                }
            }
        }
        private void SetUpFigures()
        {
            int frontRowWhite = 1;
            int backRowWhite = 0;
            int frontRowBlack = 7;
            int backRowBlack = 8;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Color color = Color.Cornsilk;
                    if (j == frontRowWhite || j == backRowWhite)
                        color = Color.White;
                    else if (j == frontRowBlack || j == backRowBlack)
                        color = Color.Black;
                    else continue;

                    char c = _chars[i];

                    if (j == backRowBlack || j == backRowWhite)
                    {
                        if (c == 'A' || c == 'H') this[c, j].Figure = new Rook(this[c, j], color);
                        if (c == 'B' || c == 'G') this[c, j].Figure = new Knight(this[c, j], color);
                        if (c == 'C' || c == 'F') this[c, j].Figure = new Bishop(this[c, j], color);
                        if (c == 'D') this[c, j].Figure = new Queen(this[c, j], color);
                        if (c == 'E') this[c, j].Figure = new King(this[c, j], color);
                    }
                    else if (j == frontRowWhite || j == frontRowBlack)
                    {
                        this[c, j].Figure = new Pawn(this[c, j], color);
                    }
                }
            }



        }
    }
}
