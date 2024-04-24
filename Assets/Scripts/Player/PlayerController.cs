using MyGame.InputSystem;
using MyGame.ShipSystem;
using MyGame.UISystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MyGame.PlayerSystem
{

    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rigi;
        private Animator animator;
        private PlayerState playerState = new PlayerState();
        [SerializeField] private bool isMoveing;
        private int speed = 5;
        //Ship
        [SerializeField] private Rigidbody2D shipRigi;
        private Vector2 shipPos;
        public bool isBoardShip; //是否在船上
        public bool is2Floor;

        [SerializeField] private bool isInteract;
        [SerializeField] Transform interactTrans;


        public SortingGroup sortingGroup;
        [SerializeField] PlayerInteract playerInteract;
        public bool isFireSelf;

        [SerializeField] float decelerationRate = 10f;//减速率

        private void Start()
        {
            rigi = GetComponent<Rigidbody2D>();
            sortingGroup = GetComponent<SortingGroup>();

            EventHandler.PlayerInetractive += Interact;
            BoardShip(shipRigi);
        }

        void Update()
        {
            PlayerInputAction();
        }

        private void LateUpdate()
        {
            AdjustPlayerInteractPos();
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
        }
        private void Interact(bool isInteract)
        {
            this.isInteract = isInteract;
        }
        public void PlayerExitInetract()
        {
            isInteract = false;
            interactTrans = null;
            playerInteract.ExitInteract();
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
            else if (isBoardShip && isInteract)
            {
                transform.localPosition = shipPos;
            }
        }
        public void To2Floor()
        {
            sortingGroup.sortingLayerName = "middleItem";
            is2Floor = true;
        }
        public void Exit2Floor()
        {
            sortingGroup.sortingLayerName = "topItem";
            is2Floor = false;
        }
        public void PlayerInputAction()
        {
            if (isFireSelf)
            {
                rigi.velocity -= rigi.velocity.normalized * decelerationRate * Time.deltaTime;
                if (rigi.velocity.magnitude < 1f)
                {
                    isFireSelf = false;
                }
                return;
            }
            if (isInteract)
            {
                rigi.velocity = isBoardShip ? shipRigi.velocity : Vector2.zero;
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
        /// <summary>
        /// 上船
        /// </summary>
        public void BoardShip(Rigidbody2D shipRigi)
        {
            var worldPos = transform.position;
            isBoardShip = true;
            this.shipRigi = shipRigi;
            transform.parent = shipRigi.transform;
            transform.position = worldPos;
            shipPos = transform.localPosition;
            shipRigi.GetComponent<ShipController>().PlayerEnter();
        }
        public void BoardShip(Rigidbody2D shipRigi, Vector2 pos)
        {
            isBoardShip = true;
            this.shipRigi = shipRigi;
            transform.parent = shipRigi.transform;
            transform.position = pos;
            shipPos = transform.localPosition;
            shipRigi.GetComponent<ShipController>().PlayerEnter();
        }
        /// <summary>
        /// 离开船
        /// </summary>
        public void LeaveShip()
        {
            shipRigi.GetComponent<ShipController>().PlayerLeave();
            isBoardShip = false;
            shipRigi = null;
            transform.parent = null;
            ShipWaterUI.Instance.CloseWaterUI();
        }
        /// <summary>
        /// 爬入大炮
        /// </summary>
        /// <param name="cannonTrans"></param>
        public void EnterCannon(Transform cannonTrans)
        {
            PlayerEnterInteract(cannonTrans);
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            sortingGroup.sortingLayerName = "middleItem";
        }
        /// <summary>
        /// 爬出大炮
        /// </summary>
        public void ExitCannon(Transform exitPoint)
        {
            PlayerEnterInteract(exitPoint);
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            sortingGroup.sortingLayerName = "topItem";
        }
        /// <summary>
        ///发射玩家 
        /// </summary>
        public void FirePlayer(Vector2 firePos, Vector2 dir)
        {
            LeaveShip();
            Debug.Log("发射玩家");
            transform.position = firePos;
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            sortingGroup.sortingLayerName = "topItem";
            PlayerExitInetract();
            isFireSelf = true;
            rigi.velocity = dir * Setting.playerFireSpeed;
        }
    }
}