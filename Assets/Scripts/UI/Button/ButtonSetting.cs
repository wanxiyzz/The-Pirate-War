using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem.Buttons
{
    public class ButtonSetting : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OpenButtonSetting);
        }

        private void OpenButtonSetting()
        {
            UIManager.Instance.settingPanel.SetActive(true);
            UIManager.Instance.startPanel.SetActive(false);
        }
    }
}