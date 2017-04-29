using chesslib.Utils;

namespace chesslib.Events
{
    public class GameStateChangedEventArgs
    {
        public Game Game { get; set; }
        public bool IsCheck { get { return BoardUtils.IsCheck(Game.Board, Game.CurrentPlayer.PlayerType); } }
        public bool IsCheckMate { get { return Game.IsGameFinished; } }

        public GameStateChangedEventArgs(Game game)
        {
            Game = game;
        }
    }
}