using UnityEngine;
using MyGame.InputSystem;
using MyGame.HandheldableSystem.WeaponSystem;
using MyGame.UISystem;
using Photon.Pun;

namespace MyGame.PlayerSystem
{
    public class PlayerWeapon : MonoBehaviourPun
    {
        [SerializeField] private Weapon[] allWeapons;
        public Weapon[] carryWeapons;
        public Weapon currentWeapon;
        [SerializeField] private int weaponIndex = 0;
        public int canTackWeaponNum = 2;
        public bool haveProp;
        public PlayerController playerController;
        public bool Is2F => playerController.playerPos == PlayerPos.Ship2F;
        public bool IsEnemy => playerController.isEnemy;
        private void OnPlayerInetractive(bool isInteractive)
        {
            if (isInteractive) PutWeaponPun();
            else TakeoutWeaponPun();
        }

        protected void Awake()
        {
            playerController = GetComponentInParent<PlayerController>();
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
            if (photonView.IsMine)
            {
                EventHandler.PlayerInetractive += OnPlayerInetractive;
                EventHandler.PickUpAllItem += OnPlayerInetractive;
                GameInput.Instance.ChangeLastWeaponAction += SwitchLastWeaponPun;
            }

            currentWeapon = carryWeapons[weaponIndex];
        }
        private void Start()
        {
            if (photonView.IsMine)
                WeaponUI.Instance.InitWeaponUI(carryWeapons);
        }
        /// <summary>
        /// 从武器盒中切武器
        /// </summary>
        public void ChangeWeaponPun(WeaponType weaponType, int index)
        {
            photonView.RPC("ChangeWeapon", RpcTarget.All, weaponType, index);
        }
        [PunRPC]
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
        public void SwitchLastWeaponPun()
        {
            photonView.RPC("SwitchLastWeapon", RpcTarget.All);
        }
        [PunRPC]
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
        public void PutWeaponPun()
        {
            photonView.RPC("PutWeapon", RpcTarget.All);
        }
        [PunRPC]
        public void PutWeapon()
        {
            WeaponUI.Instance.DeselectWeaponUI();
            currentWeapon.gameObject.SetActive(false);
        }
        public void ExchangeWeaponPun(int index1, int index2)
        {
            photonView.RPC("ExchangeWeapon", RpcTarget.All, index1, index2);
        }
        /// <summary>
        /// 交换背包武器
        /// </summary>
        [PunRPC]
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
        public void TakeoutWeaponPun()
        {
            photonView.RPC("TakeoutWeapon", RpcTarget.All);
        }
        [PunRPC]
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
