using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// This is not a real class. This is a hack to circumvent the lack of scene management
    /// and complex occulusion culling as they are not in the scope of this case.
    /// 
    /// It handles occlusion on gameobjects not visible during the menu.
    /// 
    /// This is also a primitive singleton, not a good one.
    /// </summary>
    public class OptimizationController : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_gameElements;
        [SerializeField] private GameObject m_navGridParent;

        public static OptimizationController Instance => m_instance;
        private static OptimizationController m_instance;

        private bool m_navGridVisible = false;

        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            DisableGameElements();
        }


        public void EnableGameElements()
        {
            ToggleGameObjects(m_gameElements, true);
            ToggleNavGrid();
        }


        public void DisableGameElements()
        {
            ToggleGameObjects(m_gameElements, false);
        }


        public void ToggleNavGrid()
        {
            Debug.Log("a");
            m_navGridVisible = !m_navGridVisible;
            ToggleMeshRenderers(m_navGridParent, m_navGridVisible);
        }


        private void ToggleGameObjects(GameObject[] a_go, bool a_toggle)
        {
            foreach (var go in a_go)
            {
                go.SetActive(a_toggle);
            }
        }


        private void ToggleMeshRenderers(GameObject a_go, bool a_toggle)
        {
            foreach (var meshRenderer in a_go.GetComponentsInChildren<MeshRenderer>())
            {
                meshRenderer.enabled = a_toggle;
            }
        }
    }
}