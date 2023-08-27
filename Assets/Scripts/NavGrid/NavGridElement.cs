using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Changes the material depending on navigation checks.
    /// </summary>
    public class NavGridElement : MonoBehaviour
    {
        [SerializeField] private Material m_obstacle;
        [SerializeField] private Material m_default;
        [SerializeField] private Material m_openList;
        [SerializeField] private Material m_closedList;

        private MeshRenderer m_meshRenderer;

        [HideInInspector] public int X;
        [HideInInspector] public int Y;

        private bool m_obstacleNode = false;


        public bool IsObstacle()
        {
            return m_obstacleNode;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Obstacle) && !IsObstacle())
            {
                SetMaterialObstacle();
            }
        }


        private void Awake()
        {
            m_meshRenderer = GetComponent<MeshRenderer>();
        }


        public void SetMaterialObstacle()
        {
            if (m_meshRenderer != null && m_obstacle != null)
            {
                m_obstacleNode = true;
                m_meshRenderer.material = m_obstacle;
            }
        }
    }
}