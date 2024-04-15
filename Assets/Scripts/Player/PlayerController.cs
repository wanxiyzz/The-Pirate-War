using MyGame.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
namespace MyGame.PlayerSystem
{

    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rigi;
        private PlayerState playerState;
        [SerializeField] private bool isMoveing;
        private int speed = 5;
        //Ship
        [SerializeField] private Rigidbody2D shipRigi;
        private Vector2 shipPos;
        public bool isBoardShip; //是否在船上
        [SerializeField] private bool isInteract;
        [SerializeField] Transform interactTrans;
        private void Start()
        {
            rigi = GetComponent<Rigidbody2D>();
            playerState = GetComponent<PlayerState>();
            EventHandler.PlayerInetractive += Interact;
            BoardShip(shipRigi);
        }
        private void Interact(bool isInteract)
        {
            this.isInteract = isInteract;
        }
        void Update()
        {
            PlayerInputAction();
        }
        private void LateUpdate()
        {
            if (isBoardShip)
            {
                if (isMoveing || isInteract)
                {
                    shipPos = transform.localPosition;
                }
                else
                {
                    transform.localPosition = shipPos;
                }
            }
            AdjustPlayerInteractPos();
        }
        private void FixedUpdate()
        {
        }
        public void PlayerEnterInteract(Transform interactTrans)
        {
            this.interactTrans = interactTrans;
        }
        private void AdjustPlayerInteractPos()
        {
            if (interactTrans != null)
            {
                if (isBoardShip && isInteract)
                {
                    transform.position = interactTrans.position;
                    transform.rotation = interactTrans.rotation;
                }

            }
        }
        public void PlayerInputAction()
        {
            if (isInteract)
            {
                rigi.velocity = isBoardShip ? shipRigi.velocity : Vector2.zero;
                // if (isBoardShip)
                // {
                //     rigi.velocity = shipRigi.velocity;
                // }
                // else
                // {
                //     rigi.velocity = Vector2.zero;
                // }
                return;
            }
            Vector2 playerVelocity = GameInput.Instance.GetMovementInput() * speed;
            isMoveing = playerVelocity != Vector2.zero;
            if (isBoardShip)
            {
                playerVelocity += shipRigi.velocity;
            }
            rigi.velocity = playerVelocity;
        }
        public void BoardShip(Rigidbody2D shipRigi)
        {
            isBoardShip = true;
            this.shipRigi = shipRigi;
            transform.parent = shipRigi.transform;
        }
        public void LeaveShip()
        {
            isBoardShip = false;
            shipRigi = null;
            transform.parent = null;
        }
    }
}