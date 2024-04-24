using System.Collections;
using System.Collections.Generic;
using MyGame.ShipSystem.Sail;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem
{
    public class UIManager : Singleton<UIManager>
    {
        public ShipSailUI shipSailUI;
        public HelmUI helmUI;
        [SerializeField] GameObject WeaponTableUI;
        [SerializeField] Text interactTipsText;
        [SerializeField] Text tipsText;
        public Sprite[] weaponSprites;

        #region Helm
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
        #endregion

        #region sail
        public void OpenSailUI(ShipSail shipSail)
        {
            Debug.Log(shipSail);
            shipSailUI.OpenSailUI(shipSail);
        }
        public void CloseSailUI()
        {
            shipSailUI.CloseSailUI();
        }
        public void UpdateSailValue(float sailValue)
        {
            shipSailUI.UpdateSailValue(sailValue);
        }
        #endregion
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