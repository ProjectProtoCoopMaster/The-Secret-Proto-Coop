using Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class MainMenuBehavior : MonoBehaviour
    {
        public Transform playerVR;
        public Vector3Variable playerVRPos;
        [SerializeField] CallableFunction _JoinRoom;
        [SerializeField] CallableFunction _CreateRoom;
        
        public void JoinRoom() => _JoinRoom.Raise();

        public void CreateRoom() => _CreateRoom.Raise();

        public void OpenScene()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
                SceneManager.UnloadScene(3);
            }
            else
            {
                SceneManager.LoadScene(2, LoadSceneMode.Additive);
                SceneManager.UnloadScene(3);
            }
        }

    }
}

