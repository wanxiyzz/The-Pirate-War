using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem.Buttons
{
    public class CloseSettingPanel : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(CloseSetting);
        }

        private void CloseSetting()
        {
            UIManager.Instance.settingPanel.SetActive(false);
            UIManager.Instance.startPanel.SetActive(true);
        }

    }
}