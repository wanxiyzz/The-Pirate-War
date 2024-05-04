using UnityEngine;
using System;
using MyGame.PlayerSystem;
using Photon.Pun;

namespace MyGame.HandheldableSystem.WeaponSystem
{
    public abstract class Weapon : Handheldable
    {
        [SerializeField] public WeaponType weaponType = WeaponType.Knife;
        [SerializeField] public int maxBullets = 0;
        [SerializeField] protected PlayerWeapon playerWeapon;
        public int currentBullets = 0;
        public int damage = 0;
        public Transform muzzlePos;

        public event Action UpdateBullet;
        //鼠标输入

        // private Animator animator;
        protected void Awake()
        {
            // animator = GetComponent<Animator>();
            muzzlePos = transform.Find("Muzzle");
            playerWeapon = GetComponentInParent<PlayerWeapon>();
        }

        public void CallUpdateBullet()
        {
            UpdateBullet?.Invoke();
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

        public void Init()
        {
            currentBullets = maxBullets;
            currentBufferTime = 0;
            canUsed = true;
        }
        public override void ItemUsedPun()
        {
            photonView.RPC("ItemUsed", RpcTarget.All);
        }
        [PunRPC]
        public override void ItemUsed()
        {
            Debug.Log("Weapon Used");
            if (currentBullets > 0 && canUsed)
            {
                currentBullets--;
                currentBufferTime = bufferTime;
                canUsed = false;
                Vector3 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                dir.z = 0;
                InstantiateAttack(muzzlePos.position, dir);
                UpdateBullet?.Invoke();
            }
        }
        public abstract void InstantiateAttack(Vector3 attackPos, Vector3 attackDir);
        protected Bullet InstantiateBullet(Vector3 attackPos, Vector3 attackDir)
        {
            Bullet bullet;
            if (playerWeapon.IsEnemy)
            {
                if (playerWeapon.Is2F)
                {
                    float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0, 0, angle);
                    bullet = BulletPool.Instance.GetEnemy2FBullet(attackPos, rotation);
                }
                else
                {
                    float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0, 0, angle);
                    bullet = BulletPool.Instance.GetEnemyBullet(attackPos, rotation);
                }
            }
            else
            {
                if (playerWeapon.Is2F)
                {
                    float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0, 0, angle);
                    bullet = BulletPool.Instance.GetOwnSide2FBullet(attackPos, rotation);
                }
                else
                {
                    float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0, 0, angle);
                    bullet = BulletPool.Instance.GetOwnSideBullet(attackPos, rotation);
                }
            }
            return bullet;
        }
    }
}