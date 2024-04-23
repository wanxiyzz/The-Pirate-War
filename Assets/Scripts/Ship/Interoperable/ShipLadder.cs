using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.ShipSystem
{
    public class ShipLadder : MonoBehaviour, Iinteractable
    {
        public string Feature => "船梯";

        public void EnterInteract()
        {
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