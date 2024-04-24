using System;
using System.Collections;
using System.Collections.Generic;
using MyGame.HandheldableSystem.WeaponSystem;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem
{
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] Image weaponImage;
        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;
        private Coroutine fadeCoroutine;
        [SerializeField] Text bulletText;
        private Weapon currentWeapon;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public void UpdateWeaponSlot(Weapon weapon)
        {
            currentWeapon = weapon;
            weaponImage.sprite = UIManager.Instance.weaponSprites[(int)weapon.weaponType];
            weapon.UpdateBullet += OnUpdateBullet;
            UpdateWeaponSlot(weapon.maxBullets, weapon.currentBullets);
        }

        private void OnUpdateBullet()
        {
            UpdateWeaponSlot(currentWeapon.maxBullets, currentWeapon.currentBullets);
        }

        public void UpdateWeaponSlot(int maxBullet, int currentBullet)
        {
            bulletText.text = currentBullet.ToString() + "/" + maxBullet.ToString();
        }
        public void OpenWeaponSlot()
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeInAndScale());
        }
        public void CloseOpenSlot()
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeOutAndScale());
        }

        /// <summary>
        /// 渐显
        /// </summary>
        IEnumerator FadeInAndScale()
        {
            var currentScale = rectTransform.localScale;
            var currentAlpha = canvasGroup.alpha;
            for (int i = 1; i < 26; i++)
            {
                canvasGroup.alpha = Mathf.Lerp(currentAlpha, 0.8f, i * 0.04f);
                rectTransform.localScale = Vector3.Lerp(currentScale, Vector3.one, i * 0.04f);
                yield return Setting.waitForFixedUpdate;
            }
        }
        /// <summary>
        /// 渐隐
        /// </summary>
        IEnumerator FadeOutAndScale()
        {
            var currentScale = rectTransform.localScale;
            var currentAlpha = canvasGroup.alpha;
            for (int i = 1; i < 26; i++)
            {
                canvasGroup.alpha = Mathf.Lerp(currentAlpha, 0.5f, i * 0.04f);
                rectTransform.localScale = Vector3.Lerp(currentScale, new Vector3(0.8f, 0.8f, 0.5f), i * 0.04f);
                yield return Setting.waitForFixedUpdate;
            }
        }
    }
}