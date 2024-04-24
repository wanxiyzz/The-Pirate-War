using MyGame.ShipSystem.Hole;
using MyGame.ShipSystem.Sail;
using MyGame.UISystem;
using UnityEngine;
namespace MyGame.ShipSystem
{
    public class ShipController : MonoBehaviour
    {
        //TRAN:船参数
        private ShipState shipState = new ShipState();
        [SerializeField] private float currentShipSpeed = 0;
        [SerializeField] private bool dropAnchor;
        [SerializeField] private float shipSpeed = 5;
        [SerializeField] private float maxAngularVelocity;//最大角速度
        [SerializeField] private float angularVelocity;//当前角速度
        private Rigidbody2D rigi;
        [SerializeField] private ShipHoleManager shipHole;
        [SerializeField] ShipSail shipSail;
        private
        void Start()
        {
            shipSail = GetComponentInChildren<ShipSail>();
            rigi = GetComponent<Rigidbody2D>();
            shipHole = GetComponentInChildren<ShipHoleManager>();
        }
        void Update()
        {

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
            if (dropAnchor) return;
            currentShipSpeed = shipSpeed * shipSail.sailValue;
            Vector2 direction = transform.right;
            rigi.velocity = direction * currentShipSpeed;
            angularVelocity = Mathf.Clamp(angularVelocity, -maxAngularVelocity, maxAngularVelocity);
            rigi.angularVelocity = angularVelocity;
        }
        /// <summary>
        /// 下锚
        /// </summary>
        public void SetAnchor()
        {
            dropAnchor = true;
            rigi.velocity = Vector2.zero;
        }
        /// <summary>
        /// 起锚
        /// </summary>
        public void HoistAnchor()
        {
            dropAnchor = false;
        }
        public void TakeCannonball(Vector2 position)
        {
            WoodClipPool.Instance.PrepareObject(position);
            shipHole.BrokenHole(position);
        }
        public void PlayerLeave()
        {
            shipSail.RevealSail();
            ShipWaterUI.Instance.OpenWaterUI(GetComponent<ShipTakeWater>());
            UIManager.Instance.CloseSailUI();
        }
        public void PlayerEnter()
        {
            shipSail.HiddenSail();
            ShipWaterUI.Instance.OpenWaterUI(GetComponent<ShipTakeWater>());
            UIManager.Instance.OpenSailUI(shipSail);

        }
    }
}