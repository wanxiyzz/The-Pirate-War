using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.ShipSystem.Cannon
{
    public class CannonballPool : Singleton<CannonballPool>
    {
        [SerializeField] private ObjectPool<Cannonball> cannonballPool;

        public void Initialize()
        {
            // 初始化对象池
            cannonballPool.Initialize(transform);
        }
        public void GetCannonball(Vector2 velocity, Vector2 pos)
        {
            var connonball = cannonballPool.PrepareObject(pos);
            var connonballRigi = connonball.GetComponent<Rigidbody2D>();
            connonballRigi.velocity = velocity;
        }
    }
}