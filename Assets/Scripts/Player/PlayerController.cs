using System.Diagnostics;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Controls player tap and unit commands.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GridController m_gridController;

        public static int LayerMaskPlayer = 1 << 7;
        public static int LayerMaskGround = 1 << 6;

        private Touch m_touch;
        private Camera m_mainCamera;
        private Vector3 m_dragStartPosition;

        private Ray m_ray;
        private RaycastHit m_hit;

        private UnitSelector m_unitSelector = new();
        private NavGridElement m_destinationNavGridElement = null;

        private Stopwatch m_stopwatch = new();
        private const float c_updateInterval = .1f;
        private float m_elapsedTime;


        private void Awake()
        {
            m_mainCamera = Camera.main;
            m_stopwatch.Start();
        }


        void Update()
        {
            m_elapsedTime = m_stopwatch.Elapsed.Milliseconds / 1000f;
            if (m_elapsedTime > c_updateInterval)
            {
                MouseInput();//Had to have mouse input to test Multiplayer with editor/phone combo.
                TouchInput();
            }
        }


        private void MouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_stopwatch.Restart();

                OnTouchBegan(Input.mousePosition);
            }
        }


        private void TouchInput()
        {
            if (Input.touchCount > 0)
            {
                m_stopwatch.Restart();

                m_touch = Input.GetTouch(0);

                switch (m_touch.phase)
                {
                    case TouchPhase.Began:
                        OnTouchBegan(m_touch.position);
                        break;
                }
            }
        }


        private void OnTouchBegan(Vector2 a_touchPosition)
        {
            m_dragStartPosition = m_mainCamera.ScreenToWorldPoint(a_touchPosition);
            m_dragStartPosition.z = 0;

            m_ray = m_mainCamera.ScreenPointToRay(a_touchPosition);

            if (Physics.Raycast(m_ray, out m_hit, Mathf.Infinity, LayerMaskPlayer))
            {
                GameObject hitUnit = m_hit.collider.gameObject;
                m_unitSelector.SelectUnit(hitUnit);
            }
            else if (Physics.Raycast(m_ray, out m_hit, Mathf.Infinity, LayerMaskGround))
            {
                var point = m_hit.point;
                m_destinationNavGridElement = m_hit.transform.GetComponent<NavGridElement>();

                if (m_destinationNavGridElement != null)
                {
                    m_unitSelector.MoveUnitsTo(m_gridController, m_destinationNavGridElement);
                }
            }
        }
    }
}

