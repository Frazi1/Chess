using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Memento
{
    public class Memento<T>
    {
        private T _data;

        public Memento(T data)
        {
            _data = data;
        }

        public T GetState()
        {
            return _data;
        }
    }
}
