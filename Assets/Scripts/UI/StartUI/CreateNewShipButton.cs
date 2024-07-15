using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem
{
    public class CreateNewShipButton : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject enterShipName;
        public ShipList shipList;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(CreatNewShip);
        }
        public void CreatNewShip()
        {
            if (shipList.shipNum >= 4)
                UIManager.Instance.TackWarningUI("房间内最多存在4条船");
            else
                enterShipName.gameObject.SetActive(true);
        }
    }
}