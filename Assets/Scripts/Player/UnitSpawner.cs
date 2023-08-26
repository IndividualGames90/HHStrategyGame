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
        [SerializeField] private UnitSignalHub m_unitSignalHub;
        [SerializeField] private PlayerController m_playerController;

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
                var unit = m_unitFactory.Create(m_unitPrefabNames[playerNumberIndex].Name,
                                                spawnPointEntry.SpawnPositions[i].position,
                                                Quaternion.identity);

                var unitController = unit.GetComponent<UnitController>();
                unitController.Init(m_playerController.GetComponent<ResourceController>());

                m_unitSignalHub.RegisterToHub(unitController.ResourceCollected);
                m_unitSignalHub.Initialized = true;
            }
        }
    }
}