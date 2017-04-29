using chesslib.Command;

namespace chesslib.Utils
{
    public class GameUtils
    {
        private Game game;

        public GameUtils(Game game)
        {
            this.game = game;
        }

        public void SaveState(MakeMoveCommand moveCommand)
        {
            game.MoveCommands.Add(moveCommand);
        }

        public void LoadPreviousState()
        {
            game.CurrentPlayer.CancelTurn();
            game.IsPaused = true;

            int last = game.MoveCommands.Count - 1;
            game.MoveCommands[last].Undo(game);
            game.MoveCommands.RemoveAt(last);

            game.RaiseGameStateChange();
        }
    }
}
