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

        private UnitSelector m_unitSelector;
        private Touch m_touch;
        private Camera m_mainCamera;
        private Vector3 m_dragStartPosition;

        private Ray m_ray;
        private RaycastHit m_hit;

        private NavGridElement m_destinationNavGridElement = null;


        private void Awake()
        {
            m_mainCamera = Camera.main;

            m_unitSelector = new();
        }

        void Update()
        {
            TouchInput();
        }

        private void TouchInput()
        {
            if (Input.touchCount > 0)
            {
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


        /*private LineRenderer selectionBoxRenderer;
        private bool selectionBoxActive = false;

        private Vector3 m_dragStartPosition;
        private bool m_dragging = false;

        private Touch m_touch;
        private Camera m_mainCamera;

        private Vector3 m_currentDragPosition;
        private Vector3 m_dragEndPosition;


        private Vector3 m_mouseDownPosition;
        private Vector3 m_mouseDragPosition;
        private Vector3 m_mouseUpPosition;

        private const float c_selectionDragMinimumSize = 40;
        private bool m_dragSelecting = false;

        private MeshCollider m_selectionBox;
        private Mesh m_selectionMesh;


        


        private void SelectUnits


        private Mesh GenerateSelectionMesh(Vector3[] a_meshCorners)
        {
            Vector3[] vertices = new Vector3[8];
            int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 8, 1, 1, 4, 8, 4, 5, 6, 6, 5, 7 };

            ///Bottom rectangle
            for (int i = 0; i < 4; i++)
            {
                vertices[i] = a_meshCorners[i];
            }

            ///Top rectangle
            for (int j = 4; j < 8; j++)
            {
                vertices[j] = a_meshCorners[j - 4] + Vector3.up * 100.0f;
            }

            Mesh selectionMesh = new();
            selectionMesh.vertices = vertices;
            selectionMesh.triangles = tris;

            return selectionMesh;
        }

        private Vector3[] SelectionMeshCorners()
        {
            Vector3[] corners = new Vector3[4];

            corners[0] = m_mouseDownPosition;

            corners[1].x =

            corners[4] = m_mouseUpPosition;
        }


        private void OnTriggerEnter(Collider other)
        {

        }


        private void MouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_mouseDownPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                m_mouseDragPosition = Input.mousePosition;
                if ((m_mouseDownPosition - m_mouseDragPosition).magnitude > c_selectionDragMinimumSize)
                {
                    m_dragSelecting = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                m_mouseUpPosition = Input.mousePosition;
            }
        }


        private void TouchInput()
        {
            if (Input.touchCount > 0)
            {
                m_touch = Input.GetTouch(0);

                switch (m_touch.phase)
                {
                    case TouchPhase.Began:
                        OnTouchBegan(m_touch.position);
                        break;

                    case TouchPhase.Moved:
                        OnTouchMoved(m_touch.position);
                        break;

                    case TouchPhase.Ended:
                        OnTouchEnded();
                        break;
                }
            }
        }

        #region touch



        private void OnTouchMoved(Vector2 touchPosition)
        {
            if (m_dragging)
            {
                m_currentDragPosition = m_mainCamera.ScreenToWorldPoint(touchPosition);
                m_currentDragPosition.z = 0;

                DrawSelectionBox(m_dragStartPosition, m_currentDragPosition);
            }
        }


        private void OnTouchEnded()
        {
            if (m_dragging)
            {
                m_dragging = false;
                ClearSelectionBox();

                m_dragEndPosition = m_mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
                m_dragEndPosition.z = 0;

                SelectPlayersInBox(m_dragStartPosition, m_dragEndPosition);
            }
        }


        private void DrawSelectionBox(Vector3 start, Vector3 end)
        {
            Vector3[] corners = new Vector3[5];

            corners[0] = start;
            corners[1] = new Vector3(end.x, start.y, 0);
            corners[2] = end;
            corners[3] = new Vector3(start.x, end.y, 0);
            corners[4] = start;

            selectionBoxRenderer.positionCount = corners.Length;
            selectionBoxRenderer.SetPositions(corners);
            selectionBoxActive = true;
        }

        private void ClearSelectionBox()
        {
            selectionBoxRenderer.positionCount = 0;
            selectionBoxActive = false;
        }


        private void SelectPlayersInBox(Vector3 start, Vector3 end)
        {
            Vector3 min = Vector3.Min(start, end);
            float radius = Vector3.Max(start, end).magnitude;

            Collider[] colliders = Physics.OverlapSphere(min, radius, LayerMask.GetMask("Player0"));

            foreach (Collider collider in colliders)
            {
                // Handle selection logic for tagged objects here.
                Debug.Log("Selected: " + collider.gameObject.name);
            }
        }
        #endregion*/

    }
}

