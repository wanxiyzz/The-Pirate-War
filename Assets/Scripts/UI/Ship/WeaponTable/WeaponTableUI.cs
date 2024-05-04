using UnityEngine;
using UnityEngine.UI;
using MyGame.PlayerSystem;
using MyGame.HandheldableSystem.WeaponSystem;

namespace MyGame.UISystem.WeaponTable
{
    public class WeaponTableUI : Singleton<WeaponTableUI>
    {
        [SerializeField] WeaponSlotUI[] tableWeaponSlot;
        [SerializeField] WeaponSlotUI[] bagWeaponSlot;
        [SerializeField] WeaponSlotUI weaponPrefab;
        [SerializeField] Transform bagWeaponParent;
        public Image dragImage;

        public int currentSelectBag;
        private void OnEnable()
        {
            EventHandler.ChangeWeaponUI += OnChangeWeaponUI;
        }
        private void OnDisable()
        {
            EventHandler.ChangeWeaponUI -= OnChangeWeaponUI;
        }
        private void Start()
        {
            for (int i = 0; i < tableWeaponSlot.Length; i++)
            {
                tableWeaponSlot[i].index = i;
            }
        }
        protected override void Awake()
        {
            base.Awake();
            UpdateBagWeapon(GameManager.Instance.player.playerWeapon.carryWeapons);
        }
        /// <summary>
        /// 更新背包武器的UI
        /// </summary>
        /// <param name="weapons"></param>
        public void UpdateBagWeapon(Weapon[] weapons)
        {
            bagWeaponSlot = new WeaponSlotUI[weapons.Length];

            for (int i = 0; i < bagWeaponSlot.Length; i++)
            {
                bagWeaponSlot[i] = Instantiate(weaponPrefab, bagWeaponParent);
                bagWeaponSlot[i].index = i;
                bagWeaponSlot[i].UpdateSlotUI(UIManager.Instance.weaponSprites[(int)weapons[i].weaponType], weapons[i].weaponType);
                bagWeaponSlot[i].slotType = WeaponSlotType.Bag;
                for (int j = 0; j < tableWeaponSlot.Length; j++)
                {
                    if (weapons[i].weaponType == tableWeaponSlot[j].weaponType)
                    {
                        tableWeaponSlot[j].UnableSelected();
                    }

                }
            }
            bagWeaponSlot[0].IsSelect();
        }
        /// <summary>
        /// 武器桌和背包之间的交换
        /// </summary>
        public void OnChangeWeaponUI(int tableIndex, int bagIndex)
        {
            var tableType = tableWeaponSlot[tableIndex].weaponType;
            var bagType = bagWeaponSlot[bagIndex].weaponType;
            for (int i = 0; i < bagWeaponSlot.Length; i++)
            {
                if (bagWeaponSlot[i].weaponType == tableType)
                {
                    Debug.Log("有相同的武器");
                    if (i != bagIndex)
                    {
                        ExchangeWeapon(bagIndex, i);
                        return;
                    }
                    else return;
                }
            }
            tableWeaponSlot[(int)bagType].CanSelected();
            bagWeaponSlot[bagIndex].UpdateSlotUI(UIManager.Instance.weaponSprites[(int)tableType], tableType);
            tableWeaponSlot[tableIndex].UnableSelected();
            GameManager.Instance.player.playerWeapon.ChangeWeapon(tableType, bagIndex);
        }
        /// <summary>
        ///简略版武器的更换 
        /// </summary>
        public void ChangeWeaponUI(int tableIndex)
        {
            var tableType = tableWeaponSlot[tableIndex].weaponType;
            var bagType = bagWeaponSlot[currentSelectBag].weaponType;
            tableWeaponSlot[(int)bagType].CanSelected();
            bagWeaponSlot[currentSelectBag].UpdateSlotUI(UIManager.Instance.weaponSprites[(int)tableType], tableType);
            tableWeaponSlot[tableIndex].UnableSelected();
            GameManager.Instance.player.playerWeapon.ChangeWeapon(tableType, currentSelectBag);
        }
        /// <summary>
        /// 背包武器之间的交换
        /// </summary>
        public void ExchangeWeapon(int index1, int index2)
        {
            var index2Type = bagWeaponSlot[index2].weaponType;
            var index2Sprite = bagWeaponSlot[index2].image.sprite;
            GameManager.Instance.player.playerWeapon.ExchangeWeapon(index1, index2);
            bagWeaponSlot[index2].UpdateSlotUI(bagWeaponSlot[index1]);
            bagWeaponSlot[index1].UpdateSlotUI(index2Sprite, index2Type);
        }
        /// <summary>
        /// 选择背包的武器
        /// </summary>
        public void SelectBagSlot(int index)
        {
            bagWeaponSlot[currentSelectBag].NoSelect();
            currentSelectBag = index;
        }
    }
}