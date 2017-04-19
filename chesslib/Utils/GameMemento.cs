using chesslib.Field;
using chesslib.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Utils
{
    public class GameMemento
    {
        public Stack<Memento<Board>> MementoStack { get; set; }

        public GameMemento()
        {
            MementoStack = new Stack<Memento<Board>>();
        }

        public Memento<Board> GetPreviousState()
        {
            return MementoStack.Pop();
        }
    }
}