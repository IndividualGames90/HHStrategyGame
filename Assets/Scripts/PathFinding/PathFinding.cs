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
        private List<PathNode> m_openList;
        private List<PathNode> m_closedList;

        private const int c_diagonalMoveCost = 14;
        private const int c_straightMovecost = 10;


        public PathFinding(int a_width, int a_height)
        {
            m_grid = new(a_width, a_height);
        }

        public void AssignIsWalkables(List<GameObject> m_navNodes)
        {
            foreach (GameObject navnode in m_navNodes)
            {
                var element = navnode.GetComponent<NavGridElement>();
                var pathNode = m_grid.GetGridObject(element.X, element.Y);
                var isObstacle = element.IsObstacle();
                if (isObstacle)
                {
                    pathNode.Walkable = !isObstacle;
                }
            }
        }


        public List<PathNode> FindPath(int a_startX, int a_startY, int a_endX, int a_endY)
        {
            PathNode startNode = m_grid.GetGridObject(a_startX, a_startY);
            PathNode endNode = m_grid.GetGridObject(a_endX, a_endY);

            m_openList = new() { startNode };
            m_closedList = new();

            for (int x = 0; x < m_grid.Width; x++)
            {
                for (int y = 0; y < m_grid.Height; y++)
                {
                    PathNode pathNode = m_grid.GetGridObject(x, y);
                    pathNode.GCost = int.MaxValue;
                    pathNode.CameFromNode = null;
                }
            }

            startNode.GCost = 0;
            startNode.HCost = CalculateDistanceCost(startNode, endNode);

            while (m_openList.Count > 0)
            {
                PathNode currentNode = NodeWithLowestFCost(m_openList);

                if (currentNode == endNode)
                {
                    //Reached final node
                    return CalculatePath(endNode);
                }

                m_openList.Remove(currentNode);
                m_closedList.Add(currentNode);

                foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
                {
                    if (m_closedList.Contains(neighbourNode))
                    {
                        continue;
                    }
                    if (!neighbourNode.Walkable)
                    {
                        m_closedList.Add(neighbourNode);
                        continue;
                    }

                    int tentativeGCost = currentNode.GCost +
                                         CalculateDistanceCost(currentNode, neighbourNode);

                    if (tentativeGCost < neighbourNode.GCost)
                    {
                        neighbourNode.CameFromNode = currentNode;
                        neighbourNode.GCost = tentativeGCost;
                        neighbourNode.HCost = CalculateDistanceCost(neighbourNode, endNode);

                        if (!m_openList.Contains(neighbourNode))
                        {
                            m_openList.Add(neighbourNode);
                        }
                    }
                }
            }

            return null;///No nodes remains in openList
        }


        /// <summary> Detect eighthold neighbours of the given PathNode. </summary>
        private List<PathNode> GetNeighbourList(PathNode a_currentNode)
        {
            List<PathNode> neighbourList = new();

            if (a_currentNode.X - 1 >= 0)///Left
            {
                neighbourList.Add(GetNode(a_currentNode.X - 1, a_currentNode.Y));
                if (a_currentNode.Y - 1 >= 0)///Left Down
                {
                    neighbourList.Add(GetNode(a_currentNode.X - 1, a_currentNode.Y - 1));
                }
                if (a_currentNode.Y + 1 < m_grid.Height)///Left Up
                {
                    neighbourList.Add(GetNode(a_currentNode.X - 1, a_currentNode.Y + 1));
                }
            }
            if (a_currentNode.X + 1 < m_grid.Width)///Right
            {
                neighbourList.Add(GetNode(a_currentNode.X + 1, a_currentNode.Y));
                if (a_currentNode.Y - 1 >= 0)///Right Down
                {
                    neighbourList.Add(GetNode(a_currentNode.X + 1, a_currentNode.Y - 1));
                }
                if (a_currentNode.Y + 1 < m_grid.Height)///Right Up
                {
                    neighbourList.Add(GetNode(a_currentNode.X + 1, a_currentNode.Y + 1));
                }
            }
            if (a_currentNode.Y - 1 >= 0)///Down
            {
                neighbourList.Add(GetNode(a_currentNode.X, a_currentNode.Y - 1));
            }
            if (a_currentNode.Y + 1 < m_grid.Height)///Up
            {
                neighbourList.Add(GetNode(a_currentNode.X, a_currentNode.Y + 1));
            }

            return neighbourList;
        }


        /// <summary> Grid access with coordinates. </summary>
        private PathNode GetNode(int a_x, int a_y)
        {
            return m_grid.GetGridObject(a_x, a_y);
        }


        private List<PathNode> CalculatePath(PathNode a_endNode)
        {
            List<PathNode> path = new() { a_endNode };
            PathNode currentNode = a_endNode;
            while (currentNode.CameFromNode != null)
            {
                path.Add(currentNode.CameFromNode);
                currentNode = currentNode.CameFromNode;
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


        private PathNode NodeWithLowestFCost(List<PathNode> a_pathNodeList)
        {
            PathNode lowestFCostNode = a_pathNodeList[0];
            for (int i = 0; i < a_pathNodeList.Count; i++)
            {
                if (a_pathNodeList[i].FCost < lowestFCostNode.FCost)
                {
                    lowestFCostNode = a_pathNodeList[i];
                }
            }
            return lowestFCostNode;
        }
    }
}

