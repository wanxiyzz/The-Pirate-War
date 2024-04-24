using MyGame.HandheldableSystem.WeaponSystem;
using UnityEngine;
namespace MyGame.UISystem
{
    public class WeaponUI : Singleton<WeaponUI>
    {
        public GameObject weaponSlotPrefab;
        public WeaponSlot[] weaponSlots;
        public int currentWeaponIndex = 0;
        public void InitWeaponUI(Weapon[] weapons)
        {
            // 初始化武器UI
            weaponSlots = new WeaponSlot[weapons.Length];
            for (int i = 0; i < weaponSlots.Length; i++)
            {
                weaponSlots[i] = Instantiate(weaponSlotPrefab, transform).GetComponent<WeaponSlot>();
                weaponSlots[i].UpdateWeaponSlot(weapons[i]);
            }
            SelectWeaponUI(currentWeaponIndex);
        }
        public void UpdateWeaponUI(Weapon[] weapons)
        {
            for (int i = 0; i < weaponSlots.Length; i++)
            {
                weaponSlots[i].UpdateWeaponSlot(weapons[i]);
            }
            SelectWeaponUI(currentWeaponIndex);
        }
        public void ChangeWeaponUI(Weapon weapon, int index)
        {
            weaponSlots[index].UpdateWeaponSlot(weapon);
        }
        public void SelectWeaponUI(int index)
        {
            weaponSlots[currentWeaponIndex].CloseOpenSlot();
            currentWeaponIndex = index;
            weaponSlots[currentWeaponIndex].OpenWeaponSlot();
        }
        public void DeselectWeaponUI()
        {
            weaponSlots[currentWeaponIndex].CloseOpenSlot();
        }
    }
}