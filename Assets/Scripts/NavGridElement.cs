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

        private int m_obstacleLayer = 1 << 8;


        public bool IsObstacle()
        {
            return m_meshRenderer.material == m_obstacle;
        }


        /*private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Obstacle) && !IsObstacle())
            {
                SetMaterialObstacle();
            }
        }*/


        private void Awake()
        {
            m_meshRenderer = GetComponent<MeshRenderer>();

            var sphereRadius = 1f;
            var castDistance = 5f;
            RaycastHit hit;

            if (Physics.SphereCast(transform.position,
                                   sphereRadius,
                                   transform.forward,
                                   out hit,
                                   castDistance,
                                   m_obstacleLayer))
            {
                SetMaterialObstacle();
            }
        }


        public void SetMaterialObstacle()
        {
            if (m_meshRenderer != null && m_obstacle != null)
            {
                m_meshRenderer.material = m_obstacle;
            }
        }

        public void SetMaterialDefault()
        {
            if (m_meshRenderer != null && m_default != null)
            {
                m_meshRenderer.material = m_default;
            }
        }

        public void SetMaterialOpenList()
        {
            if (m_meshRenderer != null && m_openList != null)
            {
                m_meshRenderer.material = m_openList;
            }
        }

        public void SetMaterialClosedList()
        {
            if (m_meshRenderer != null && m_closedList != null)
            {
                m_meshRenderer.material = m_closedList;
            }
        }
    }
}