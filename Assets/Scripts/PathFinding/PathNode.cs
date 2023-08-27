namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Individual node on path.
    /// </summary>
    public class PathNode
    {
        //private Grid<PathNode> grid;

        public int X => m_x;
        private int m_x;
        public int Y => m_y;
        private int m_y;

        public int GCost;
        public int HCost;
        public int FCost => HCost + GCost;

        public bool Walkable;

        public PathNode CameFromNode;


        public PathNode(int x, int y)
        {
            this.m_x = x;
            this.m_y = y;
            Walkable = true;
        }
    }
}

