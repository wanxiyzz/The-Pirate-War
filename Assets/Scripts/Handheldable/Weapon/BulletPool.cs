using UnityEngine;

namespace MyGame.HandheldableSystem.WeaponSystem
{
    public class BulletPool : Singleton<BulletPool>
    {
        [SerializeField] private ObjectPool<Bullet> OwnSideBulletPool;
        [SerializeField] private Transform OwnSideBulletParent;
        [SerializeField] private ObjectPool<Bullet> EnemyBulletPool;
        [SerializeField] private Transform EnemyBulletParent;
        [SerializeField] private ObjectPool<Bullet> OwnSide2FBulletPool;
        [SerializeField] private Transform OwnSide2FBulletParent;
        [SerializeField] private ObjectPool<Bullet> Enemy2FBulletPool;
        [SerializeField] private Transform Enemy2FBulletParent;

        protected override void Awake()
        {
            base.Awake();
            OwnSideBulletPool.Initialize(OwnSideBulletParent);
            EnemyBulletPool.Initialize(EnemyBulletParent);
            OwnSide2FBulletPool.Initialize(OwnSide2FBulletParent);
            Enemy2FBulletPool.Initialize(Enemy2FBulletParent);
        }
        public Bullet GetOwnSideBullet(Vector3 position, Quaternion rotation)
        {
            return OwnSideBulletPool.PrepareObject(position, rotation);
        }
        public Bullet GetEnemyBullet(Vector3 position, Quaternion rotation)
        {
            return EnemyBulletPool.PrepareObject(position, rotation);
        }
        public Bullet GetOwnSide2FBullet(Vector3 position, Quaternion rotation)
        {
            return OwnSide2FBulletPool.PrepareObject(position, rotation);
        }
        public Bullet GetEnemy2FBullet(Vector3 position, Quaternion rotation)
        {
            return Enemy2FBulletPool.PrepareObject(position, rotation);
        }
    }
}