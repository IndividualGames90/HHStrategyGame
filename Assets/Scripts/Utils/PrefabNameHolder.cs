using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// This converts the prefab name to string.
    /// 
    /// Intention of this class is to express why this class is used.
    /// Otherwise we just pass a GameObject which has no clear intention why
    /// it was declared.
    /// 
    /// Normally a class like this holds Keys or Hash for lookups but here it's primitive, even redundant.
    /// </summary>
    public class PrefabNameHolder : MonoBehaviour
    {
        public string Name => gameObject.name;
    }
}