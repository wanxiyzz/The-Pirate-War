using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.ShipSystem
{
    public class ShipController : MonoBehaviour
    {
        //TRAN:船参数
        private ShipState shipState = new ShipState(0, 5);

        private Vector2 shipDir = new Vector2();
        public float shipSpeed;

        private Rigidbody2D rb;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        void Update()
        {

        }

        /// <summary>
        /// 下锚
        /// </summary>
        public void SetAnchor()
        {
            shipState.shipSpeed = 0;
        }
        /// <summary>
        /// 起锚
        /// </summary>
        public void HoistAnchor()
        {
        }

    }
}