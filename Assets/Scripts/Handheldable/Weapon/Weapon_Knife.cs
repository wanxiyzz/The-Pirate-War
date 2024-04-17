using UnityEngine;
namespace MyGame.HandheldableSystem.WeaponSystem
{
    public class Weapon_Knife : Weapon
    {
        protected override void Aim()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePos - (Vector2)transform.position).normalized;
            transform.right = direction;
            if (!canUsed)
            {
                currentBufferTime -= Time.deltaTime;
                if (currentBufferTime < 0)
                {
                    canUsed = true;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1))
                {
                    //突刺
                    canUsed = false;
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
        public override void ItemUsed()
        {

        }
        protected override void InstantiateAttack(Vector3 attackPos, Vector3 attackDir)
        {

        }
    }
}