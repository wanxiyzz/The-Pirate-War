using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem
{
    public class EnterShipName : MonoBehaviour
    {
        public InputField inputField;
        private void Awake()
        {
            inputField.text = "ship" + Random.Range(1000, 9999);
        }
    }
}