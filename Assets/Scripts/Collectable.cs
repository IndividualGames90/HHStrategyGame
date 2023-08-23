using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Interract with players to be collected.
    /// </summary>
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private int m_collectableValue = 10;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<UnitController>() is UnitController unitController)
            {
                unitController.CollectedResource();
                Destroy(gameObject);
            }
        }
    }
}
