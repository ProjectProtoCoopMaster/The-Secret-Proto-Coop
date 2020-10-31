using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Gameplay.Networking
{

    public class TransmitterManager : MonoBehaviour
    {
        [SerializeField] private PhotonView photonView;
        [SerializeField] private Vector3Variable playerVRPosition;

        public void SendPlayerVRPosToOthers(Vector3Variable playerVRPosition)
        {
            photonView.RPC("SendVector3", RpcTarget.Others, playerVRPosition.Value);
        }

        [PunRPC]
        private void SendVector3(Vector3 position) => playerVRPosition.Value = position;

    }
}

