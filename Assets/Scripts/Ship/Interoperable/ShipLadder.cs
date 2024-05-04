using System.Collections;
using System.Collections.Generic;
using MyGame.PlayerSystem;
using UnityEngine;
namespace MyGame.ShipSystem
{
    public class ShipLadder : MonoBehaviour, Iinteractable
    {
        public string Feature => "船梯";
        public bool IsSimple => true;

        public bool IsInteractable => false;

        public bool IsBoard => false;

        [SerializeField] Transform leavePoint;
        [SerializeField] Transform enterPoint;
        [SerializeField] Rigidbody2D shipRigi;
        [SerializeField] ShipController shipController;
        public void EnterInteract(PlayerController playerController)
        {
            if (playerController.playerPos == PlayerPos.Ship1F)
            {
                playerController.LeaveShipPun(leavePoint.position);
            }
            else
            {
                playerController.BoardShipPun(shipController.name, enterPoint.position);
            }
            GameManager.Instance.player.PlayerEnterInteract(null);
        }

        public void EnterWaitInteract()
        {
        }

        public void ExitInteract()
        {
            //null
        }

        public void ExitWaitInteract()
        {
        }

        public void InputInteract(Vector2 input)
        {

        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}