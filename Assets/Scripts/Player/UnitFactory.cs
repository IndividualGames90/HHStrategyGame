using Photon.Pun;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Prefab Factory Implementation. Factory to create prefabs.
    /// </summary>
    public class UnitFactory : IPrefabFactoryPUN
    {
        public virtual GameObject Create(string a_prefabName, Vector3 a_position, Quaternion a_rotation)
        {
            return PhotonNetwork.Instantiate(a_prefabName, a_position, a_rotation);
        }
    }
}