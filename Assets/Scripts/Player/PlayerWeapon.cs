using UnityEngine;
using MyGame.WeaponSystem;
namespace MyGame.PlayerSystem
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon[] allWeapons;
        [SerializeField] private Weapon[] carryWeapon;
        [SerializeField] private Weapon currentWeapon;
        private int weaponIndex = 0;
        private void Awake()
        {
            // TODO:测试完删除
            allWeapons = GetComponentsInChildren<Weapon>();
            carryWeapon[0] = allWeapons[0];
            carryWeapon[1] = allWeapons[1];
            for (int i = 1; i < allWeapons.Length; i++)
            {
                allWeapons[i].gameObject.SetActive(false);
            }
            EventHandler.PlayerInetractive += OnPlayerInetractive;
        }

        private void OnPlayerInetractive(bool isInteractive)
        {
            if (isInteractive) PutWeapon();
            else TakeoutWeapon();
        }

        void Start()
        {
            currentWeapon = carryWeapon[weaponIndex];
        }
        /// <summary>
        /// 从武器盒中切武器
        /// </summary>
        public void ChangeWeapon(WeaponType weaponType, int index)
        {
            carryWeapon[index] = allWeapons[(int)weaponType];
            currentWeapon.Init();
            currentWeapon.gameObject.SetActive(true);
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
            if (weaponIndex > carryWeapon.Length)
            {
                weaponIndex = 0;
            }
            currentWeapon = carryWeapon[weaponIndex];
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
                weaponIndex = carryWeapon.Length - 1;
            }
            currentWeapon = carryWeapon[weaponIndex];
            currentWeapon.gameObject.SetActive(true);
        }
        /// <summary>
        /// 收起武器
        /// </summary>
        public void PutWeapon()
        {
            currentWeapon.gameObject.SetActive(false);
        }
        /// <summary>
        /// 拿出武器
        /// </summary>
        public void TakeoutWeapon()
        {
            Debug.Log("拿出武器");
            currentWeapon.gameObject.SetActive(true);
        }
    }
}
