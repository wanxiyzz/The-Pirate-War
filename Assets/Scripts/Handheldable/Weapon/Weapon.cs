using UnityEngine;
using System;
using MyGame.PlayerSystem;

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
        public override void ItemUsed()
        {
            if (currentBullets > 0 && canUsed)
            {
                currentBullets--;
                currentBufferTime = bufferTime;
                canUsed = false;
                InstantiateAttack(muzzlePos.position, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized);
                UpdateBullet?.Invoke();
            }
        }

        protected abstract void InstantiateAttack(Vector3 attackPos, Vector3 attackDir);
    }
}