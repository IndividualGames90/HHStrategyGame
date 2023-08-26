using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Exposure of Canvas related events.
    /// </summary>
    public class CanvasEventHub : MonoBehaviour
    {
        public readonly BasicSignal JoinGame = new();


        public void OnJoinGame()
        {
            JoinGame.Emit();
        }
    }
}