namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Basic Generic Grid.
    /// </summary>
    public class Grid<T>
    {
        public int Width => m_width;
        private int m_width;

        public int Height => m_height;
        private int m_height;

        private T[,] m_gridArray;


        public Grid(int a_width, int a_height)
        {
            m_width = a_width;
            m_height = a_height;

            m_gridArray = new T[a_width, a_height];

            for (int x = 0; x < a_width; x++)
            {
                for (int y = 0; y < a_height; y++)
                {
                    m_gridArray[x, y] = (T)System.Activator.CreateInstance(typeof(T), new object[] { x, y });
                }
            }
        }


        public T GetGridObject(int a_x, int a_y)
        {
            return m_gridArray[a_x, a_y];
        }
    }
}
