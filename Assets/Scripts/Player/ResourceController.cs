using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Handles player's resources.
    /// </summary>
    public class ResourceController : MonoBehaviour
    {
        private int m_resourceCount = 0;
        private const int m_woodResourceIncrease = 10;


        public int ResourceCollected()
        {
            return m_resourceCount += m_woodResourceIncrease;
        }
    }
}