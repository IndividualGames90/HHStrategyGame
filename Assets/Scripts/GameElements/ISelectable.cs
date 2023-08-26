using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Injection for selectable elements by the SelectionBox.
    /// </summary>
    public interface ISelectable
    {
        public abstract GameObject GameObject();
    }
}