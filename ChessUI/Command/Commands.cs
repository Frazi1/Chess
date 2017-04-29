using chesslib.Utils;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ChessUI.Command
{
    public class Commands
    {
        public Commands()
        {
            LoadGameCommand = new RelayCommand<GameCreationParams>((param) => 
            {
            });
        }

        public RelayCommand<GameCreationParams> LoadGameCommand { get; private set; }
    }
}
