using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using chesslib.Command;

namespace chesslib.Utils
{
    public class GameUtils
    {
        private readonly Game _game;

        public GameUtils(Game game)
        {
            _game = game;
        }

        public void SaveState(MakeMoveCommand command)
        {
            _game.MoveCommands.Add(command);
        }

        public void LoadPreviousState()
        {
            _game.CurrentPlayer.CancelTurn();
            _game.IsPaused = true;
            int last = _game.MoveCommands.Count - 1;
            _game.MoveCommands[last].Undo(_game);
            _game.MoveCommands.RemoveAt(last);
            _game.RaiseGameStateChange();
        }

        public void SaveToFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, _game.MoveCommands);
                fs.Close();
            }
        }
        public void LoadFromFile(string path)
        {
            while (_game.MoveCommands.Count > 0)
            {
                LoadPreviousState();
            }
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                List<MakeMoveCommand> moveCommands = (List<MakeMoveCommand>)bf.Deserialize(fs);
                fs.Close();
                _game.MoveCommands = moveCommands;
            }
        }
    }
}
