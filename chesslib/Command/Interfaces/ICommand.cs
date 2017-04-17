namespace chesslib.Command
{
    public interface ICommand
    {
        bool CanExecute { get; }
        void Execute();
    }
}
