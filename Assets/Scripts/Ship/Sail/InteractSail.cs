using MyGame.InputSystem;
using UnityEngine;
namespace MyGame.ShipSystem.Sail
{
    public class InteractSail : MonoBehaviour, Iinteractable
    {
        private Transform tacksailPos;

        public string Feature => "收/放船帆";

        public bool IsSimple => false;
        private ShipSail shipSail;
        private void Awake()
        {
            shipSail = GetComponentInParent<ShipSail>();
        }
        public void EnterInteract()
        {
            GameInput.Instance.MovementAction += InputInteract;
            GameManager.Instance.player.PlayerEnterInteract(tacksailPos);
        }

        public void EnterWaitInteract()
        {
        }

        public void ExitInteract()
        {
            GameInput.Instance.MovementAction -= InputInteract;
        }

        public void ExitWaitInteract()
        {
        }

        public void InputInteract(Vector2 input)
        {
            Debug.Log(input);
            shipSail.AddSailValue(input.y * Time.deltaTime * shipSail.sailSpeed);
        }
    }
}