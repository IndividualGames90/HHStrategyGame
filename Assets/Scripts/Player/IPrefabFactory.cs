using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Prefab Factory Interface. Factory to create prefabs.
    /// </summary>
    public interface IPrefabFactory
    {
        public abstract GameObject Create(string a_prefabName, Vector3 a_position, Quaternion a_rotation);
    }
}