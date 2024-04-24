using MyGame.HandheldableSystem;
using MyGame.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyGame.PlayerSystem
{
    public class PlayerProp : MonoBehaviour
    {
        public Lantern lanternHolder;
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
                currentHandHeld = HandHeld.Weapon;
                TackOutWeapon();
            }
            else
            {
                lanternHolder.TackOutLantern();
                currentHandHeld = HandHeld.Lantern;
                PickUpWeapon();
            }
        }
        public void PickUpWeapon()
        {
            PlayerWeapon.Instance.haveProp = true;
            PlayerWeapon.Instance.PutWeapon();
        }
        public void TackOutWeapon()
        {
            PlayerWeapon.Instance.haveProp = false;
            PlayerWeapon.Instance.TakeoutWeapon();
        }

    }
}