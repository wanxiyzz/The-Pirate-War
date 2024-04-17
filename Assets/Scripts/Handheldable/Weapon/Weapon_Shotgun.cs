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
                Bullet bullet = BulletPool.Instance.GetOwnSideBullet(attackPos, transform.localRotation);
                bullet.Init(damage, Quaternion.AngleAxis(bulletAngle * (i - 3) + halfAngle, Vector3.forward) * direction);
            }
        }
    }
}