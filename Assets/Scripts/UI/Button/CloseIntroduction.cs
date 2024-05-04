using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem.Buttons
{
    public class CloseIntroduction : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(CloseIntroductionPanel);
        }

        private void CloseIntroductionPanel()
        {
            UIManager.Instance.introdutcionUI.SetActive(false);
            UIManager.Instance.startPanel.SetActive(true);
        }

    }
}