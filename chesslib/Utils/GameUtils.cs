using chesslib.Command;
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
        private IOriginator<MakeMoveCommand> _originator;

        public GameMemento Memento
        {
            get
            {
                return _memento;
            }

            set
            {
                _memento = value;
            }
        }

        public GameUtils(IOriginator<MakeMoveCommand> originator)
        {
            Memento = new GameMemento();
            _originator = originator;
        }

        public void SaveState()
        {
            Memento.MementoStack.Push(_originator.GetMemento());
        }

        public void LoadPreviousState()
        {
            _originator.SetMemento(Memento.GetPreviousState());
        }
    }
}
