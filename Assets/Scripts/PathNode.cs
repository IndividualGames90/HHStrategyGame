namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Individual node on path.
    /// </summary>
    public class PathNode
    {
        //private Grid<PathNode> grid;

        public int X => x;
        private int x;
        public int Y => y;
        private int y;

        public int GCost;
        public int HCost;
        public int FCost => HCost + GCost;

        public bool isWalkable;

        public PathNode cameFromNode;


        public PathNode(/*Grid<PathNode> grid,*/ int x, int y)
        {
            //this.grid = grid;
            this.x = x;
            this.y = y;
            isWalkable = true;
        }


        public override string ToString()
        {
            return x + "," + y;
        }
    }
}

