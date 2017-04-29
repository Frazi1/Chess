namespace chesslib.Command
{
    public interface ICommand
    {
        bool CanExecute(object parameter);
        void Execute(object parameter);
    }
}
