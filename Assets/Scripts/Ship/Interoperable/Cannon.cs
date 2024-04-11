using System.Collections;
using System.Collections.Generic;
using MyGame.PlayerSystem;
using UnityEngine;
namespace MyGame.ShipSystem
{
    public class Cannon : MonoBehaviour, Iinteractable
    {
        public void EnterInteract()
        {
            GameInput.Instance.MovementAction += InputInteract;
        }

        public void EnterWaitInteract()
        {
        }

        public void ExitInteract()
        {
        }

        public void ExitrWaitInteract()
        {
        }

        public void InputInteract(Vector2 input)
        {
        }
    }
}
