using chesslib.Command;

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
    }
}
