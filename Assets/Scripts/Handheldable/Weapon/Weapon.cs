using UnityEngine;
using System;
namespace MyGame.HandheldableSystem.WeaponSystem
{
    public abstract class Weapon : Handheldable
    {
        [SerializeField] public WeaponType weaponType = WeaponType.Knife;
        [SerializeField] public int maxBullets = 0;
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
        }

        public void CallUpdateBullet()
        {
            UpdateBullet?.Invoke();
        }
        protected override void Aim()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePos - (Vector2)transform.position).normalized;
            transform.right = direction;
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
                InstantiateAttack(muzzlePos.position, direction);
                UpdateBullet?.Invoke();
            }
        }

        protected abstract void InstantiateAttack(Vector3 attackPos, Vector3 attackDir);
    }
}