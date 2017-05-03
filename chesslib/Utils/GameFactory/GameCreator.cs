using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace chesslib.Utils
{
    public class GameCreator
    {
        public virtual Game StartNew(GameCreationParams param)
        {
            if (param.LoadFromFile)
                return new LoadedGameCreator().StartNew(param);
            return new FreshGameCreator().StartNew(param);
        }
    }

    public class FreshGameCreator : GameCreator
    {
        public override Game StartNew(GameCreationParams param)
        {
            Game g = new Game();
            g.AddPlayer(param.Player1);
            g.AddPlayer(param.Player2);
            g.CurrentPlayer = g.Players.First(p => p.PlayerColor == PlayerColor.White);
            return g;
        }
    }

    public class LoadedGameCreator : GameCreator
    {
        public override Game StartNew(GameCreationParams param)
        {
            Game g = new FreshGameCreator().StartNew(param);
            g.LoadFromFile(param.Path);
            return g;
        }
    }
}
