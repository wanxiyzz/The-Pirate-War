using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] Image progressBar;
        private Text progressText;
        private void Awake()
        {
            progressText = GetComponentInChildren<Text>();
        }
        public void SetText(string text)
        {
            progressText.text = text;
        }
        public void SetProgress(float progress)
        {
            progressBar.fillAmount = progress;
        }
    }
}