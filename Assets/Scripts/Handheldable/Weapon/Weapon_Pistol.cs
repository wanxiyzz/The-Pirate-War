using Photon.Pun;
using UnityEngine;

namespace MyGame.HandheldableSystem.WeaponSystem
{
    public class Weapon_Pistol : Weapon
    {
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
            Debug.Log(attackDir);
            Bullet bullet = InstantiateBullet(attackPos, attackDir);
            bullet.Init(damage, attackDir);
        }
    }
}