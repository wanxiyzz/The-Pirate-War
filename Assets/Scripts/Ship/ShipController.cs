using System.Collections.Generic;
using MyGame.PlayerSystem;
using MyGame.ShipSystem.Anchor;
using MyGame.ShipSystem.Hole;
using MyGame.ShipSystem.Sail;
using MyGame.UISystem;
using Photon.Pun;
using UnityEngine;
namespace MyGame.ShipSystem
{
    public class ShipController : MonoBehaviourPun, IPunObservable
    {
        public string shipName;
        private ShipState shipState = new ShipState();
        [SerializeField] private float currentShipSpeed = 0;
        [SerializeField] private float shipSpeed = 5;
        [SerializeField] private float maxAngularVelocity;//最大角速度
        [SerializeField] private float angularVelocity;//当前角速度
        private Rigidbody2D rigi;
        [SerializeField] private ShipHoleManager shipHole;
        [SerializeField] ShipSail shipSail;
        [SerializeField] ShipAnchor shipAnchor;
        private Animator animator;
        private List<PlayerController> playerControllers = new List<PlayerController>();
        void Start()
        {
            animator = GetComponent<Animator>();
            shipSail = GetComponentInChildren<ShipSail>();
            rigi = GetComponent<Rigidbody2D>();
            shipHole = GetComponentInChildren<ShipHoleManager>();
            shipAnchor = GetComponentInChildren<ShipAnchor>();
        }
        private void FixedUpdate()
        {
            Swerve(angularVelocity);
        }
        /// <summary>
        /// 使用舵来控制角速度
        /// </summary>
        /// <param name="RudderNum">旋转百分比 正数左 负数右</param>
        public void AdjustAngular(float RudderNum)
        {
            angularVelocity = maxAngularVelocity * RudderNum;
        }
        /// <summary>
        /// 转向
        /// </summary>
        private void Swerve(float torque)
        {
            if (shipAnchor.dropAnchor)
            {
                rigi.velocity = Vector2.zero;
                rigi.angularVelocity = 0;
                animator.Play("Stop");
                return;
            }
            currentShipSpeed = shipSpeed * shipSail.sailValue;
            if (currentShipSpeed > 2)
            {
                animator.Play("Moving");
            }
            Vector2 direction = transform.right;
            rigi.velocity = direction * currentShipSpeed;
            angularVelocity = Mathf.Clamp(angularVelocity, -maxAngularVelocity, maxAngularVelocity);
            rigi.angularVelocity = angularVelocity;
        }
        public void TakeCannonball(Vector2 position)
        {
            WoodClipPool.Instance.PrepareObject(position);
            shipHole.BrokenHole(position);
        }
        public void PlayerLeave(PlayerController player)
        {
            shipSail.RevealSail();
            playerControllers.Remove(player);
            if (player == GameManager.Instance.player)
            {
                ShipWaterUI.Instance.OpenWaterUI(GetComponent<ShipTakeWater>());
                UIManager.Instance.CloseSailUI();
            }
        }
        public void PlayerEnter(PlayerController player)
        {
            playerControllers.Add(player);
            shipSail.HiddenSail();
            if (player == GameManager.Instance.player)
            {
                ShipWaterUI.Instance.OpenWaterUI(GetComponent<ShipTakeWater>());
                UIManager.Instance.OpenSailUI(shipSail);
            }
        }
        public void Shipwreck()
        {
            for (int i = 0; i < playerControllers.Count; i++)
            {
                playerControllers[i].LeaveShipPun();
            }
            playerControllers.Clear();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(shipName);
            }
            else
            {
                shipName = (string)stream.ReceiveNext();
            }
        }
    }
}