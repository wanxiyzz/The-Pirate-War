using System.Collections;
using MyGame.ShipSystem;
using UnityEngine;
namespace MyGame.UISystem
{

    public class ShipWaterUI : Singleton<ShipWaterUI>
    {
        [SerializeField] RectTransform waterTrans;
        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;
        private Coroutine fadeCoroutine;
        private float water100 = -36f;
        private float water0 = -176f;
        private ShipTakeWater currentShipTakeWater;
        protected override void Awake()
        {
            base.Awake();
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
        }
        private void Update()
        {
            UpdateWaterUI();
        }
        public void OpenWaterUI(ShipTakeWater shipTakeWater)
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            currentShipTakeWater = shipTakeWater;
            fadeCoroutine = StartCoroutine(FadeInAndScale());
            UpdateWaterUI();
        }
        public void CloseWaterUI()
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
        public void UpdateWaterUI()
        {
            if (currentShipTakeWater != null)
                waterTrans.anchoredPosition = new Vector2(0, Mathf.Lerp(water0, water100, currentShipTakeWater.waterValue));
        }
    }
}