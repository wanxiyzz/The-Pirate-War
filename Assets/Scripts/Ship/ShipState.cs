using UnityEngine;
using System;
namespace MyGame.ShipSystem
{
    [Serializable]
    public class ShipState
    {
        public float torque;
        public float shipSpeed;
        public float waterVolume;
        private int maxTorque = 25;//最大扭矩
        public ShipState(float torque, float shipSpeed)
        {
            this.torque = torque;
            this.shipSpeed = shipSpeed;
        }
        /// <summary>
        /// 转向
        /// </summary>
        public void Swerve(Vector2 shipDir)
        {
            if (torque > 0)
            {
            }
            else
            {

            }
        }
    }
}