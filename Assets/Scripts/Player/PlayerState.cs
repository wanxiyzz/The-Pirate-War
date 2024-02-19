using UnityEngine;
namespace MyGame.PlayerSystem
{
    public class PlayerState : MonoBehaviour
    {
        private int maxHealth = 100;
        private int currentHealth = 0;
        private bool isDeath;
        public int speed = 5;
        private void Awake()
        {
            currentHealth = maxHealth;
            isDeath = false;
        }
        public void ChangeHealth(int amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDeath = true;
            }
        }
        public void ChangeSpeed(int amount)
        {
            speed = amount;
        }

    }
}