using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Exposure of Canvas related events.
    /// </summary>
    public class CanvasEventHub : MonoBehaviour
    {
        public readonly BasicSignal JoinGame = new();
        public readonly BasicSignal<float> VolumeChanged = new();


        public void OnJoinGame()
        {
            JoinGame.Emit();
        }


        public void OnVolumeChanged(float a_volume)
        {
            VolumeChanged.Emit(a_volume);
        }
    }
}