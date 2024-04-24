using System.Collections;
using System.Collections.Generic;
using MyGame.ShipSystem.Hole;
using UnityEngine;
namespace MyGame.ShipSystem
{
    public class ShipTakeWater : MonoBehaviour
    {
        public float waterValue;
        [SerializeField] ShipHoleManager shipHoleManager;
        void Update()
        {
            ShipFlooded();
        }
        private void ShipFlooded()
        {
            waterValue += shipHoleManager.ShipFlooded() * Time.deltaTime * 0.01f;
            waterValue = Mathf.Clamp(waterValue, 0f, 1f);
        }
    }
}