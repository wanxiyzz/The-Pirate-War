using System.Collections;
using System.Collections.Generic;
using MyGame.ShipSystem;
using UnityEngine;
namespace MyGame.PlayerSystem
{
    public class PlayerCollider : MonoBehaviour
    {
        private PlayerController playerController;
        void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (playerController.isFireSelf)
            {
                if (other.gameObject.CompareTag("Ship"))
                {
                    playerController.isFireSelf = false;
                    PlayerFire2Ship(other.gameObject.GetComponent<ShipController>().shipName, other.transform, other.collider.ClosestPoint(transform.position));
                }
            }
        }
        private void PlayerFire2Ship(string shipName, Transform shipTrans, Vector3 pos)
        {
            Vector3 dir = (shipTrans.position - pos).normalized;
            playerController.BoardShipPun(shipName, pos + dir * 0.5f);
        }

    }
}