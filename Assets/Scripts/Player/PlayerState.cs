using UnityEngine;
using System;
namespace MyGame.PlayerSystem
{
    [Serializable]
    public class PlayerState
    {
        public Vector2 position;
        public Vector2 plsyerDir;
        public int currentHealth = 0;
        public int playerAction;
        public bool is2Floor;
        private void Awake()
        {
            currentHealth = Setting.maxHealth;
        }
        public void ChangeHealth(int amount)
        {
            currentHealth += amount;
            if (currentHealth > Setting.maxHealth)
            {
                currentHealth = Setting.maxHealth;
            }
            else if (currentHealth <= 0)
            {
                Death();
            }
        }
        public void Death()
        {
            currentHealth = 0;
        }
    }
}