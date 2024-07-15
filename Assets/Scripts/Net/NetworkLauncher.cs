using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using MyGame.UISystem;
using MyGame.PlayerSystem;

namespace MyGame.Net
{
    //TODO:FU
    public class NetworkLauncher : MonoBehaviourPunCallbacks
    {
        public Dictionary<string, List<Player>> ships = new Dictionary<string, List<Player>>();
        public GameObject shipPrefab;
        private void Awake()
        {
            EventHandler.CountShipsAndPlayers += CountShipsAndPlayers;
        }
        public void DiffEnemyAndTeammate(string myShipName)
        {
            CountShipsAndPlayers();
            foreach (var player in PhotonNetwork.PlayerList)
            {

                if (player.NickName == PhotonNetwork.LocalPlayer.NickName)
                    continue;
                var playerController = GameObject.Find(player.NickName).GetComponent<PlayerController>();
                if (playerController.shipHomeName == myShipName)
                {
                    SetPlayerAttributes(player, "OwnSide", "Player");
                }
                else
                {
                    SetPlayerAttributes(player, "Enemy", "Enemy");
                    playerController.isEnemy = true;
                }
            }
        }
        public void CountShipsAndPlayers()
        {
            ships.Clear();
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                string shipName = GetPlayerShipName(player);
                if (shipName != string.Empty)
                {
                    if (!ships.ContainsKey(shipName))
                    {
                        ships.Add(shipName, new List<Player>());
                    }
                    ships[shipName].Add(player);
                }
            }
            UIManager.Instance.shipList.SetActive(true);
            UIManager.Instance.shipList.GetComponent<ShipList>().Init(ships);
        }
        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            UIManager.Instance.BackTittle();
            UIManager.Instance.TackWarningUI("房主已退出");
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            UIManager.Instance.TackWarningUI("已断开连接");
        }
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            UIManager.Instance.CloseLodingUI();
            UIManager.Instance.TackWarningUI("成功加入房间");
            string myShipName = GameManager.Instance.shipName;
            DiffEnemyAndTeammate(myShipName);
        }
        void SetPlayerAttributes(Photon.Realtime.Player player, string tag, string layerName)
        {
            GameObject playerObject = GameObject.Find(player.NickName);
            if (playerObject != null)
            {
                playerObject.tag = tag;
                playerObject.layer = LayerMask.NameToLayer(layerName);
            }
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("连接成功");
            UIManager.Instance.TackWarningUI("成功连接服务器");
            StartCoroutine(DelayedJoinRoom());
        }
        private IEnumerator DelayedJoinRoom()
        {
            yield return new WaitForSeconds(0.5f);
            RoomOptions options = new RoomOptions
            {
                MaxPlayers = 12,
            };
            PhotonNetwork.JoinOrCreateRoom("ThePirateWarMain", options, TypedLobby.Default);
        }
        public override void OnCreatedRoom()
        {
            UIManager.Instance.CloseLodingUI();
            UIManager.Instance.TackWarningUI("成功创建房间");
        }
        /// <summary>
        /// 获取一个角色的属性
        /// </summary>
        string GetPlayerShipName(Player player)
        {
            if (player.CustomProperties != null && player.CustomProperties.ContainsKey("ShipName"))
            {
                return (string)player.CustomProperties["ShipName"];
            }
            return string.Empty;
        }
        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
            if (changedProps.ContainsKey("ShipName"))
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    string shipName = (string)changedProps["ShipName"];
                    if (!ShipExists(shipName))
                    {
                        GameManager.Instance.CreateShip(shipName);
                    }
                }
            }
        }
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            UIManager.Instance.TackWarningUI("加入房间失败");
            UIManager.Instance.CloseLodingUI();
        }
        bool ShipExists(string shipName)
        {
            return ships.ContainsKey(shipName);
        }
    }
}