using MyGame.InputSystem;
using MyGame.ShipSystem;
using MyGame.UISystem;
using UnityEngine;
using UnityEngine.Rendering;
using Photon.Pun;
using Mygame.PlayerSystem;

namespace MyGame.PlayerSystem
{
    public class PlayerController : MonoBehaviourPun, IPunObservable
    {
        public bool isEnemy;
        private Rigidbody2D rigi;
        public bool isMoveing;
        private int speed = 5;
        //Ship
        public string shipHomeName;
        public Rigidbody2D shipRigi;
        private Vector2 shipPos;
        public bool isBoardShip; //是否在船上

        [SerializeField] private bool isInteract;
        [SerializeField] Transform interactTrans;


        public SortingGroup sortingGroup;
        [SerializeField] PlayerInteract playerInteract;
        public bool isFireSelf;

        [SerializeField] float decelerationRate = 10f;//减速率
        public PlayerPos playerPos;
        public PlayerWeapon playerWeapon;
        public PlayerHealth playerHealth;
        public PlayerAnimation playerAnimation;
        public bool isAnimation;
        public GameObject playerInSeaEff;
        [SerializeField] private PlayerBackShip playerBackShip;
        private void Awake()
        {
            Debug.Log(GameManager.Instance);
            if (photonView.IsMine)
            {
                GameManager.Instance.player = this;
            }
        }
        private void Start()
        {
            playerAnimation = GetComponent<PlayerAnimation>();
            playerHealth = GetComponent<PlayerHealth>();
            rigi = GetComponent<Rigidbody2D>();
            sortingGroup = GetComponent<SortingGroup>();
            playerBackShip = GetComponent<PlayerBackShip>();
            if (photonView.IsMine)
                EventHandler.PlayerInetractive += Interact;
        }

        void Update()
        {
            if (photonView.IsMine)
            {
                PlayerInputAction();
            }
        }

        private void LateUpdate()
        {
            if (photonView.IsMine)
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
        }

        public void Init(Transform shipTrans)
        {
            ShipController shipController = shipTrans.GetComponent<ShipController>();
            shipHomeName = shipController.shipName;
            BoardShipPun(shipController.shipName);
            playerBackShip.shipTrans = shipTrans;
            playerBackShip.shipName = shipHomeName;
        }
        private void Aim()
        {
            var direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            transform.right = (Vector2)direction;
        }
        private void Interact(bool isInteract)
        {
            this.isInteract = isInteract;
            if (!isInteract)
            {
                interactTrans = null;
            }
            Debug.Log(isInteract);
        }
        public void PlayerExitInetract()
        {
            isInteract = false;
            interactTrans = null;
            playerInteract.ExitInteract();
        }

        public void PlayerEnterInteract(Transform interactTrans)
        {
            if (interactTrans != null)
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
            playerPos = PlayerPos.Ship2F;
        }
        public void Exit2Floor()
        {
            sortingGroup.sortingLayerName = "topItem";
            playerPos = PlayerPos.Ship1F;

        }
        public void PlayerInputAction()
        {
            if (isAnimation) return;
            if (isFireSelf)
            {
                rigi.velocity -= rigi.velocity.normalized * decelerationRate * Time.deltaTime;
                if (rigi.velocity.magnitude < 1f)
                {
                    isFireSelf = false;
                    playerPos = PlayerPos.Sea;
                }
                return;
            }
            if (isInteract)
            {
                rigi.velocity = isBoardShip ? shipRigi.velocity : Vector2.zero;
                return;
            }
            Aim();
            Vector2 playerVelocity = GameInput.Instance.GetMovementInput() * speed;
            isMoveing = playerVelocity != Vector2.zero;
            if (isBoardShip)
            {
                playerVelocity += shipRigi.velocity;
            }
            rigi.velocity = playerVelocity;
        }
        public void BoardShipPun(string shipName)
        {
            photonView.RPC("BoardShip", RpcTarget.All, shipName);
        }
        public void BoardShipPun(string shipName, Vector2 pos)
        {
            photonView.RPC("BoardShip", RpcTarget.All, shipName, pos);
        }
        /// <summary>
        /// 上船
        /// </summary>
        [PunRPC]
        public void BoardShip(string shipName)
        {
            var shipRigiBody2D = GameManager.Instance.Getship(shipName).GetComponent<Rigidbody2D>();
            playerAnimation.BoardOrLeaveShip();
            var worldPos = transform.position;
            isBoardShip = true;
            this.shipRigi = shipRigiBody2D;
            transform.parent = shipRigiBody2D.transform;
            transform.position = worldPos;
            shipPos = transform.localPosition;
            shipRigi.GetComponent<ShipController>().PlayerEnter(this);
            playerPos = PlayerPos.Ship1F;
            if (playerInSeaEff != null)
                DestroyImmediate(playerInSeaEff, true);
        }
        [PunRPC]
        public void BoardShip(string shipName, Vector2 pos)
        {
            var shipRigiBody2D = GameManager.Instance.Getship(shipName).GetComponent<Rigidbody2D>();
            playerAnimation.BoardOrLeaveShip();
            isBoardShip = true;
            this.shipRigi = shipRigiBody2D;
            transform.parent = shipRigiBody2D.transform;
            transform.position = pos;
            shipPos = transform.localPosition;
            Debug.Log(shipPos);
            shipRigi.GetComponent<ShipController>().PlayerEnter(this);
            playerPos = PlayerPos.Ship1F;
            if (playerInSeaEff != null)
                DestroyImmediate(playerInSeaEff, true);
        }
        public void LeaveShipPun()
        {
            photonView.RPC("LeaveShip", RpcTarget.All);
        }
        public void LeaveShipPun(Vector2 pos)
        {
            photonView.RPC("LeaveShip", RpcTarget.All, pos);
        }
        /// <summary>
        /// 离开船
        /// </summary>
        [PunRPC]
        public void LeaveShip()
        {
            Debug.Log("角色离开");
            playerPos = PlayerPos.Sea;
            shipRigi.GetComponent<ShipController>().PlayerLeave(this);
            isBoardShip = false;
            shipRigi = null;
            transform.parent = null;
            ShipWaterUI.Instance.CloseWaterUI();
            playerInSeaEff = Instantiate(GameManager.Instance.playerInSeaEffect, transform.position, Quaternion.identity);
            playerInSeaEff.GetComponent<PlayerInSeaEffect>().player = transform;
        }
        [PunRPC]
        public void LeaveShip(Vector2 pos)
        {
            playerAnimation.BoardOrLeaveShip();
            playerPos = PlayerPos.Sea;
            shipRigi.GetComponent<ShipController>().PlayerLeave(this);
            isBoardShip = false;
            shipRigi = null;
            transform.parent = null;
            transform.position = pos;
            ShipWaterUI.Instance.CloseWaterUI();
            playerInSeaEff = Instantiate(GameManager.Instance.playerInSeaEffect, transform.position, Quaternion.identity);
            playerInSeaEff.GetComponent<PlayerInSeaEffect>().player = transform;
        }
        /// <summary>
        /// 爬入大炮
        /// </summary>
        /// <param name="cannonTrans"></param>
        public void EnterCannon(Transform cannonTrans, out PlayerController player)
        {
            player = this;
            PlayerEnterInteract(cannonTrans);
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            sortingGroup.sortingLayerName = "buttom";
        }
        /// <summary>
        /// 爬出大炮
        /// </summary>
        public void ExitCannon(Transform exitPoint)
        {
            PlayerEnterInteract(exitPoint);
            transform.localScale = new Vector3(1f, 1f, 1f);
            sortingGroup.sortingLayerName = "topItem";
        }
        /// <summary>
        ///发射玩家 
        /// </summary>
        public void FirePlayer(Vector2 firePos, Vector2 dir)
        {
            LeaveShipPun();
            Debug.Log("发射玩家");
            transform.position = firePos;
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            sortingGroup.sortingLayerName = "topItem";
            PlayerExitInetract();
            isFireSelf = true;
            rigi.velocity = dir * Setting.playerFireSpeed;
            playerPos = PlayerPos.None;
        }
        public void StartAnimation()
        {
            isAnimation = true;
        }
        public void EndAnimation()
        {
            isAnimation = false;
        }
        public void StartBackShipAnimation()
        {
            isAnimation = true;
            sortingGroup.sortingLayerName = "sky";
        }
        public void EndBackShipAnimation()
        {
            isAnimation = false;
            sortingGroup.sortingLayerName = "topItem";
        }
        //被攻击
        [PunRPC]
        public void ChangeHealth(int health)
        {

            playerHealth.ChangeHealth(health);
        }
        public void PlayerBackShip()
        {
            var ship = GameManager.Instance.Getship(shipHomeName);
            Debug.Log(transform.position);
            isBoardShip = true;
            this.shipRigi = ship.GetComponent<Rigidbody2D>();
            transform.parent = shipRigi.transform;
            transform.position = ship.position;
            shipPos = transform.localPosition;
            Debug.Log(shipPos);
            shipRigi.GetComponent<ShipController>().PlayerEnter(this);
            playerPos = PlayerPos.Ship1F;
            Destroy(playerInSeaEff);
        }
        public void FirePlayerBackShip()
        {
            PlayerExitInetract();
            playerInSeaEff.gameObject.SetActive(false);
            playerAnimation.FirePlayerBackShip();
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(isMoveing);
                stream.SendNext(playerPos);
            }
            else
            {
                isMoveing = (bool)stream.ReceiveNext();
                playerPos = (PlayerPos)stream.ReceiveNext();
            }
        }
    }
}