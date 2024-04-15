using UnityEngine;
using MyGame.InputSystem;
namespace MyGame.WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponType weaponType = WeaponType.Knife;
        [SerializeField] protected int maxBullets = 0;
        public int currentBullets = 0;
        public int damage = 0;
        public float bufferTime = 0;
        public float currentBufferTime = 0;
        protected bool canAttack;

        //鼠标输入
        public Transform muzzlePos;
        protected Vector2 mousePos;

        protected Vector2 direction;
        // private Animator animator;
        protected void Awake()
        {
            // animator = GetComponent<Animator>();
            muzzlePos = transform.Find("Muzzle");
        }
        protected void Update()
        {
            Aim();
        }
        protected void OnEnable()
        {
            GameInput.Instance.AttackAction += WeaponAttack;
        }
        protected void OnDisable()
        {
            GameInput.Instance.AttackAction -= WeaponAttack;
        }
        protected virtual void Aim()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePos - (Vector2)transform.position).normalized;
            transform.right = direction;
            if (!canAttack)
            {
                currentBufferTime -= Time.deltaTime;
                if (currentBufferTime < 0)
                {
                    canAttack = true;
                }
            }
        }

        public void Init()
        {
            currentBullets = maxBullets;
            currentBufferTime = 0;
            canAttack = true;
        }
        public virtual void WeaponAttack()
        {
            if (currentBullets > 0 && canAttack)
            {
                currentBullets--;
                currentBufferTime = bufferTime;
                canAttack = false;
                InstantiateAttack(muzzlePos.position, direction);
            }
        }
        protected virtual void InstantiateAttack(Vector3 attackPos, Vector3 attackDir)
        {

        }
    }
}