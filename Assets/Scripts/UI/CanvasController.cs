using TMPro;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// UI element callback handler.
    /// </summary>
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_playerLabel;


        private void Awake()
        {
            PhotonController.JoinedRoom.Connect(PlayerJoined);
        }


        public void ExitGame()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                Debug.Log("App Exiting.");
                Application.Quit();
            }
        }


        public void PlayerJoined()
        {
            m_playerLabel.text = $"Player {PhotonController.PlayerNumber}";
        }
    }
}