using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Memento
{
   public interface IOriginator<T>
    {
        Memento<T> GetMemento();
        void SetMemento(Memento<T> value);
    }
}
