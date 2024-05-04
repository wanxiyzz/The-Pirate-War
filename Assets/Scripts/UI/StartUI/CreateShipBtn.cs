using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem
{
    public class CreateShipBtn : MonoBehaviour
    {
        public InputField inputField;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnAddShipNameButtonClicked);
        }
        void OnAddShipNameButtonClicked()
        {
            string shipName = inputField.text;

            AddShipNameToPlayer(shipName);
        }
        void AddShipNameToPlayer(string shipName)
        {
            if (!string.IsNullOrEmpty(shipName))
            {
                ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
                customProperties["ShipName"] = shipName;
                PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
            }
            UIManager.Instance.EnterGame(shipName);
        }
    }
}