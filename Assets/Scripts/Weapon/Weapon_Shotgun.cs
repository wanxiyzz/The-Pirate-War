using UnityEngine;

namespace MyGame.WeaponSystem
{
    public class Weapon_Shotgun : Weapon
    {
        private float bulletAngle = 10;
        private const int bulletCount = 6;
        private void Awake()
        {
            weaponType = WeaponType.AWM;
            currentBullets = 8;
            maxBullets = 8;
            damage = 9;
            bufferTime = 1f;
        }
        protected override void InstantiateAttack(Vector3 attackPos, Vector3 attackDir)
        {
            float halfAngle = bulletAngle / 2;
            for (int i = 0; i < bulletCount; i++)
            {
                Bullet bullet = BulletPool.Instance.GetOwnSideBullet(attackPos, transform.localRotation);
                bullet.Init(damage, Quaternion.AngleAxis(bulletAngle * (i - 3) + halfAngle, Vector3.forward) * direction);
            }
        }
    }
}