using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Injection for selectable elements by the SelectionBox.
    /// 
    /// DevNote: I don't use double spacing in interfaces as they are just declarations and 
    /// easier to read when closer.
    /// </summary>
    public interface ISelectable
    {
        public abstract GameObject GameObject();
    }
}