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

        private List<ISelectable> m_selectableUnits = new();


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

                OnTouch(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                OnDrag(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnRelease();
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
                        OnTouch(m_touch.position);
                        break;

                    case TouchPhase.Moved:
                        OnDrag(m_touch.position);
                        break;

                    case TouchPhase.Ended:
                        OnRelease();
                        break;
                }
            }
        }


        private void OnTouch(Vector2 a_touchPosition)
        {
            m_dragStartPosition = a_touchPosition;

            m_ray = m_mainCamera.ScreenPointToRay(a_touchPosition);

            if (RayCastToPlayer())
            {
                m_unitSelector.SelectUnit(m_hit.collider.gameObject);
            }
            else if (RayCastToGround())
            {
                m_destinationNavGridElement = m_hit.transform.GetComponent<NavGridElement>();

                if (m_destinationNavGridElement != null)
                {
                    m_unitSelector.MoveUnitsTo(m_gridController, m_destinationNavGridElement);
                }
            }
        }


        private void OnDrag(Vector2 a_touchPosition)
        {
            m_selectionBox.StartSelecting(m_dragStartPosition,
                                          a_touchPosition,
                                          m_selectableUnits);
        }


        private void OnRelease()
        {
            m_selectionBox.DisableSelectionBox();
        }


        public void RegisterUnit(UnitController unitController)
        {
            m_selectableUnits.Add(unitController);
        }


        private void OnUnitSelected(ISelectable a_selectedUnit)
        {
            if (a_selectedUnit is UnitController unitController)
            {
                UnityEngine.Debug.Log("TODO!");
            }
        }


        private bool RayCastToPlayer()
        {
            return Physics.Raycast(m_ray, out m_hit, Mathf.Infinity, LayerMaskPlayer);
        }


        private bool RayCastToGround()
        {
            return Physics.Raycast(m_ray, out m_hit, Mathf.Infinity, LayerMaskGround);
        }
    }
}

