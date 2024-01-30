namespace MyGame.WeaponSystem
{
    public class Weapon_Shotgun : Weapon
    {
        private void Awake()
        {
            weaponType = WeaponType.AWM;
            currentBullets = 8;
            maxBullets = 8;
            damage = 9;
            bufferTime = 1f;
        }
        protected override void InstantiateAttack()
        {

        }
    }
}