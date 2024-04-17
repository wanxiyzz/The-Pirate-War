using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem
{
    public class UIManager : Singleton<UIManager>
    {
        public Text hemlText;
        [SerializeField] GameObject WeaponTableUI;
        void Start()
        {
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                OpenWeaponTableUI(true);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                OpenWeaponTableUI(false);
            }
        }
        public void EnterHelm(float helmRotate)
        {
            hemlText.gameObject.SetActive(true);
            UpdateHeml(helmRotate);
        }
        public void ExitHelm()
        {
            hemlText.gameObject.SetActive(false);
        }
        public void UpdateHeml(float helmRotate)
        {
            hemlText.text = "舵角：" + helmRotate.ToString("f2");
        }
        public void OpenWeaponTableUI(bool isOpen)
        {
            WeaponTableUI.gameObject.SetActive(isOpen);
        }
    }
}