using UnityEngine;
namespace MyGame.WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        protected WeaponType weaponType = WeaponType.Knife;
        protected int maxBullets = 0;
        public int currentBullets = 0;
        public int damage = 0;
        public float bufferTime = 0;
        public float currentBufferTime = 0;
        protected bool canAttack;
        protected void Update()
        {
            if (!canAttack)
            {
                currentBufferTime -= Time.deltaTime;
                if (currentBufferTime < 0)
                {
                    canAttack = true;
                }
            }
        }
        public void Init()
        {
            currentBullets = maxBullets;
            currentBufferTime = 0;
            canAttack = true;
        }
        public virtual void WeaponAttack(Vector3 pos, Vector3 Dir)
        {
            if (currentBullets > 0 || canAttack)
            {
                currentBullets--;
                currentBufferTime = bufferTime;
                canAttack = false;
                InstantiateAttack();
            }
        }
        protected virtual void InstantiateAttack()
        {

        }
    }
}