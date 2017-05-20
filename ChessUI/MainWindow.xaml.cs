using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using chesslib.Field;
using ChessUI.ViewModel;
using Microsoft.Practices.ServiceLocation;

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
            _gameViewModel.ViewWindow = this;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int x = (int)e.GetPosition(ChessBoard).X;
            int y = (int)e.GetPosition(ChessBoard).Y;
            string text = "";

            if (e.LeftButton == e.ButtonState)
                if (_gameViewModel.ActivePlayerViewModel != null)
                {
                    if (_gameViewModel.SelectedPiece == null)
                    {
                        var img = e.OriginalSource as Image;
                        if (img != null)
                            _gameViewModel.SelectedPiece = img.DataContext as ChessPieceViewModel;
                    }
                    else
                    {
                        //int x = (int) e.GetPosition(this.ChessBoard).X;
                        //int y = (int) e.GetPosition(this.ChessBoard).Y;
                        try
                        {
                            _gameViewModel.NextCell = _gameViewModel
                                .Game
                                .GetCell(x, y);
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


            if (e.RightButton == MouseButtonState.Pressed)
            {
                //int x = (int) e.GetPosition(this.ChessBoard).X;
                //int y = (int) e.GetPosition(this.ChessBoard).Y;
                //string text = "";
                var piece = _gameViewModel.Game.GetPiece(x, y);
                if (piece != null)
                {
                    piece.AllowedCells.ForEach(a =>
                    {
                        text += a.ToString();
                        text += Environment.NewLine;
                    });
                }
                MessageBox.Show(text);
            }
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                Cell cell = _gameViewModel.Game.GetCell(x, y);
                if (cell != null)
                {
                    cell.AttackersList.ForEach(a =>
                    {
                        text += a.ToString();
                        text += Environment.NewLine;
                    });
                }
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

        //private void ListView_Loaded(object sender, RoutedEventArgs e)
        //{
        //    ThreadPool.QueueUserWorkItem((x) =>
        //    {
        //        while (true)
        //        {
        //            UpdateMovesHistory();
        //            Thread.Sleep(500);
        //        }
        //    });
        //}

        public DispatcherOperation UpdateMovesHistory()
        {
            return Dispatcher.BeginInvoke((Action)(() =>
            {
                if (_gameViewModel.Game != null)
                {
                    _gameViewModel.MoveCommands.Clear();
                    var list = _gameViewModel.Game.MoveCommands;
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        _gameViewModel.MoveCommands.Add(list[i]);
                    }
                }
            }));
        }

        private void MenuItem_NewGame_Click(object sender, RoutedEventArgs e)
        {
            Window newGame = new NewGame();
            newGame.ShowDialog();
        }
    }
}

