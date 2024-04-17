using UnityEngine;
namespace MyGame.HandheldableSystem.WeaponSystem
{
    public class Weapon_AWM : Weapon
    {
        protected override void Aim()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePos - (Vector2)transform.position).normalized;
            transform.right = direction;
            if (!canUsed)
            {
                currentBufferTime -= Time.deltaTime;
                if (currentBufferTime < 0)
                {
                    canUsed = true;
                }
            }
        }

        protected override void InstantiateAttack(Vector3 attackPos, Vector3 attackDir)
        {
            Bullet bullet = BulletPool.Instance.GetOwnSideBullet(attackPos, transform.localRotation);
            bullet.Init(damage, attackDir);
        }
    }
}