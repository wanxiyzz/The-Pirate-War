using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
namespace MyGame.UISystem.Buttons
{
    public class Buttonstart : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(JoinAndCreateRoom);
        }

        public void JoinAndCreateRoom()
        {
            Debug.Log("点击了开始游戏按钮");
            PhotonNetwork.ConnectUsingSettings();
            UIManager.Instance.OpenLodingUI("连接服务器中...");
        }
    }
}