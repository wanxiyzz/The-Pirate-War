using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace MyGame.Net
{
    public class NetworkLauncher : MonoBehaviourPunCallbacks
    {
        public void JoinAndCreateRoom()
        {
            RoomOptions options = new RoomOptions
            {
                MaxPlayers = 12,
            };
            PhotonNetwork.JoinOrCreateRoom("main", options, TypedLobby.Default);
        }
    }
}