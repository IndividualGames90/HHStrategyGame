using System.Collections.Generic;
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
        [SerializeField] private RectTransform m_selectionBoxView;

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

        private SelectionBox m_selectionBox;

        private List<UnitController> m_selectableUnits = new();


        private void Awake()
        {
            m_mainCamera = Camera.main;
            m_stopwatch.Start();
            m_selectionBox = new(m_selectionBoxView);
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


        public void RegisterUnit(UnitController unitController)
        {
            m_selectableUnits.Add(unitController);
        }
    }

    public class SelectionBox
    {
        private float m_width;
        private float m_height;

        private RectTransform m_selectionBox;
        private Vector2 m_anchoredPositionAdjustment;
        private Vector2 m_sizeDeltaAdjustment;
        private Bounds m_bounds;
        private Camera m_mainCamera;


        public SelectionBox(RectTransform a_selectionBox)
        {
            m_selectionBox = a_selectionBox;
            m_mainCamera = Camera.main;
        }


        public void StartSelecting(Vector2 a_startPosition, Vector2 a_endPosition, List<GameObject> a_selectables)
        {
            ResizeSelectionBox(a_startPosition, a_endPosition);
            CheckSelectionBox(a_selectables);
        }


        private void ResizeSelectionBox(Vector2 a_startPosition, Vector2 a_endPosition)
        {
            m_width = a_endPosition.x - a_startPosition.x;
            m_height = a_endPosition.y - a_endPosition.y;

            m_anchoredPositionAdjustment.x = m_width / 2;
            m_anchoredPositionAdjustment.y = m_height / 2;

            m_sizeDeltaAdjustment.x = Mathf.Abs(m_width);
            m_sizeDeltaAdjustment.y = Mathf.Abs(m_height);

            m_selectionBox.anchoredPosition = a_startPosition + m_anchoredPositionAdjustment;
            m_selectionBox.sizeDelta = m_sizeDeltaAdjustment;

            m_bounds.center = m_selectionBox.anchoredPosition;
            m_bounds.size = m_selectionBox.sizeDelta;
        }


        private void CheckSelectionBox(List<GameObject> a_selectables)
        {
            foreach (var selectable in a_selectables)
            {
                if (InsideSelectionBox(m_mainCamera.WorldToScreenPoint(selectable.transform.position), m_bounds))
                {
                    UnityEngine.Debug.Log($"Inside Selectedion Box");
                }
                else
                {
                    UnityEngine.Debug.Log($"Outside Selectedion Box");
                }
            }
        }


        private bool InsideSelectionBox(Vector3 a_selectionPosition, Bounds a_bounds)
        {
            return a_selectionPosition.x > a_bounds.min.x &&
                   a_selectionPosition.x < a_bounds.max.x &&
                   a_selectionPosition.y > a_bounds.min.y &&
                   a_selectionPosition.y < a_bounds.max.y;
        }
    }
}

