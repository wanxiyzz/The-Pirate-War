using UnityEngine;

namespace MyGame.HandheldableSystem.WeaponSystem
{
    public class Weapon_Pistol : Weapon
    {
        protected override void InstantiateAttack(Vector3 attackPos, Vector3 attackDir)
        {
            Bullet bullet = BulletPool.Instance.GetOwnSideBullet(attackPos, transform.localRotation);
            bullet.Init(damage, attackDir);
        }
    }
}