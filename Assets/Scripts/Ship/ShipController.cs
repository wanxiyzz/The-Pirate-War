using UnityEngine;
namespace MyGame.ShipSystem
{
    public class ShipController : MonoBehaviour
    {
        //TRAN:船参数
        private ShipState shipState = new ShipState();

        [SerializeField] private float shipSpeed = 5;
        [SerializeField] private float maxAngularVelocity;//最大扭矩
        [SerializeField] private float angularVelocity;//当前扭矩
        private Rigidbody2D rigi;
        void Start()
        {
            rigi = GetComponent<Rigidbody2D>();
        }
        void Update()
        {

        }
        private void FixedUpdate()
        {
            Swerve(angularVelocity);
        }
        /// <summary>
        /// 使用舵来控制扭矩
        /// </summary>
        /// <param name="RudderNum">旋转百分比 正数左 负数右</param>
        public void AdjustTorque(float RudderNum)
        {
            angularVelocity = maxAngularVelocity * RudderNum;
        }
        /// <summary>
        /// 转向
        /// </summary>
        private void Swerve(float torque)
        {
            // float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
            // Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            // rigi.velocity = direction * shipSpeed;
            // float clampedAngularVelocity = Mathf.Clamp(angularVelocity, -maxAngularVelocity, maxAngularVelocity);
            // rigi.angularVelocity = clampedAngularVelocity;
            Vector2 direction = transform.right;
            rigi.velocity = direction * shipSpeed;
            angularVelocity = Mathf.Clamp(angularVelocity, -maxAngularVelocity, maxAngularVelocity);
            rigi.angularVelocity = angularVelocity;
        }
        /// <summary>
        /// 下锚
        /// </summary>
        public void SetAnchor()
        {
            shipSpeed = 0;
            rigi.velocity = Vector2.zero;
        }
        /// <summary>
        /// 起锚
        /// </summary>
        public void HoistAnchor()
        {
        }

    }
}