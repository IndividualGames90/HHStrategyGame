using Photon.Pun;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Interract with players to be collected.
    /// </summary>
    public class Collectable : MonoBehaviourPun
    {
        [SerializeField] private int m_collectableValue = 10;
        private bool m_collected = false;


        private void OnTriggerEnter(Collider other)
        {
            if (m_collected)
            {
                return;
            }

            if (other.gameObject.GetComponent<UnitController>() is UnitController unitController)
            {
                m_collected = true;
                unitController.CollectedResource();
                DestroyThis();
            }
        }


        public void DestroyThis()
        {
            GetComponent<PhotonView>().RPC("DestroyOnNetwork", RpcTarget.All);
        }


        [PunRPC]
        public void DestroyOnNetwork()
        {
            PhotonController.Destroy(gameObject);
        }
    }
}
