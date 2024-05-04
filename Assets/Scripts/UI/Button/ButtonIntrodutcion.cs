using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem.Buttons
{
    public class ButtonIntrodutcion : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OpenButtonIntrodutcion);
        }

        private void OpenButtonIntrodutcion()
        {
            UIManager.Instance.introdutcionUI.SetActive(true);
            UIManager.Instance.startPanel.SetActive(false);
        }
    }
}