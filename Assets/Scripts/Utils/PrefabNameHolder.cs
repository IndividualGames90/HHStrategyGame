using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// This converts the prefab name to string.
    /// 
    /// Intention of this class is to express why this type of class is being used as a member.
    /// Just passing a GameObject would hide the intention why it was declared.
    /// 
    /// A full version of this class would holds Keys or Hash for lookups.
    /// </summary>
    public class PrefabNameHolder : MonoBehaviour
    {
        public string Name => gameObject.name;
    }
}