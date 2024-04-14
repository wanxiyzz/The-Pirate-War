using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.ShipSystem
{
    public class ExitFloor : MonoBehaviour
    {
        private ShipFloor shipFloor;
        private void Start()
        {
            shipFloor = GetComponentInParent<ShipFloor>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && shipFloor.isEnterFloor)
            {
                shipFloor.Exit2F();
            }
        }
    }
}