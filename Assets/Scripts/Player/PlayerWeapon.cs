using UnityEngine;
using MyGame.WeaponSystem;
namespace MyGame.PlayerSystem
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon[] allWeapons;
        [SerializeField] private Weapon[] caryyWeapon = new Weapon[2];
        private Weapon currentWeapon;
        private int weaponIndex = 0;
        private void Awake()
        {
            //TODO:测试完删除
            // allWeapons = GetComponentsInChildren<Weapon>();
            // caryyWeapon[0] = allWeapons[0];
            // caryyWeapon[1] = allWeapons[1];
            // for (int i = 1; i < allWeapons.Length; i++)
            // {
            //     allWeapons[i].gameObject.SetActive(false);
            // }
        }

        void Start()
        {
            currentWeapon = caryyWeapon[weaponIndex];
        }
        public void ChangeWeapon(WeaponType weaponType, int index)
        {
            if (weaponIndex == index)
            {
                currentWeapon.gameObject.SetActive(false);
                currentWeapon = caryyWeapon[index];
            }
            caryyWeapon[index] = allWeapons[(int)weaponType];
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
        public void PickUpWeapon()
        {
            currentWeapon.gameObject.SetActive(false);
        }
        public void PickOnWeapon()
        {
            currentWeapon.gameObject.SetActive(true);
        }
    }
}
