using chesslib.Field;
using chesslib.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Utils
{
    public class GameUtils
    {
        private GameMemento _memento;
        private IOriginator<Board> _originator;
        public GameUtils(IOriginator<Board> originator)
        {
            _memento = new GameMemento();
            _originator = originator;
        }

        public void SaveState()
        {

            _memento.MementoStack.Push(_originator.GetMemento());
        }

        public void LoadPreviousState()
        {
            _originator.SetMemento(_memento.GetPreviousState());
        }
    }
}
