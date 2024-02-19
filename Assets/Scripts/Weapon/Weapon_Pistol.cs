using UnityEngine;

namespace MyGame.WeaponSystem
{
    public class Weapon_Pistol : Weapon
    {
        private void Awake()
        {
            weaponType = WeaponType.Pistol;
            currentBullets = 10;
            maxBullets = 10;
            damage = 20;
            bufferTime = 0.7f;
        }
        protected override void InstantiateAttack(Vector3 attackPos, Vector3 attackDir)
        {
            Bullet bullet = BulletPool.Instance.GetOwnSideBullet(attackPos, transform.localRotation);
            bullet.Init(damage, attackDir);
        }
    }
}