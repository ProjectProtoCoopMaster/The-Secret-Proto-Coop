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
        [SerializeField] private CallableFunction _switch;

        public void SendPlayerVRPosToOthers(Vector3Variable playerVRPosition)=> photonView.RPC("SendVector3", RpcTarget.Others, playerVRPosition.Value);
        [PunRPC]private void SendVector3(Vector3 position) => playerVRPosition.Value = position;

        public void SendSwicherChangeToOthers(float ID) => photonView.RPC("SendSwitcherChange", RpcTarget.Others, ID);
        [PunRPC] public void SendSwitcherChange(float ID) => _switch.Raise(ID);

    }
}

