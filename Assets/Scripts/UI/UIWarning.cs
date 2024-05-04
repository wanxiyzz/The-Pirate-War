using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem
{
    public class UIWarning : MonoBehaviour
    {
        public RectTransform rectTransform;
        public float growDuration = 1f;
        public float stayDuration = 2.0f;
        public Vector2 targetPosition = new Vector2(-100, 0);
        public Vector2 startPosition;
        public Text warningText;
        public void ShowWarning(string content)
        {
            rectTransform.localScale = Vector2.zero;
            rectTransform.anchoredPosition = startPosition;
            warningText.text = content;
            gameObject.SetActive(true);
            StartCoroutine(ShowWarningIE());
        }
        IEnumerator ShowWarningIE()
        {
            float elapsedTime = 0;
            Vector2 initialSize = rectTransform.localScale;
            Vector2 initialPosition = rectTransform.anchoredPosition;

            while (elapsedTime < growDuration)
            {
                float t = elapsedTime / growDuration;
                rectTransform.localScale = Vector2.Lerp(initialSize, Vector2.one, t);
                rectTransform.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.localScale = Vector2.one;
            rectTransform.anchoredPosition = targetPosition;

            yield return new WaitForSeconds(stayDuration);


            Destroy(gameObject);
        }

    }
}