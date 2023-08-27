using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    public interface IPool
    {
        public abstract void Initialize();
        public abstract GameObject Retrieve();
    }
}