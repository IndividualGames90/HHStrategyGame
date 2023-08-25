namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Basic Generic Grid.
    /// </summary>
    public class Grid<T>
    {
        public int Width => width;
        private int width;

        public int Height => height;
        private int height;

        private T[,] gridArray;


        public Grid(int width, int height)
        {
            this.width = width;
            this.height = height;

            gridArray = new T[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gridArray[x, y] = (T)System.Activator.CreateInstance(typeof(T), new object[] { x, y });
                }
            }
        }


        public T GetGridObject(int a_x, int a_y)
        {
            return gridArray[a_x, a_y];
        }
    }
}
