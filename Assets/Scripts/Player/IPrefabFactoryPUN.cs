using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Prefab Factory Interface for PUN Prefabs.
    /// Requirements: Prefab have to be in root/Resources folder and created by name.
    /// </summary>
    public interface IPrefabFactoryPUN
    {
        public abstract GameObject Create(string a_prefabName, Vector3 a_position, Quaternion a_rotation);
    }
}