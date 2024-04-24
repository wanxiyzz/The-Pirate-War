using System.Collections;
using System.Collections.Generic;
using MyGame.ShipSystem.Sail;
using UnityEngine;
using UnityEngine.UI;

public class ShipSailUI : MonoBehaviour
{
    [SerializeField] Text sailValueText;
    private Coroutine fadeCoroutine;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public ShipSail currentshipSail;
    private void Awake()
    {
        sailValueText = GetComponentInChildren<Text>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        rectTransform.localScale = Vector3.zero;
    }
    private void Update()
    {
        if (currentshipSail != null)
            UpdateSailValue(currentshipSail.sailValue);
    }
    public void OpenSailUI(ShipSail shipSail)
    {
        currentshipSail = shipSail;
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeInAndScale());
        UpdateSailValue(shipSail.sailValue);
    }
    public void CloseSailUI()
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
    public void UpdateSailValue(float sailValue)
    {
        sailValueText.text = ((int)(sailValue * 100)).ToString();
    }

}
