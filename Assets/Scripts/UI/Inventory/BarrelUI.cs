using System.Collections;
using MyGame.Inventory;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem.InventoryUI
{
    public class BarrelUI : MonoBehaviour
    {
        public SlotUI[] slots;
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Coroutine fadeCoroutine;
        private bool isOpen;
        [SerializeField] Text barralText;
        private void Awake()
        {
            slots = GetComponentsInChildren<SlotUI>();
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.zero;
            canvasGroup.alpha = 0;

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].index = i;
                slots[i].slotType = SlotType.Barrel;
            }
        }
        public void OpenBarrelSlots(Item[] items, BarrelType barrelType)
        {
            if (isOpen)
            {
                UpdateBarrelUI(items);
                return;
            }
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeInAndScale());
            UpdateText(barrelType);
            isOpen = true;
            UpdateBarrelUI(items);
        }
        public void UpdateText(BarrelType type)
        {
            barralText.text = type switch
            {
                BarrelType.Normal => "木桶",
                BarrelType.Board => "木板桶",
                BarrelType.CannonBall => "炮弹筒",
                BarrelType.Fruit => "水果桶",
                _ => "未知木桶"
            };
        }
        public void UpdateBarrelUI(Item[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                slots[i].UpdateSlotUI(items[i]);
            }
        }
        public void CloseBarrelSlots()
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeOutAndScale());
            isOpen = false;
        }
        /// <summary>
        /// 渐显
        /// </summary>
        IEnumerator FadeInAndScale()
        {
            var currentScale = rectTransform.localScale;
            var currentAlpha = canvasGroup.alpha;
            for (int i = 1; i < 51; i++)
            {
                canvasGroup.alpha = Mathf.Lerp(currentAlpha, 1, i * 0.02f);
                rectTransform.localScale = Vector3.Lerp(currentScale, Vector3.one, i * 0.02f);
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
            for (int i = 1; i < 51; i++)
            {
                canvasGroup.alpha = Mathf.Lerp(currentAlpha, 0, i * 0.02f);
                rectTransform.localScale = Vector3.Lerp(currentScale, Vector3.zero, i * 0.02f);
                yield return Setting.waitForFixedUpdate;
            }
        }

    }
}