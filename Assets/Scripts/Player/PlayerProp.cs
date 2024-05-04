using MyGame.HandheldableSystem;
using MyGame.InputSystem;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyGame.PlayerSystem
{
    public class PlayerProp : MonoBehaviourPun
    {
        public Handheldable[] handheldables;
        public HandHeld currentHandHeld;
        public bool tackProp;
        [SerializeField] PlayerWeapon playerWeapon;
        void Start()
        {
            currentHandHeld = HandHeld.Weapon;
            GameInput.Instance.playerInputActions.Player.Lantern.performed += TakeOutLantern;
            GameInput.Instance.playerInputActions.Player.Spyglass.performed += TakeOutSpyglass;
            GameInput.Instance.playerInputActions.Player.Bucket.performed += TakeOutBucket;
            EventHandler.PlayerInetractive += OnPlayerInetractive;
            EventHandler.PickUpAllItem += OnPlayerInetractive;
        }
        private void OnPlayerInetractive(bool obj)
        {
            if (obj)
            {
                if (currentHandHeld != HandHeld.Weapon)
                    PickUpProp();
            }
            else
            {
                if (currentHandHeld != HandHeld.Weapon)
                    TakeOutProp((int)currentHandHeld);
            }

        }
        private void TakeOutSpyglass(InputAction.CallbackContext context)
        {
            TackOutPorp(2);
        }
        private void TakeOutBucket(InputAction.CallbackContext context)
        {
            TackOutPorp(1);
        }
        private void TakeOutLantern(InputAction.CallbackContext context)
        {
            TackOutPorp(0);
        }
        [PunRPC]
        private void TackOutPorp(int index)
        {
            if (currentHandHeld == (HandHeld)index)
            {
                Debug.Log("收起道具");
                PickUpProp();
                currentHandHeld = HandHeld.Weapon;
                TackOutWeapon();
            }
            else
            {
                Debug.Log("拿出道具");
                if (currentHandHeld == HandHeld.Weapon)
                {
                    currentHandHeld = (HandHeld)index;
                    PickUpWeapon();
                    Debug.Log("拿出" + (HandHeld)index);
                    Debug.Log("拿出" + index);
                    TakeOutProp(index);
                }
                else
                {
                    PickUpProp();
                    currentHandHeld = (HandHeld)index;
                    TakeOutProp(index);
                    Debug.Log("拿出" + (HandHeld)index);
                    Debug.Log("拿出" + index);
                }
            }
        }
        [PunRPC]
        private void PickUpProp()
        {
            handheldables[(int)currentHandHeld].gameObject.SetActive(false);
        }
        [PunRPC]
        private void TakeOutProp(int index)
        {
            Debug.Log(index);
            handheldables[index].gameObject.SetActive(true);
        }

        public void PickUpWeapon()
        {
            playerWeapon.haveProp = true;
            playerWeapon.PutWeaponPun();
        }
        public void TackOutWeapon()
        {
            playerWeapon.haveProp = false;
            playerWeapon.TakeoutWeaponPun();
        }

    }
}