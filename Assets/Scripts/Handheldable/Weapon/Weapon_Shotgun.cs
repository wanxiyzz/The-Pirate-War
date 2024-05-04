using UnityEngine;

namespace MyGame.HandheldableSystem.WeaponSystem
{
    public class Weapon_Shotgun : Weapon
    {
        [SerializeField] private float bulletAngle = 10;
        [SerializeField] private const int bulletCount = 6;
        protected override void InstantiateAttack(Vector3 attackPos, Vector3 attackDir)
        {
            float halfAngle = bulletAngle / 2;
            for (int i = 0; i < bulletCount; i++)
            {
                Bullet bullet;
                if (playerWeapon.Is2F)
                    bullet = BulletPool.Instance.GetOwnSide2FBullet(attackPos, Quaternion.Euler(attackDir));
                else
                    bullet = BulletPool.Instance.GetOwnSideBullet(attackPos, Quaternion.Euler(attackDir));
                bullet.Init(damage, Quaternion.AngleAxis(bulletAngle * (i - 3) + halfAngle, Vector3.forward) * attackDir);
            }
        }
    }
}