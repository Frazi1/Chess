using chesslib.Strategy;
using ChessUI.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ViewModelLocator v = new ViewModelLocator();
        GameViewModel _gameViewModel;
        public MainWindow()
        {
            InitializeComponent();
            _gameViewModel = ServiceLocator.Current.GetInstance<GameViewModel>();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == e.ButtonState)
                if (_gameViewModel.ActivePlayerViewModel != null)
                {
                    if (_gameViewModel.SelectedPiece == null)
                    {
                        var img = e.OriginalSource as Image;
                        if(img!=null)
                            _gameViewModel.SelectedPiece = img.DataContext as ChessPieceViewModel;
                    }
                    else
                    {
                        int x = (int) e.GetPosition(this.ChessBoard).X;
                        int y = (int) e.GetPosition(this.ChessBoard).Y;
                        try
                        {
                            _gameViewModel.NextCell = _gameViewModel
                                .Game
                                .Board
                                .ChessBoard[x, y];
                            _gameViewModel
                                .ActivePlayerViewModel.PushCommand();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                            _gameViewModel.SelectedPiece = null;
                        }
                    }

                }
            if (e.RightButton == e.ButtonState)
            {
                int x = (int) e.GetPosition(this.ChessBoard).X;
                int y = (int) e.GetPosition(this.ChessBoard).Y;
                string text = "";
                //_gameViewModel.Game.Board.ChessBoard[x, y].AttackersList.ForEach(a => { text += a.ToString(); text += Environment.NewLine; });
                _gameViewModel.Game.Board.ChessBoard[x, y].Piece?.AllowedCells.ForEach(a => { text += a.ToString(); text += Environment.NewLine; });
                MessageBox.Show(text);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _gameViewModel.Game.Start();
        }

        private void button_prev_Click(object sender, RoutedEventArgs e)
        {
            _gameViewModel.Game.GameUtils.LoadPreviousState();
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((x) =>
            {
                while (true)
                {
                    Dispatcher.BeginInvoke((Action)(() =>
                    {
                        _gameViewModel.MoveCommands.Clear();
                        var list = _gameViewModel.Game.MoveCommands;
                        for (int i = list.Count - 1; i >= 0; i--)
                        {
                            _gameViewModel.MoveCommands.Add(list[i]);
                        }
                    }));
                    Thread.Sleep(500);
                }
            });
        }
    }
}

