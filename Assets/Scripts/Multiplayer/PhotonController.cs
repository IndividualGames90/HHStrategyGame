using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Core Photon functionality.
    /// </summary>
    public class PhotonController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private CanvasEventHub m_canvasEventHub;

        public readonly static BasicSignal JoinedRoom = new();

        private const string c_roomName = "HappyHourStrategyCase.ServerRoom01";

        public static int PlayerNumber => PhotonNetwork.LocalPlayer.ActorNumber;


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


        public new static void Destroy(Object a_destroyed)
        {
            var go = (GameObject)a_destroyed;
            var photonView = go.GetComponent<PhotonView>();

            PhotonNetwork.Destroy(go);
        }


        public override void OnLeftRoom()
        {
            PhotonNetwork.LeaveLobby();
        }
    }
}