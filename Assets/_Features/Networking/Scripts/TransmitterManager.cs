using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Gameplay.Networking
{

    public class TransmitterManager : MonoBehaviour
    {
        static PhotonView photonView;
        [SerializeField] private Vector3Variable playerVRPosition;

        private void OnEnable()
        {
            photonView = GetComponent<PhotonView>();
        }
        public static void SendPlayerVRPosToOthers(Vector3 playerVRPos)
        {
            photonView.RPC("SendVector3", RpcTarget.Others, playerVRPos);
        }

        [PunRPC]
        private void SendVector3(Vector3 position) => playerVRPosition.Value = position;
    }
}

