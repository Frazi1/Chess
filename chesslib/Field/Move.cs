namespace chesslib.Field
{
    public class Move
    {
        public int FromX { get; }
        public int FromY { get;}
        public int ToX { get; }
        public int ToY { get; }

        public Move()
        { 
        }

        public Move(int fromX, int fromY, int toX, int toY)
        {
            FromX = fromX;
            FromY = fromY;
            ToX = toX;
            ToY = toY;  
        }

        public override string ToString()
        {
            return string.Format("{0},{1} - {2}, {3}", FromX, FromY, ToX, ToY);
        }
    }
}
