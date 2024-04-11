using UnityEngine;
namespace MyGame.WeaponSystem
{
    public class Weapon_Knife : Weapon
    {
        private void Awake()
        {
            weaponType = WeaponType.Knife;
            bufferTime = 0.5f;
            damage = 20;
        }
        protected override void Aim()
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
            else
            {
                if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1))
                {
                    //突刺
                    canAttack = false;
                    currentBufferTime = bufferTime;
                    // animator.Play("Sprint");
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    // animator.Play("Attack");
                }
                else if (Input.GetMouseButtonUp(2))
                {
                    // animator.Play("Defense");
                }
            }
        }
        public override void WeaponAttack()
        {

        }
        protected override void InstantiateAttack(Vector3 attackPos, Vector3 attackDir)
        {

        }
    }
}