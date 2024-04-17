using UnityEngine;

namespace MyGame.HandheldableSystem.WeaponSystem
{
    public class BulletPool : Singleton<BulletPool>
    {
        [SerializeField] private ObjectPool<Bullet> OwnSideBulletPool;
        [SerializeField] private Transform OwnSideBulletParent;
        [SerializeField] private ObjectPool<Bullet> EnemyBulletPool;
        [SerializeField] private Transform EnemyBulletParent;

        protected override void Awake()
        {
            base.Awake();
            OwnSideBulletPool.Initialize(OwnSideBulletParent);
            EnemyBulletPool.Initialize(EnemyBulletParent);
        }
        public Bullet GetOwnSideBullet(Vector3 position, Quaternion rotation)
        {
            return OwnSideBulletPool.PrepareObject(position, rotation);
        }
        public Bullet GetEnemyBullet(Vector3 position, Quaternion rotation)
        {
            return EnemyBulletPool.PrepareObject(position, rotation);
        }
    }
}