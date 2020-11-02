using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Gameplay;

namespace Networking
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameEvent _OnConnectedToServer;
        [SerializeField] private GameEvent _OnJoinRoomFailed;
        [SerializeField] private GameEvent _OnRoomFulled;

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();

        }
        public static void JoinRoom()
        {
            PhotonNetwork.JoinRoom("1");

        }

        public static void CreateRoom()
        {
            RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
            PhotonNetwork.CreateRoom("1", roomOptions);

        }

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                _OnRoomFulled.Raise();
            }
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            _OnJoinRoomFailed.Raise();
        }
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            _OnConnectedToServer.Raise();
        }

        
    }
}

