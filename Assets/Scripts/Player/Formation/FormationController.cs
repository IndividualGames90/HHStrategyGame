using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Hold formation data and passes it to units.
    /// </summary>
    public class FormationController : MonoBehaviour
    {
        [SerializeField] private Transform[] m_formationPositions;

        /// <summary> True is reserved. </summary>
        private bool[] m_reservations;


        private void Awake()
        {
            m_reservations = new bool[m_formationPositions.Length];
        }


        public Transform ReserveFirstOrDefault()
        {
            for (int i = 0; i < m_reservations.Length; ++i)
            {
                if (!m_reservations[i])
                {
                    return m_formationPositions[i];
                }
            }
            return null;
        }


        public void ReleasePositions()
        {
            for (int i = 0; i < m_reservations.Length; i++)
            {
                m_reservations[i] = false;
            }
        }
    }
}