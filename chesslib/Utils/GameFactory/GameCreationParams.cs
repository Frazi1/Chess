namespace chesslib.Utils
{
    public class GameCreationParams
    {
        public chesslib.Player.IPlayer Player1 { get; set; }
        public chesslib.Player.IPlayer Player2 { get; set; }
        public bool LoadFromFile { get; set; }
        public string Path { get; set; }

        public GameCreationParams()
        {
            LoadFromFile = false;
        }
    }
}