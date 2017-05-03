﻿using chesslib.Command;

namespace chesslib.Utils
{
    public class GameUtils
    {
        private Game _game;

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
            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create))
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf =
                                    new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
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
            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf =
                                    new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                MoveCommands moveCommands = (MoveCommands) bf.Deserialize(fs);
                fs.Close();
                _game.MoveCommands = moveCommands;
            }
        }
    }
}
