using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem
{
    public class UIManager : Singleton<UIManager>
    {
        public HelmUI helmUI;
        [SerializeField] GameObject WeaponTableUI;
        [SerializeField] Text interactTipsText;
        [SerializeField] Text tipsText;

        public void EnterHelm(float helmRotate)
        {
            helmUI.OpenHelmUI(helmRotate);
        }
        public void ExitHelm()
        {
            helmUI.CloseHelmUI();
        }
        public void UpdateHeml(float helmRotate)
        {
            helmUI.UpdateHeml(helmRotate);
        }
        public void OpenWeaponTableUI(bool isOpen)
        {
            WeaponTableUI.gameObject.SetActive(isOpen);
        }
        public void Tips(bool display, string tips)
        {
            tipsText.enabled = display;
            tipsText.text = tips;
        }
        public void InteractTips(Iinteractable interactable)
        {
            if (interactable == null)
            {
                interactTipsText.enabled = false;
            }
            else
            {
                interactTipsText.enabled = true;
                interactTipsText.text = "按下 F " + interactable.Feature;
            }
        }
    }
}