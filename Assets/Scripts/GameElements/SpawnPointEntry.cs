using UnityEngine;
namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Editor entry to hold spawn positions.
    /// </summary>
    public class SpawnPointEntry : MonoBehaviour
    {
        [SerializeField] public Transform[] SpawnPositions;
    }
}