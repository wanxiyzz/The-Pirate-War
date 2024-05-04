using MyGame.PlayerSystem;
using Photon.Pun;
using UnityEngine;
namespace MyGame.HandheldableSystem.WeaponSystem
{
    public class Weapon_Knife : Weapon
    {
        private Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        public override void ItemUsedPun()
        {
            photonView.RPC("ItemUsed", RpcTarget.All);
        }
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
        }
        [PunRPC]
        public override void ItemUsed()
        {
            Debug.Log("Weapon Used");
            Debug.Log(canUsed);
            if (canUsed)
            {
                Vector3 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                dir.z = 0;
                InstantiateAttack(muzzlePos.position, dir);
            }
        }
        public override void InstantiateAttack(Vector3 attackPos, Vector3 attackDir)
        {
            Debug.Log("Attack");
            animator.Play("Attack");
            canUsed = false;
            currentBufferTime = bufferTime;
        }
        public string attackTag;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(attackTag))
            {
                if (other.TryGetComponent<PlayerController>(out PlayerController player))
                {
                    if (playerWeapon.Is2F && player.playerPos == PlayerPos.Ship2F)
                        player.ChangeHealth(-damage);
                    else if (!playerWeapon.Is2F && player.playerPos == PlayerPos.Ship1F)
                        player.ChangeHealth(-damage);
                }
            }
        }
    }
}