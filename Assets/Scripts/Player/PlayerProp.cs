using System;
using MyGame.HandheldableSystem;
using MyGame.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace MyGame.PlayerSystem
{
    public class PlayerProp : MonoBehaviour
    {
        public Lantern lanternHolder;
        public PlayerWeapon playerWeapon;
        public HandHeld currentHandHeld;
        void Start()
        {
            currentHandHeld = HandHeld.Weapon;
            GameInput.Instance.playerInputActions.Player.Lantern.performed += TakeOutLantern;
        }

        private void TakeOutLantern(InputAction.CallbackContext context)
        {
            if (currentHandHeld == HandHeld.Lantern)
            {
                lanternHolder.PackUpLantern();
                playerWeapon.TakeoutWeapon();
                currentHandHeld = HandHeld.Weapon;

            }
            else
            {
                lanternHolder.TackOutLantern();
                playerWeapon.PutWeapon();
                currentHandHeld = HandHeld.Lantern;
            }
        }

        void Update()
        {

        }

    }
}