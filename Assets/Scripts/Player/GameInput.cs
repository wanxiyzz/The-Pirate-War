using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyGame.InputSystem
{
    public class GameInput : Singleton<GameInput>
    {
        public PlayerInputActions playerInputActions;
        public event Action InteractAction;
        public event Action<Vector2> MovementAction;
        public event Action ChangeLastWeaponAction;
        public event Action AttackAction;
        protected override void Awake()
        {
            base.Awake();
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Interact.performed += Interact;
            playerInputActions.Player.ChangeLastWeapon.performed += ChangeLastWeapon;
            playerInputActions.Player.Attack.performed += Attack;
        }

        private void ChangeLastWeapon(InputAction.CallbackContext context)
        {
            ChangeLastWeaponAction?.Invoke();
        }

        private void Update()
        {
            MovementAction?.Invoke(GetMovementInput());
        }
        private void Interact(InputAction.CallbackContext context)
        {
            InteractAction?.Invoke();
        }
        private void Attack(InputAction.CallbackContext context)
        {
            AttackAction?.Invoke();
        }
        public Vector2 GetMovementInput()
        {
            return playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
        }
    }
}