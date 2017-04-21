namespace chesslib.Events
{
    public class GameStateChangedEventArgs
    {
        public Game Game { get; set; }

        public GameStateChangedEventArgs(Game game)
        {
            Game = game;
        }
    }
}