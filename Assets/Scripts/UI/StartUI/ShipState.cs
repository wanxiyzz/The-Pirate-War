using System;
using System.Collections;
using System.Collections.Generic;
using MyGame.UISystem;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem
{
    public class ShipState : MonoBehaviour
    {
        public Text shipNameText;
        public Text shipPeopleNumText;
        public string shipName;
        public int peopleNum;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            GameManager.Instance.shipName = shipName;
            if (peopleNum >= 3)
            {
                UIManager.Instance.TackWarningUI("船员已满");
            }
            else
            {
                UIManager.Instance.EnterGame(shipName);
            }
        }

        public void Init(string shipName, int peopleNum)
        {
            this.shipName = shipName;
            this.peopleNum = peopleNum;
            shipNameText.text = shipName;
            shipPeopleNumText.text = "人数: " + peopleNum.ToString() + "/3";
        }

    }
}