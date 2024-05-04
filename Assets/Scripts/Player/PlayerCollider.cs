using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.PlayerSystem
{
    public class PlayerCollider : MonoBehaviour
    {
        private PlayerController playerController;
        void Start()
        {
            playerController = GetComponent<PlayerController>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (playerController.isFireSelf)
            {
                if (other.gameObject.CompareTag("Ship"))
                {
                    Debug.Log("BoardShip");
                    playerController.isFireSelf = false;
                    PlayerFire2Ship(other.gameObject.GetComponent<Rigidbody2D>(), other.transform, other.collider.ClosestPoint(transform.position));
                }
            }
        }
        private void PlayerFire2Ship(Rigidbody2D shiRigi, Transform shipTrans, Vector3 pos)
        {
            Vector3 dir = (shipTrans.position - pos).normalized;
            playerController.BoardShip(shiRigi, pos + dir * 0.5f);
        }

    }
}