using chesslib.Command;
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
        public Stack<Memento<MakeMoveCommand>> MementoStack { get; set; }

        public GameMemento()
        {
            MementoStack = new Stack<Memento<MakeMoveCommand>>();
        }

        public Memento<MakeMoveCommand> GetPreviousState()
        {
            return MementoStack.Pop();
        }
    }
}