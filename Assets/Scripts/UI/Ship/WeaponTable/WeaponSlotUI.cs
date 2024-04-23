using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MyGame.UISystem.WeaponTable
{
    public class WeaponSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public WeaponSlotType slotType;
        public WeaponType weaponType;
        public int index;
        private Button button;
        public Image image;
        [SerializeField] Image selectImage;
        bool canSelected;//是否可以点击仅限于桌子
        bool isSelect;//是否被选中仅限于背包
        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
            image = GetComponent<Image>();
            canSelected = true;
        }
        private void OnClick()
        {
            IsSelect();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (canSelected)
            {
                selectImage.enabled = true;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isSelect) return;
            selectImage.enabled = false;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            WeaponTableUI.Instance.dragImage.gameObject.SetActive(true);
            WeaponTableUI.Instance.dragImage.sprite = image.sprite;
        }

        public void OnDrag(PointerEventData eventData)
        {
            WeaponTableUI.Instance.dragImage.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            WeaponTableUI.Instance.dragImage.gameObject.SetActive(false);
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<WeaponSlotUI>(out WeaponSlotUI slotUI))
                {
                    if (slotUI.slotType != slotType)
                    {
                        if (slotType == WeaponSlotType.Bag)
                        {
                            EventHandler.CallChangeWeaponUI(slotUI.index, index);
                        }
                        else
                        {
                            EventHandler.CallChangeWeaponUI(index, slotUI.index);
                        }
                    }
                    else if (slotType == WeaponSlotType.Bag)
                    {
                        WeaponTableUI.Instance.ExchangeWeapon(index, slotUI.index);
                    }
                }
            }
        }
        /// <summary>
        /// 更新UI的属性
        /// </summary>
        public void UpdateSlotUI(Sprite sprite, WeaponType type)
        {
            weaponType = type;
            image.sprite = sprite;
        }
        public void UpdateSlotUI(WeaponSlotUI weaponSlotUI)
        {
            weaponType = weaponSlotUI.weaponType;
            image.sprite = weaponSlotUI.image.sprite;
        }
        /// <summary>
        /// 黑掉桌子上的UI让他无法点击
        /// </summary>
        public void UnableSelected()
        {
            image.color = new Color(0.4f, 0.4f, 0.4f, 1f);
            button.interactable = false;
            selectImage.enabled = false;
            canSelected = false;
        }
        /// <summary>
        /// 恢复桌子上的UI让他可以点击
        /// </summary>
        public void CanSelected()
        {
            image.color = new Color(1f, 1f, 1f, 1f);
            button.interactable = true;
            canSelected = true;
        }
        /// <summary>
        /// 点击事件
        /// </summary>
        public void IsSelect()
        {
            if (slotType == WeaponSlotType.Bag)
            {
                WeaponTableUI.Instance.SelectBagSlot(index);
                isSelect = true;
                selectImage.enabled = true;
            }
            else
            {
                WeaponTableUI.Instance.ChangeWeaponUI(index);
            }
        }
        /// <summary>
        /// 背包UI取消选择
        /// </summary>
        public void NoSelect()
        {
            isSelect = false;
            selectImage.enabled = false;
        }
    }
}