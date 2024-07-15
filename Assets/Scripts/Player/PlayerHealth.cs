using System.Collections;
using System.Collections.Generic;
using MyGame.UISystem;
using Photon.Pun;
using UnityEngine;
namespace MyGame.PlayerSystem
{
    public class PlayerHealth : MonoBehaviourPun
    {
        public int playerHealth;
        public bool isDead;
        [SerializeField] private SpriteRenderer playerRenderer;
        private void Awake()
        {
            playerHealth = Setting.maxHealth;
        }
        public bool ChangeHealth(int health)
        {
            playerHealth += health;
            if (health > 0)
            {
                StartCoroutine(ChangeHealthIE(Color.green));
            }
            else
            {
                StartCoroutine(ChangeHealthIE(Color.red));
            }
            if (playerHealth <= 0)
            {
                PlayerDeath();
            }
            if (playerHealth >= Setting.maxHealth)
            {
                playerHealth = Setting.maxHealth;
            }
            if (photonView.IsMine)
                UIManager.Instance.ChangeHealth(playerHealth, Setting.maxHealth);
            return isDead;
        }
        IEnumerator ChangeHealthIE(Color color)
        {
            Color defalutColor = playerRenderer.color;
            playerRenderer.color = color;
            yield return new WaitForSeconds(0.1f);
            playerRenderer.color = defalutColor;
        }
        private void PlayerDeath()
        {
            isDead = true;
            playerHealth = 0;
            PhotonNetwork.Destroy(gameObject);
        }
    }
}