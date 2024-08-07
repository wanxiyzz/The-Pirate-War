using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.ShipSystem.Cannon
{
    public class CannonballPool : Singleton<CannonballPool>
    {
        [SerializeField] private ObjectPool<Cannonball> cannonballPool;

        protected override void Awake()
        {
            base.Awake();
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