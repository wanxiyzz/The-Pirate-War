using System.Collections;
using System.Collections.Generic;
using MyGame.PlayerSystem;
using MyGame.UISystem;
using UnityEngine;
namespace MyGame.ShipSystem.Floor
{
    public class EnterFloor : MonoBehaviour
    {
        private ShipFloor shipFloor;
        private PolygonCollider2D polygonCollider2D;
        private ShipTakeWater shipTakeWater;
        private void Start()
        {
            shipTakeWater = GetComponentInParent<ShipTakeWater>();
            shipFloor = GetComponentInParent<ShipFloor>();
            polygonCollider2D = GetComponent<PolygonCollider2D>();

        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (shipTakeWater.waterValue > 0.8f)
            {
                UIManager.Instance.TackWarningUI("水太深了！下不去了");
                return;
            }
            if (other.GetComponent<PlayerController>() == GameManager.Instance.player)
            {
                if (other.CompareTag("Player") && !shipFloor.isEnterFloor)
                {
                    shipFloor.Enter2F();
                    polygonCollider2D.enabled = false;
                }
            }
        }
    }
}