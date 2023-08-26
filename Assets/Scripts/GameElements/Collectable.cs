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


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<UnitController>() is UnitController unitController)
            {
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
