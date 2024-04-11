using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyGame.PlayerSystem
{
    public class GameInput : Singleton<GameInput>
    {
        PlayerInputActions playerInputActions;
        public event Action<InputAction.CallbackContext> InteractAction;
        public event Action<Vector2> MovementAction;
        protected override void Awake()
        {
            base.Awake();
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Interact.performed += Interact;
        }
        private void FixedUpdate()
        {
            MovementAction?.Invoke(GetMovementInput());
        }
        private void Interact(InputAction.CallbackContext context)
        {
            InteractAction?.Invoke(context);
        }

        public Vector2 GetMovementInput()
        {
            return playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
        }
    }
}