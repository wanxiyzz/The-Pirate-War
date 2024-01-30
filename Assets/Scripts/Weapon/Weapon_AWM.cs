namespace MyGame.WeaponSystem
{
    public class Weapon_AWM : Weapon
    {
        private void Awake()
        {
            weaponType = WeaponType.AWM;
            currentBullets = 5;
            maxBullets = 5;
            damage = 40;
            bufferTime = 1f;
        }
        protected override void InstantiateAttack()
        {

        }
    }
}