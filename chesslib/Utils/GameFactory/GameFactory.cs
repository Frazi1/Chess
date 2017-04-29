using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace chesslib.Utils
{
    public class GameFactory
    {
        FreshGame FreshGame { get; set; }
        LoadedGame LoadedGame { get; set; }
    }

    public class LoadedGame
    {
        public static Game New(GameCreationParams param)
        {
            Game g = null;

            using (FileStream fs = new FileStream(param.Path, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                g = (Game) bf.Deserialize(fs);
            }
            g.Players.Clear();
            g.AddPlayer(param.Player1);
            g.AddPlayer(param.Player2);
            return g;
        }
    }

    public class FreshGame : Game
    {
        public static Game New(GameCreationParams param)
        {
            Game g = new Game();
            g.AddPlayer(param.Player1);
            g.AddPlayer(param.Player2);

            return g;
        }

    }
}
