using UnityEngine;
using MyGame.WeaponSystem;
using UnityEngine.InputSystem;
namespace MyGame.PlayerSystem
{

    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rigi;
        private PlayerState playerState;
        private Weapon currentWeapon;
        private void Awake()
        {
            rigi = GetComponent<Rigidbody2D>();
            playerState = GetComponent<PlayerState>();

        }
        private void Start()
        {
            GameInput.Instance.InteractAction += Interact;
        }

        private void Interact(InputAction.CallbackContext context)
        {
            Debug.Log("Interact");
        }

        // Update is called once per frame
        void Update()
        {
            rigi.velocity = GameInput.Instance.GetMovementInput() * playerState.speed;
        }
        public void ChangeWeapon(int index, WeaponType type)
        {
        }
    }
}