using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
