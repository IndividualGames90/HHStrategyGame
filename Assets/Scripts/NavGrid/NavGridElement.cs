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
        private bool m_obstacleNode = false;

        public bool IsObstacle()
        {
            return m_obstacleNode;
            //return m_meshRenderer.material == m_obstacle;
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

            /*var sphereRadius = 5f;
            var castDistance = 5f;
            RaycastHit hit;

            Vector3 boxSize = transform.localScale;
            Vector3 boxCenter = transform.position - transform.forward * (boxSize.z * 2);

            //Debug.DrawRay(boxCenter, transform.forward, Color.red, 10f);

            // Visualize the box cast
            Debug.DrawLine(boxCenter, boxCenter + transform.forward * castDistance, Color.red, 100f);
            DebugDrawBox(boxCenter, boxSize / 2.0f, transform.rotation, Color.red);

            if (Physics.BoxCast(boxCenter,
                                boxSize,
                                transform.forward,
                                out hit,
                                transform.rotation,
                                castDistance,
                                m_obstacleLayer))
            {
                SetMaterialObstacle();
            }*/
        }



        // Visualize a rotated box
        private void DebugDrawBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, Color color)
        {
            Matrix4x4 matrix = Matrix4x4.TRS(center, orientation, Vector3.one);
            Vector3 frontBottomLeft = matrix.MultiplyPoint(-halfExtents);
            Vector3 frontBottomRight = matrix.MultiplyPoint(new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z));
            Vector3 frontTopLeft = matrix.MultiplyPoint(new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z));
            Vector3 frontTopRight = matrix.MultiplyPoint(new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z));
            Vector3 backBottomLeft = matrix.MultiplyPoint(new Vector3(-halfExtents.x, -halfExtents.y, halfExtents.z));
            Vector3 backBottomRight = matrix.MultiplyPoint(new Vector3(halfExtents.x, -halfExtents.y, halfExtents.z));
            Vector3 backTopLeft = matrix.MultiplyPoint(halfExtents);
            Vector3 backTopRight = matrix.MultiplyPoint(new Vector3(-halfExtents.x, halfExtents.y, halfExtents.z));

            Debug.DrawLine(frontBottomLeft, frontBottomRight, color, 100f);
            Debug.DrawLine(frontBottomRight, frontTopRight, color, 100f);
            Debug.DrawLine(frontTopRight, frontTopLeft, color, 100f);
            Debug.DrawLine(frontTopLeft, frontBottomLeft, color, 100f);

            Debug.DrawLine(backBottomLeft, backBottomRight, color, 100f);
            Debug.DrawLine(backBottomRight, backTopRight, color, 100f);
            Debug.DrawLine(backTopRight, backTopLeft, color, 100f);
            Debug.DrawLine(backTopLeft, backBottomLeft, color, 100f);

            Debug.DrawLine(frontBottomLeft, backBottomLeft, color, 100f);
            Debug.DrawLine(frontBottomRight, backBottomRight, color, 100f);
            Debug.DrawLine(frontTopRight, backTopRight, color, 100f);
            Debug.DrawLine(frontTopLeft, backTopLeft, color, 100f);
        }



        public void SetMaterialObstacle()
        {
            if (m_meshRenderer != null && m_obstacle != null)
            {
                m_obstacleNode = true;
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