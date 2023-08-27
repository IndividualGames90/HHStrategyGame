using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Hold formation data and passes it to units.
    /// </summary>
    public class FormationController : MonoBehaviour
    {
        [SerializeField] private Transform[] m_formationPositions;

        /// <summary> True means position is reserved. </summary>
        private bool[] m_reservations;


        private void Awake()
        {
            m_reservations = new bool[m_formationPositions.Length];
            ReleaseAllPositions();
        }


        public void UpdatePosition(Vector3 a_newPosition)
        {
            ReleaseAllPositions();
            transform.position = a_newPosition;
        }


        public void ReleasePosition(int a_index)
        {
            if (!IndexWithinBounds(a_index))
            {
                return;
            }

            m_reservations[a_index] = false;
        }


        public void ReleaseAllPositions()
        {
            for (int i = 0; i < m_reservations.Length; i++)
            {
                m_reservations[i] = false;
            }
        }


        /// <summary> Retrieve first available spot on the formation or return null. </summary>
        /// <returns>Index, Transform tuple</returns>
        public (int, Transform) ReserveFirstAvailableOrDefault()
        {
            for (int i = 0; i < m_reservations.Length; ++i)
            {
                if (!m_reservations[i])
                {
                    m_reservations[i] = true;
                    return (i, m_formationPositions[i]);
                }
            }
            return (-1, null);
        }


        public bool IndexWithinBounds(int a_index)
        {
            return 0 <= a_index && a_index < m_reservations.Length;
        }
    }
}