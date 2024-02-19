using UnityEngine;
namespace MyGame.WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        protected WeaponType weaponType = WeaponType.Knife;
        protected int maxBullets = 0;
        public int currentBullets = 0;
        public int damage = 0;
        public float bufferTime = 0;
        public float currentBufferTime = 0;
        protected bool canAttack;

        //鼠标输入
        public Transform muzzlePos;
        protected Vector2 mousePos;

        protected Vector2 direction;
        protected float timer;
        // private Animator animator;
        protected void Start()
        {
            // animator = GetComponent<Animator>();
            muzzlePos = transform.Find("Muzzle");
        }
        protected void Update()
        {
            Aim();
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
            if (Input.GetMouseButtonDown(0))
            {
                WeaponAttack();
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