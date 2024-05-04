using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MyGame.UISystem
{
    public class HealthBar : MonoBehaviour
    {
        public Image bloodBar;
        public Image virtualBar;
        private Coroutine healthCorotine;
        public void ChangeHealth(int health, int maxHealth)
        {
            bloodBar.fillAmount = health / (float)maxHealth;
            healthCorotine = StartCoroutine(VirtualBarIE(bloodBar.fillAmount));
        }
        IEnumerator VirtualBarIE(float targetAmount)
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 26; i++)
            {
                var currentFillAmount = virtualBar.fillAmount;
                Debug.Log(virtualBar.fillAmount);
                virtualBar.fillAmount = Mathf.Lerp(currentFillAmount, targetAmount, i * 0.04f);
                yield return Setting.waitForFixedUpdate;
            }
        }
    }
}