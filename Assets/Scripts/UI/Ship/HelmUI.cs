using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelmUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text helmText;
    [SerializeField] RectTransform shipUITrans;
    private Coroutine fadeCoroutine;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        rectTransform.localScale = Vector3.zero;
    }
    public void OpenHelmUI(float helmRotate)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeInAndScale());
        UpdateHeml(helmRotate);
    }
    public void CloseHelmUI()
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
            canvasGroup.alpha = Mathf.Lerp(currentAlpha, 1, i * 0.04f);
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
            canvasGroup.alpha = Mathf.Lerp(currentAlpha, 0, i * 0.04f);
            rectTransform.localScale = Vector3.Lerp(currentScale, Vector3.zero, i * 0.04f);
            yield return Setting.waitForFixedUpdate;
        }
    }
    public void UpdateHeml(float helmRotate)
    {
        helmText.text = helmRotate.ToString("F2");
        shipUITrans.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(-50f, 50f, (helmRotate + 1) / 2));
    }

}
