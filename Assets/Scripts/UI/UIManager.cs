using System.Collections;
using MyGame.InputSystem;
using MyGame.Inventory;
using MyGame.ShipSystem.Sail;
using MyGame.UISystem.InventoryUI;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
        [SerializeField] HealthBar healthBarUI;
        public Image dragImage;
        public GameObject allStartUI;
        public GameObject startPanel;
        public GameObject introdutcionUI;
        public GameObject settingPanel;
        public GameObject loadingUI;
        public GameObject shipList;
        public GameObject pauseUI;
        protected override void Awake()
        {
            base.Awake();
            // SceneManager.LoadScene("Main", LoadSceneMode.Additive);
            dragImage.enabled = false;
            healthBarUI = GetComponentInChildren<HealthBar>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(PhotonNetwork.GetPing());
            }
        }
        private void OnEnable()
        {
            EventHandler.OpenBarrelUI += OnOpenBarrelUI;
            EventHandler.OpenBagUI += OnOpenOpenBagUI;
            GameInput.Instance.playerInputActions.Player.Esc.performed += OnPauseUI;
        }

        private void OnPauseUI(InputAction.CallbackContext context)
        {
            if (allStartUI.activeSelf)
            {
                return;
            }
            else
            {
                if (pauseUI.activeSelf)
                {
                    pauseUI.SetActive(false);
                }
                else
                {
                    pauseUI.SetActive(true);
                }
            }
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
        public void OpenLodingUI(string text)
        {
            loadingUI.SetActive(true);
            loadingUI.GetComponentInChildren<Text>().text = text;
        }
        public void CloseLodingUI()
        {
            loadingUI.SetActive(false);
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
        public void ChangeHealth(int currenHealth, int maxHealth)
        {
            healthBarUI.ChangeHealth(currenHealth, maxHealth);
        }
        public void EnterGame(string shipName)
        {
            OpenLodingUI("进入游戏中");
            StartCoroutine(EnterGameCoroutine(shipName));
        }

        private IEnumerator EnterGameCoroutine(string shipName)
        {
            yield return new WaitForSeconds(1f);
            GameManager.Instance.EnterGame(shipName);
            CloseLodingUI();
            allStartUI.SetActive(false);
        }
        public void OpenAllStartUI(bool isOpen)
        {
            allStartUI.SetActive(isOpen);
        }
        public void OpenStartPanel(bool isOpen)
        {
            startPanel.SetActive(isOpen);
        }
        public void BackTittle()
        {
            StartCoroutine(BackTittleIE());
        }
        IEnumerator BackTittleIE()
        {
            allStartUI.SetActive(true);
            pauseUI.SetActive(false);
            PhotonNetwork.Disconnect();
            yield return SceneManager.UnloadSceneAsync("main");
            yield return SceneManager.LoadSceneAsync("main");

        }
    }
}