using UnityEngine;

namespace MyGame.HandheldableSystem.WeaponSystem
{
    public class Weapon_Pistol : Weapon
    {
        protected override void InstantiateAttack(Vector3 attackPos, Vector3 attackDir)
        {
            Debug.Log(attackDir);
            Bullet bullet;
            if (playerWeapon.Is2F)
                bullet = BulletPool.Instance.GetOwnSide2FBullet(attackPos, Quaternion.Euler(attackDir));
            else
                bullet = BulletPool.Instance.GetOwnSideBullet(attackPos, Quaternion.Euler(attackDir));
            bullet.Init(damage, attackDir);
        }
    }
}