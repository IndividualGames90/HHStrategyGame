using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{

    /// <summary>
    /// 3D Grid controller
    /// </summary>
    public class GridController : MonoBehaviour
    {
        [SerializeField] private GameObject m_navNodePrefab;
        [SerializeField, Tooltip(ToolTips.NavNodeParent)] private Transform m_navNodesParent;
        [SerializeField] private int m_rows = 10;
        [SerializeField] private int m_columns = 10;
        [SerializeField] private Transform m_gridOrigin;

        private List<GameObject> m_navNodes = new();
        private PathFinding m_pathFinding = null;


        private void Awake()
        {
            m_pathFinding = new(m_rows, m_columns);

            GenerateGrid();

            transform.position = m_gridOrigin.position;
        }


        public List<GameObject> FindPath(int a_startX, int a_startY, int a_endX, int a_endY)
        {
            m_pathFinding.AssignIsWalkeables(m_navNodes);

            List<PathNode> path = m_pathFinding.FindPath(a_startX, a_startY, a_endX, a_endY);

            List<GameObject> returnList = new();

            foreach (var pathNode in path)
            {
                returnList.Add(m_navNodes.FirstOrDefault(node =>
                                    node.GetComponent<NavGridElement>().X == pathNode.X &&
                                    node.GetComponent<NavGridElement>().Y == pathNode.Y));
            }

            return returnList;
        }


        private void GenerateGrid()
        {
            for (int x = 0; x < m_rows; x++)
            {
                for (int y = 0; y < m_columns; y++)
                {
                    GameObject cube = Instantiate(
                                            m_navNodePrefab,
                                            m_gridOrigin.position +
                                            new Vector3(
                                                x * m_navNodePrefab.transform.localScale.x,
                                                0,
                                                y * m_navNodePrefab.transform.localScale.y),
                                            Quaternion.identity);

                    cube.name = $"NavNode {y}{x}";
                    cube.transform.parent = m_navNodesParent;
                    var element = cube.GetComponent<NavGridElement>();
                    element.X = x;
                    element.Y = y;

                    m_navNodes.Add(cube);
                }
            }
        }
    }
}
