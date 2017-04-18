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
            if (_gameViewModel.ActivePlayerViewModel != null)
            {
                if (_gameViewModel.SelectedPiece == null)
                {
                    _gameViewModel.SelectedPiece = (e.OriginalSource as Image)?.DataContext as ChessPieceViewModel;
                }
                else
                {
                    int x = (int)e.GetPosition(this.ChessBoard).X;
                    int y = (int)e.GetPosition(this.ChessBoard).Y;
                    try
                    {
                        _gameViewModel.NextCell = _gameViewModel
                            .Game
                            .Board
                            .ChessBoard[x, y];
                        _gameViewModel
                            .ActivePlayerViewModel.PushStrategy();

                        //_gameViewModel
                        //    .Game
                        //    .CurrentPlayer
                        //    .Strategy = new RealPlayerStrategy(
                        //        _gameViewModel.SelectedPiece.Piece,
                        //        _gameViewModel.Game.Board.ChessBoard[x, y]);
                        //_gameViewModel
                        //    .Game
                        //    .CurrentPlayer
                        //    .MakeMove();
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() => _gameViewModel.Game.Start());
            t.IsBackground = true;
            t.Start();
        }
    }
}

