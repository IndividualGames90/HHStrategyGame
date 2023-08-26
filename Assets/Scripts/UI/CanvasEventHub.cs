using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Exposure of Canvas related events.
    /// </summary>
    public class CanvasEventHub : MonoBehaviour
    {
        public readonly BasicSignal JoinGame = new();
        public readonly BasicSignal<int> VolumeChanged = new();


        public void OnJoinGame()
        {
            JoinGame.Emit();
        }


        public void OnVolumeChanged(int a_volume)
        {
            VolumeChanged.Emit(a_volume);
        }
    }
}