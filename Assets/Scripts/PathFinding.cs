using System.Collections.Generic;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Pritimitive Astar Pathfinding.
    /// </summary>
    public class PathFinding
    {
        private Grid<PathNode> m_grid;
        private List<PathNode> openList;
        private List<PathNode> closedList;

        private const int c_diagonalMoveCost = 14;
        private const int c_straightMovecost = 10;


        public PathFinding(int a_width, int a_height)
        {
            m_grid = new(a_width, a_height);
        }

        public void AssignIsWalkeables(List<GameObject> m_navNodes)
        {
            foreach (GameObject navnode in m_navNodes)
            {
                var element = navnode.GetComponent<NavGridElement>();
                var pathNode = m_grid.GetGridObject(element.X, element.Y);
                pathNode.isWalkable = !element.IsObstacle();
            }
        }



        public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {
            PathNode startNode = m_grid.GetGridObject(startX, startY);
            PathNode endNode = m_grid.GetGridObject(endX, endY);

            openList = new() { startNode };
            closedList = new();

            for (int x = 0; x < m_grid.Width; x++)
            {
                for (int y = 0; y < m_grid.Height; y++)
                {
                    PathNode pathNode = m_grid.GetGridObject(x, y);
                    pathNode.GCost = int.MaxValue;
                    pathNode.cameFromNode = null;
                }
            }

            startNode.GCost = 0;
            startNode.HCost = CalculateDistanceCost(startNode, endNode);

            while (openList.Count > 0)
            {
                PathNode currentNode = NodeWithLowestFCost(openList);
                if (currentNode == endNode)
                {
                    //Reached final node
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
                {
                    if (closedList.Contains(neighbourNode))
                    {
                        continue;
                    }
                    if (!neighbourNode.isWalkable)
                    {
                        closedList.Add(neighbourNode);
                        continue;
                    }

                    int tentativeGCost = currentNode.GCost +
                                         CalculateDistanceCost(currentNode, neighbourNode);

                    if (tentativeGCost < neighbourNode.GCost)
                    {
                        neighbourNode.cameFromNode = currentNode;
                        neighbourNode.GCost = tentativeGCost;
                        neighbourNode.HCost = CalculateDistanceCost(neighbourNode, endNode);

                        if (!openList.Contains(neighbourNode))
                        {
                            openList.Add(neighbourNode);
                        }
                    }
                }
            }

            return null;///No nodes remain in openList
        }


        /// <summary> Detect eighthold neighbours of the given PathNode. </summary>
        private List<PathNode> GetNeighbourList(PathNode currentNode)
        {
            List<PathNode> neighbourList = new();

            if (currentNode.X - 1 >= 0)///Left
            {
                neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y));
                if (currentNode.Y - 1 >= 0)///Left Down
                {
                    neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y - 1));
                }
                if (currentNode.Y + 1 < m_grid.Height)///Left Up
                {
                    neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y + 1));
                }
            }
            if (currentNode.X + 1 < m_grid.Width)///Right
            {
                neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y));
                if (currentNode.Y - 1 >= 0)///Right Down
                {
                    neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y - 1));
                }
                if (currentNode.Y + 1 < m_grid.Height)///Right Up
                {
                    neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y + 1));
                }
            }
            if (currentNode.Y - 1 >= 0)///Down
            {
                neighbourList.Add(GetNode(currentNode.X, currentNode.Y - 1));
            }
            if (currentNode.Y + 1 < m_grid.Height)///Up
            {
                neighbourList.Add(GetNode(currentNode.X, currentNode.Y + 1));
            }

            return neighbourList;
        }


        /// <summary> Grid access with coordinates. </summary>
        private PathNode GetNode(int a_x, int a_y)
        {
            return m_grid.GetGridObject(a_x, a_y);
        }


        private List<PathNode> CalculatePath(PathNode endNode)
        {
            List<PathNode> path = new() { endNode };
            PathNode currentNode = endNode;
            while (currentNode.cameFromNode != null)
            {
                path.Add(currentNode.cameFromNode);
                currentNode = currentNode.cameFromNode;
            }
            path.Reverse();
            return path;
        }


        /// <summary> Calculate the direct distance between two nodes for HCost.</summary>
        private int CalculateDistanceCost(PathNode a_first, PathNode a_second)
        {
            int diagonalX = Mathf.Abs(a_first.X - a_second.X);
            int diagonalY = Mathf.Abs(a_first.Y - a_second.Y);
            int straightMove = Mathf.Abs(diagonalX - diagonalY);
            return c_diagonalMoveCost * Mathf.Min(diagonalX, diagonalY) +
                   c_straightMovecost * straightMove;
        }


        private PathNode NodeWithLowestFCost(List<PathNode> pathNodeList)
        {
            PathNode lowestFCostNode = pathNodeList[0];
            for (int i = 0; i < pathNodeList.Count; i++)
            {
                if (pathNodeList[i].FCost < lowestFCostNode.FCost)
                {
                    lowestFCostNode = pathNodeList[i];
                }
            }
            return lowestFCostNode;
        }
    }
}

