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
        public override void WeaponAttack(Vector3 pos, Vector3 Dir)
        {

        }
        protected override void InstantiateAttack()
        {

        }
    }
}