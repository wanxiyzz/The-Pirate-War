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
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => enterShipName.gameObject.SetActive(true));
        }
    }
}