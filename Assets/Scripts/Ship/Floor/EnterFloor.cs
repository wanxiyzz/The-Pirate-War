using System.Collections;
using System.Collections.Generic;
using MyGame.PlayerSystem;
using UnityEngine;
namespace MyGame.ShipSystem.Floor
{
    public class EnterFloor : MonoBehaviour
    {
        private ShipFloor shipFloor;
        private PolygonCollider2D polygonCollider2D;
        private void Start()
        {
            shipFloor = GetComponentInParent<ShipFloor>();
            polygonCollider2D = GetComponent<PolygonCollider2D>();

        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !shipFloor.isEnterFloor)
            {
                shipFloor.Enter2F();
                polygonCollider2D.enabled = false;
            }
        }
    }
}