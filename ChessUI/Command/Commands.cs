﻿using chesslib;
using ChessUI.ViewModel;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;

namespace ChessUI.Command
{
    public class Commands
    {
        public Commands(GameViewModel gameViewModel)
        {
            SaveGameCommand = new RelayCommand<Game>(game =>
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Файлы игры (*.chess)|*.chess|Все файлы (*.*)|*.*";
                if ((bool) sfd.ShowDialog())
                {
                    string path = sfd.FileName;
                    game.GameUtils.SaveToFile(path);
                }
            });

            LoadGameCommand = new RelayCommand(() =>
            {

            });
        }

        public RelayCommand<Game> SaveGameCommand { get; private set; }
        public RelayCommand LoadGameCommand { get; private set; }
    }
}
