using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Unit spawner for Players.
    /// </summary>
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnPointEntry[] m_spawnPointEntries;
        [SerializeField] private PrefabNameHolder[] m_unitPrefabNames;

        private UnitFactory m_unitFactory;


        private void Awake()
        {
            PhotonController.JoinedRoom.Connect(SpawnUnits);
        }


        private void SpawnUnits()
        {
            if (m_unitFactory == null)
            {
                m_unitFactory = new();
            }

            var playerNumberIndex = PhotonController.PlayerNumber - 1;
            var spawnPointEntry = m_spawnPointEntries[playerNumberIndex];
            for (int i = 0; i < spawnPointEntry.SpawnPositions.Length; i++)
            {
                m_unitFactory.Create(m_unitPrefabNames[playerNumberIndex].Name,
                                     spawnPointEntry.SpawnPositions[i].position,
                                     Quaternion.identity);
            }
        }
    }
}