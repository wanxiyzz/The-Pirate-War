namespace MyGame.WeaponSystem
{
    public class Weapon_Pistol : Weapon
    {
        private void Awake()
        {
            weaponType = WeaponType.Pistol;
            currentBullets = 10;
            maxBullets = 10;
            damage = 20;
            bufferTime = 0.7f;
        }
        protected override void InstantiateAttack()
        {

        }
    }
}