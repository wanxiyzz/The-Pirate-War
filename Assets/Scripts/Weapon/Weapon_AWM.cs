using UnityEngine;
namespace MyGame.WeaponSystem
{
    public class Weapon_AWM : Weapon
    {
        protected override void InstantiateAttack(Vector3 attackPos, Vector3 attackDir)
        {
            Bullet bullet = BulletPool.Instance.GetOwnSideBullet(attackPos, transform.localRotation);
            bullet.Init(damage, attackDir);
        }
    }
}