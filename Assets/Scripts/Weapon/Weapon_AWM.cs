using UnityEngine;
namespace MyGame.WeaponSystem
{
    public class Weapon_AWM : Weapon
    {
        private void Awake()
        {
            weaponType = WeaponType.AWM;
            currentBullets = 5;
            maxBullets = 5;
            damage = 40;
            bufferTime = 1.5f;
        }
        protected override void InstantiateAttack(Vector3 attackPos, Vector3 attackDir)
        {
            Bullet bullet = BulletPool.Instance.GetOwnSideBullet(attackPos, transform.localRotation);
            bullet.Init(damage, attackDir);
        }
    }
}