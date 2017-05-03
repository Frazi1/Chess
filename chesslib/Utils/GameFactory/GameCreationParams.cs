using chesslib.Player;

namespace chesslib.Utils
{
    public class GameCreationParams
    {
        public IPlayer Player1 { get; set; }
        public IPlayer Player2 { get; set; }
        public bool LoadFromFile { get; set; }
        public string Path { get; set; }

        public GameCreationParams()
        {
            LoadFromFile = false;
        }
    }
}