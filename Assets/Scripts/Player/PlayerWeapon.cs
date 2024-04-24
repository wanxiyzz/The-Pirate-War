using UnityEngine;
using MyGame.InputSystem;
using MyGame.HandheldableSystem.WeaponSystem;
using MyGame.UISystem;

namespace MyGame.PlayerSystem
{
    public class PlayerWeapon : Singleton<PlayerWeapon>
    {
        [SerializeField] private Weapon[] allWeapons;
        public Weapon[] carryWeapons;
        public Weapon currentWeapon;
        [SerializeField] private int weaponIndex = 0;
        public int canTackWeaponNum = 2;
        public bool haveProp;
        private void OnPlayerInetractive(bool isInteractive)
        {
            if (isInteractive) PutWeapon();
            else TakeoutWeapon();
        }

        protected override void Awake()
        {
            base.Awake();
            allWeapons = GetComponentsInChildren<Weapon>();
            carryWeapons = new Weapon[canTackWeaponNum];
            for (int i = 0; i < canTackWeaponNum; i++)
            {
                carryWeapons[i] = allWeapons[i];
            }
            for (int i = 1; i < allWeapons.Length; i++)
            {
                allWeapons[i].gameObject.SetActive(false);
            }
            EventHandler.PlayerInetractive += OnPlayerInetractive;
            GameInput.Instance.ChangeLastWeaponAction += SwitchLastWeapon;

            currentWeapon = carryWeapons[weaponIndex];
        }
        private void Start()
        {
            WeaponUI.Instance.InitWeaponUI(carryWeapons);
        }
        /// <summary>
        /// 从武器盒中切武器
        /// </summary>
        public void ChangeWeapon(WeaponType weaponType, int index)
        {
            if (currentWeapon == carryWeapons[index])
            {
                currentWeapon = allWeapons[(int)weaponType];
            }
            carryWeapons[index] = allWeapons[(int)weaponType];
            carryWeapons[index].Init();
            WeaponUI.Instance.ChangeWeaponUI(carryWeapons[index], index);
        }
        /// <summary>
        /// 切换下一个武器
        /// </summary>
        /// <param name="index"></param>
        public void SwitchNextWeapon()
        {
            if (currentWeapon.gameObject.activeSelf)
            {
                currentWeapon.gameObject.SetActive(false);
                weaponIndex += 1;
                if (weaponIndex > carryWeapons.Length)
                {
                    weaponIndex = 0;
                }
                currentWeapon = carryWeapons[weaponIndex];
                currentWeapon.gameObject.SetActive(true);
            }
            WeaponUI.Instance.SelectWeaponUI(weaponIndex);
        }
        /// <summary>
        /// 切换上一个武器
        /// </summary>
        /// <param name="index"></param>
        public void SwitchLastWeapon()
        {
            if (currentWeapon.gameObject.activeSelf)
            {
                currentWeapon.gameObject.SetActive(false);
                weaponIndex -= 1;
                if (weaponIndex < 0)
                {
                    weaponIndex = carryWeapons.Length - 1;
                }
                currentWeapon = carryWeapons[weaponIndex];
                currentWeapon.gameObject.SetActive(true);
                WeaponUI.Instance.SelectWeaponUI(weaponIndex);
            }
        }
        /// <summary>
        /// 收起武器
        /// </summary>
        public void PutWeapon()
        {
            WeaponUI.Instance.DeselectWeaponUI();
            currentWeapon.gameObject.SetActive(false);
        }
        /// <summary>
        /// 交换背包武器
        /// </summary>
        public void ExchangeWeapon(int index1, int index2)
        {
            carryWeapons[index1].gameObject.SetActive(false);
            carryWeapons[index2].gameObject.SetActive(false);
            Weapon temp = carryWeapons[index1];
            carryWeapons[index1] = carryWeapons[index2];
            carryWeapons[index2] = temp;
            currentWeapon = carryWeapons[weaponIndex];
            currentWeapon.gameObject.SetActive(true);
            WeaponUI.Instance.UpdateWeaponUI(carryWeapons);
        }
        /// <summary>
        /// 拿出武器
        /// </summary>
        public void TakeoutWeapon()
        {
            if (haveProp) return;
            WeaponUI.Instance.SelectWeaponUI(weaponIndex);
            currentWeapon.gameObject.SetActive(true);
        }
        public void AddBullet()
        {
            currentWeapon.currentBullets = currentWeapon.maxBullets;
            currentWeapon.CallUpdateBullet();
        }
    }
}
