using System.Collections;
using System.Collections.Generic;
using MyGame.Inventory;
using MyGame.ShipSystem.Sail;
using MyGame.UISystem.InventoryUI;
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
        [SerializeField] ProgressBar progressBar;
        [SerializeField] GameObject waringUI;
        [SerializeField] BagUI bagUI;
        [SerializeField] BarrelUI barrelUI;
        public Image dragImage;
        protected override void Awake()
        {
            base.Awake();
            dragImage.enabled = false;
        }
        private void OnEnable()
        {
            EventHandler.OpenBarrelUI += OnOpenBarrelUI;
            EventHandler.OpenBagUI += OnOpenOpenBagUI;
        }

        private void OnDisable()
        {
            EventHandler.OpenBarrelUI -= OnOpenBarrelUI;
            EventHandler.OpenBagUI -= OnOpenOpenBagUI;
        }
        private void OnOpenOpenBagUI(Item[] items, bool value)
        {
            if (value)
            {
                bagUI.OpenBagSlots(items);
            }
            else
            {
                bagUI.CloseBagUI();
            }
        }

        private void OnOpenBarrelUI(Item[] items, bool value, BarrelType barrelType)
        {
            if (value)
            {
                barrelUI.OpenBarrelSlots(items, barrelType);
            }
            else
            {
                barrelUI.CloseBarrelSlots();
            }
        }
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

        #region progressBar
        public void OpenProgressBar(string text)
        {
            progressBar.gameObject.SetActive(true);
            progressBar.SetText(text);
        }
        public void UpdateProgressBar(float progress)
        {
            progressBar.SetProgress(progress);
        }
        public void UpdateProgressBar(float progress, string text)
        {
            progressBar.SetProgress(progress);
            progressBar.SetText(text);
        }
        public void CloseProgressBar()
        {
            progressBar.gameObject.SetActive(false);
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
        public void TackWarningUI(string content)
        {
            Instantiate(waringUI, transform).GetComponent<UIWarning>().ShowWarning(content);
        }
    }
}