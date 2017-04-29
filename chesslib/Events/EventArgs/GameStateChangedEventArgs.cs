using chesslib.Utils;

namespace chesslib.Events
{
    public class GameStateChangedEventArgs
    {
        public Game Game { get; set; }
        public bool IsCheck { get { return BoardUtils.IsCheck(Game.Board, Game.CurrentPlayer.PlayerType); } }

        public GameStateChangedEventArgs(Game game)
        {
            Game = game;
        }
    }
}