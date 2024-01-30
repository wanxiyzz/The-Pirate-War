using UnityEngine;
using MyGame.WeaponSystem;
using UnityEngine.InputSystem;
namespace MyGame.Player
{

    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rigi;
        private const int speed = 5;
        [SerializeField] private Weapon[] allWeapons = new Weapon[5];
        [SerializeField] private Weapon[] weapons = new Weapon[2];
        private Weapon currentWeapon;
        private void Awake()
        {
            rigi = GetComponent<Rigidbody2D>();
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
            rigi.velocity = GameInput.Instance.GetMovementInput() * speed;
        }
        public void ChangeWeapon(int index, WeaponType type)
        {
        }
    }
}