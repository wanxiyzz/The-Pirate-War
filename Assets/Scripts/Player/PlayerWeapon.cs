using UnityEngine;
using MyGame.WeaponSystem;
namespace MyGame.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon[] AllWeapons;
        [SerializeField] private Weapon[] caryyWeapon = new Weapon[2];
        private int weaponIndex = 0;
        private Weapon currentWeapon;

        void Start()
        {
            currentWeapon = caryyWeapon[weaponIndex];
        }

        void Update()
        {

        }
        public void ChangeWeapon(WeaponType weaponType, int index)
        {
            if (weaponIndex == index)
            {
                currentWeapon.gameObject.SetActive(false);
                currentWeapon = caryyWeapon[index];
            }
            caryyWeapon[index] = AllWeapons[(int)weaponType];
            currentWeapon.Init();
            currentWeapon.gameObject.SetActive(true);
        }
        public void SwitchWeapon(int index)
        {
            currentWeapon.gameObject.SetActive(false);
            weaponIndex = index;
            currentWeapon = caryyWeapon[weaponIndex];
            currentWeapon.gameObject.SetActive(true);
        }
    }
}
