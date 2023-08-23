using System.Collections.Generic;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Astar testing.
    /// </summary>
    public class Testing : MonoBehaviour
    {
        [SerializeField] private bool m_enabled = true;

        private void Start()
        {
            if (!m_enabled) { return; }

            PathFinding pathFinding = new(10, 10);

            ///Get world position here
            var x = 7;
            var y = 9;
            List<PathNode> path = pathFinding.FindPath(0, 0, x, y);

            if (path != null)
            {
                foreach (PathNode node in path)
                {
                    Debug.Log($" {node.X} {node.Y} {node.HCost} {node.FCost} {node.GCost}");
                }
            }
        }
    }
}