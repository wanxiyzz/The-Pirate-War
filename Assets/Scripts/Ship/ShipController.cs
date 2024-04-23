using UnityEngine;
namespace MyGame.ShipSystem
{
    public class ShipController : MonoBehaviour
    {
        //TRAN:船参数
        private ShipState shipState = new ShipState();

        [SerializeField] private float shipSpeed = 5;
        [SerializeField] private float maxAngularVelocity;//最大角速度
        [SerializeField] private float angularVelocity;//当前角速度
        private Rigidbody2D rigi;
        public Vector2 Velocity => rigi.velocity;
        [SerializeField] private ShipHoleManager shipHole;
        void Start()
        {
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
        public void TakeCannonball(Vector2 position)
        {
            Debug.Log("打中了");
            WoodClipPool.Instance.PrepareObject(position);
            shipHole.BrokenHole(position);
        }

    }
}