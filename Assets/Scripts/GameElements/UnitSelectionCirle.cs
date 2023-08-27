using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Enables/Disabled the selection circle around the unit.
    /// </summary>
    public class UnitSelectionCirle : MonoBehaviour
    {
        [SerializeField] private GameObject m_selectionCircle;


        private void Awake()
        {
            m_selectionCircle.SetActive(false);
        }


        public void Selected()
        {
            m_selectionCircle.SetActive(true);
        }
    }
}

