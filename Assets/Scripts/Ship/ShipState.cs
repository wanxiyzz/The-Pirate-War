using UnityEngine;
using System;
namespace MyGame.ShipSystem
{
    [Serializable]
    public class ShipState
    {
        public float waterVolume;
        public Vector2 position;
        public Vector2 shipDir;
        public float shipSailValue;
        public void UpdateState(Vector2 pos, Vector2 shipDir)
        {
            this.position = pos;
            this.shipDir = shipDir;
        }
    }
}