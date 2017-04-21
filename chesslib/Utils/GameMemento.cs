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
        public List<Memento<MakeMoveCommand>> MementoList { get; set; }

        public GameMemento()
        {
            MementoList = new List<Memento<MakeMoveCommand>>();
        }

        public Memento<MakeMoveCommand> GetPreviousState()
        {
            var prevState = MementoList.Last();
            MementoList.RemoveAt(MementoList.Count-1);
            return prevState;
        }
    }
}