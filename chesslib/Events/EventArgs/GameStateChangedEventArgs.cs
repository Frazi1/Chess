using chesslib.Utils;

namespace chesslib.Events
{
    public class GameStateChangedEventArgs
    {
        public Game Game { get; set; }
        //public Piece IsCheck { get { return BoardUtils.IsCheck(Game.Board, Game.Board.CurrentPlayer.PlayerType); } }

        public GameStateChangedEventArgs(Game game)
        {
            Game = game;
        }
    }
}