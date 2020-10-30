using Gameplay.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class MainMenuBehavior : MonoBehaviour
    {
        public Transform playerVR;
        public Vector3Variable playerVRPos;
        public void JoinRoom() => NetworkManager.JoinRoom();

        public void CreateRoom() => NetworkManager.CreateRoom();

    }
}

