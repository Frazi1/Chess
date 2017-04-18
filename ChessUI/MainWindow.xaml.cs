using chesslib.Strategy;
using ChessUI.ViewModel;
using System;
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
        ViewModelLocator v = new ViewModelLocator();
        GameViewModel _gameViewModel;
        public MainWindow()
        {
            InitializeComponent();
            _gameViewModel = v.Main;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
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
                    _gameViewModel
                        .Game
                        .CurrentPlayer
                        .Strategy = new RealPlayerStrategy(
                            _gameViewModel.SelectedPiece.Piece,
                            _gameViewModel.Game.Board.ChessBoard[x, y]);
                    _gameViewModel
                        .Game
                        .CurrentPlayer
                        .MakeMove();
                    //_gameViewModel
                    //    .Game
                    //    .CurrentPlayer
                    //    .PrepareMove(_gameViewModel.SelectedPiece.Piece,_gameViewModel.Game.Board.ChessBoard[x, y]);
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
}

