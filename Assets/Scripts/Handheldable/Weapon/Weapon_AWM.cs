using Photon.Pun;
using UnityEngine;
namespace MyGame.HandheldableSystem.WeaponSystem
{
    public class Weapon_AWM : Weapon
    {
        protected override void Aim()
        {
            if (!canUsed)
            {
                currentBufferTime -= Time.deltaTime;
                if (currentBufferTime < 0)
                {
                    canUsed = true;
                }
            }
            CameraManager.Instance.CameraOffsetWithMouse();
        }
        public override void ItemUsedPun()
        {
            photonView.RPC("ItemUsed", RpcTarget.All);
        }
        [PunRPC]
        public override void ItemUsed()
        {
            base.ItemUsed();
        }
        public override void InstantiateAttack(Vector3 attackPos, Vector3 attackDir)
        {
            // 发射子弹
            int damage = 100;
            Bullet bullet;
            if (playerWeapon.Is2F)
                bullet = BulletPool.Instance.GetOwnSide2FBullet(attackPos, Quaternion.Euler(attackDir));
            else
                bullet = BulletPool.Instance.GetOwnSideBullet(attackPos, Quaternion.Euler(attackDir));
            bullet.Init(damage, attackDir);
        }
    }
}