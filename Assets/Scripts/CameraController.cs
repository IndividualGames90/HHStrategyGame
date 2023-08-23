using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Moves the camera to mouse input.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float m_scrollSpeed = 5.0f;
        [SerializeField] private float m_scrollZone = 20.0f;
        [SerializeField] private float m_cameraHeight;

        private Transform m_transform;
        private Vector3 m_previousInput;
        private Vector3 m_currentInput;

        private int m_screenWidth;
        private int m_screenHeight;


        private void Awake()
        {
            m_transform = transform;

            m_cameraHeight = m_transform.localPosition.y;

            m_screenWidth = Screen.width;
            m_screenHeight = Screen.height;
        }


        void Update()
        {
            m_currentInput = m_transform.position;

            var cameraMoved = m_currentInput.x != m_previousInput.x ||
                              m_currentInput.y != m_previousInput.y;
            if (!cameraMoved)
            {
                return;
            }

            if (Input.mousePosition.x < m_scrollZone)
            {
                m_currentInput.x += m_scrollSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x > m_screenWidth - m_scrollZone)
            {
                m_currentInput.x -= m_scrollSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.y < m_scrollZone)
            {
                m_currentInput.z += m_scrollSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.y > m_screenHeight - m_scrollZone)
            {
                m_currentInput.z -= m_scrollSpeed * Time.deltaTime;
            }

            m_currentInput.y = m_cameraHeight;
            m_transform.position = m_currentInput;
        }
    }
}