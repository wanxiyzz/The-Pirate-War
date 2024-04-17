using UnityEngine;
using MyGame.InputSystem;
using MyGame.HandheldableSystem.WeaponSystem;

namespace MyGame.PlayerSystem
{
    public class PlayerWeapon : Singleton<PlayerWeapon>
    {
        [SerializeField] private Weapon[] allWeapons;
        public Weapon[] carryWeapons;
        public Weapon currentWeapon;
        [SerializeField] private int weaponIndex = 0;
        public int canTackWeaponNum = 2;
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
        /// <summary>
        /// 从武器盒中切武器
        /// </summary>
        public void ChangeWeapon(WeaponType weaponType, int index)
        {
            if (currentWeapon == carryWeapons[index])
            {
                Debug.Log("替换当前的武器");
                currentWeapon.gameObject.SetActive(false);
                currentWeapon = allWeapons[(int)weaponType];
                currentWeapon.gameObject.SetActive(true);
            }
            carryWeapons[index] = allWeapons[(int)weaponType];
            currentWeapon.Init();
            //TODO:UI更新子弹数
        }
        /// <summary>
        /// 切换下一个武器
        /// </summary>
        /// <param name="index"></param>
        public void SwitchNextWeapon()
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
        /// <summary>
        /// 切换上一个武器
        /// </summary>
        /// <param name="index"></param>
        public void SwitchLastWeapon()
        {
            currentWeapon.gameObject.SetActive(false);
            weaponIndex -= 1;
            if (weaponIndex < 0)
            {
                weaponIndex = carryWeapons.Length - 1;
            }
            currentWeapon = carryWeapons[weaponIndex];
            currentWeapon.gameObject.SetActive(true);
            //TODO: UI更新子弹和枪械
        }
        /// <summary>
        /// 收起武器
        /// </summary>
        public void PutWeapon()
        {
            currentWeapon.gameObject.SetActive(false);
            //TODO: UI更新子弹和枪械
        }
        public void ExchangeWeapon(int index1, int index2)
        {
            carryWeapons[index1].gameObject.SetActive(false);
            carryWeapons[index2].gameObject.SetActive(false);
            Weapon temp = carryWeapons[index1];
            carryWeapons[index1] = carryWeapons[index2];
            carryWeapons[index2] = temp;
            currentWeapon = carryWeapons[weaponIndex];
            currentWeapon.gameObject.SetActive(true);
        }
        /// <summary>
        /// 拿出武器
        /// </summary>
        public void TakeoutWeapon()
        {
            currentWeapon.gameObject.SetActive(true);
        }
    }
}
