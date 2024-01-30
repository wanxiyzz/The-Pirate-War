using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyGame.Player
{
    public class GameInput : Singleton<GameInput>
    {
        PlayerInputActions playerInputActions;
        public event Action<InputAction.CallbackContext> InteractAction;
        protected override void Awake()
        {
            base.Awake();
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Interact.performed += Interact;
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