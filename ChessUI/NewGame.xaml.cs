using chesslib;
using chesslib.Player;
using chesslib.Strategy;
using chesslib.Utils;
using ChessUI.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChessUI
{
    /// <summary>
    /// Логика взаимодействия для NewGame.xaml
    /// </summary>
    public partial class NewGame : Window
    {
        public string Path { get; set; }
        public NewGame()
        {
            Path = string.Empty;
            InitializeComponent();
        }

        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файлы игры (*.chess)|*.chess|Все файлы (*.*)|*.*";
            if ((bool) ofd.ShowDialog())
            {
                Path = ofd.FileName;
            }
        }

        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            List<IPlayer> players = new List<IPlayer>();
            GameCreationParams param = new GameCreationParams();
            //MessageBox.Show(ComboBox_Player1.SelectedValue.ToString());
            switch (ComboBox_Player1.SelectedValue)
            {
                case PlayerType.Computer:
                    players.Add(new ComputerPlayer(PlayerColor.White, new DefaultComputerStrategy()));
                    break;
                case PlayerType.Human:
                    players.Add(new RealPlayer(PlayerColor.White));
                    break;
            }
            switch (ComboBox_Player2.SelectedValue)
            {
                case PlayerType.Computer:
                    players.Add(new ComputerPlayer(PlayerColor.Black, new DefaultComputerStrategy()));
                    break;
                case PlayerType.Human:
                    players.Add(new RealPlayer(PlayerColor.Black));
                    break;
            }
            param.Player1 = players[0];
            param.Player2 = players[1];
            if (Path != string.Empty)
            {
                param.LoadFromFile = true;
                param.Path = Path;
            }
            Game g = new GameCreator().StartNew(param);
            GameViewModel gvm = ServiceLocator.Current.GetInstance<GameViewModel>();
            gvm.Initialize(g);
            DialogResult = true;
        }
    }
}
