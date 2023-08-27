using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Core Photon functionality and encapsulation.
    /// </summary>
    public class PhotonController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private CanvasEventHub m_canvasEventHub;

        public static int PlayerNumber => PhotonNetwork.LocalPlayer.ActorNumber;
        public readonly static BasicSignal JoinedRoom = new();

        private const string c_roomName = "HappyHourStrategyCase.ServerRoom01";


        void Awake()
        {
            m_canvasEventHub.JoinGame.Connect(JoinLobby);
            PhotonNetwork.ConnectUsingSettings();
        }


        private void JoinLobby()
        {
            PhotonNetwork.JoinLobby();
        }


        public static bool RoomIsMaxxed()
        {
            return PhotonNetwork.CurrentRoom.PlayerCount > PhotonNetwork.CurrentRoom.MaxPlayers;
        }


        public override void OnJoinedLobby()
        {
            PhotonNetwork.JoinOrCreateRoom(c_roomName,
                                           new RoomOptions
                                           {
                                               MaxPlayers = 2,
                                               IsOpen = true,
                                               IsVisible = true
                                           },
                                           TypedLobby.Default);
        }


        public override void OnJoinedRoom()
        {
            if (RoomIsMaxxed())
            {
                OnJoinRandomFailed(0, "Room is full.");
                OnLeftRoom();
            }
            else
            {
                JoinedRoom.Emit();
            }
        }


        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.LogError($"{returnCode} {message}");
        }


        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"{returnCode} {message}");
        }


        public static void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }


        public override void OnLeftRoom()
        {
            PhotonNetwork.LeaveLobby();
        }
    }
}