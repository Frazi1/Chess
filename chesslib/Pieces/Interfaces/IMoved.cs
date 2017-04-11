using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chesslib.Figures.Interfaces
{
    interface IMoved
    {
        bool HasAlreadyMoved { get; set; }
    }
}
