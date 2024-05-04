using System.Collections;
using System.Collections.Generic;
using MyGame.Inventory;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MyGame.UISystem.InventoryUI
{
    public class SlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public SlotType slotType;
        public int index;
        [SerializeField] Text amountText;
        [SerializeField] Image iconImage;
        private Image backgroundImage;
        private Button button;
        [SerializeField] Item currentItem;
        private void Awake()
        {
            backgroundImage = GetComponent<Image>();
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
            button.interactable = false;
            amountText.enabled = false;
            iconImage.enabled = false;
        }
        public void UpdateSlotUI(Item item)
        {
            if (item.itemAmount != 0)
            {
                currentItem = item;
                button.interactable = true;
                iconImage.enabled = true;
                amountText.enabled = true;
                iconImage.sprite = InventoryManager.Instance.GetItemDetails(item.itemID).itemIcon;
                amountText.text = item.itemAmount.ToString();
            }
            else
            {
                currentItem.itemID = 0;
                button.interactable = false;
                amountText.enabled = false;
                iconImage.enabled = false;
            }
        }

        private void OnClick()
        {
            if (slotType == SlotType.Bag)
            {
                if (InventoryManager.Instance.CurrentOpenBox)
                {
                    currentItem = InventoryManager.Instance.RemoveToBarral(currentItem, index);
                    UpdateSlotUI(currentItem);
                    InventoryManager.Instance.UpdateBarrelUI();
                }
                else
                {
                    if (InventoryManager.Instance.GetItemDetails(currentItem.itemID).recoveryNum > 0)
                    {
                        UpdateSlotUI(InventoryManager.Instance.UseItemInBag(index));
                    }
                }
            }
            else if (slotType == SlotType.Barrel)
            {
                currentItem = InventoryManager.Instance.RemoveToBag(currentItem, index);
                InventoryManager.Instance.UpdateBagUI();
                UpdateSlotUI(currentItem);
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (currentItem.itemAmount != 0)
            {
                UIManager.Instance.dragImage.enabled = true;
                UIManager.Instance.dragImage.sprite = iconImage.sprite;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            UIManager.Instance.dragImage.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
            }
            UIManager.Instance.dragImage.enabled = false;
            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.TryGetComponent<SlotUI>(out SlotUI slotUI))
            {
                if (slotType == SlotType.Bag && slotUI.slotType == SlotType.Barrel)
                {
                    currentItem = InventoryManager.Instance.RemoveToBarral(currentItem, index, slotUI.index);
                    UpdateSlotUI(currentItem);
                    InventoryManager.Instance.UpdateBarrelUI();
                }
                else if (slotType == SlotType.Barrel && slotUI.slotType == SlotType.Bag)
                {
                    currentItem = InventoryManager.Instance.RemoveToBag(currentItem, index, slotUI.index);
                    InventoryManager.Instance.UpdateBagUI();
                    UpdateSlotUI(currentItem);
                }
                else if (slotType == SlotType.Barrel && slotUI.slotType == SlotType.Barrel)
                {
                    InventoryManager.Instance.ExchangeBarreItem(index, slotUI.index);
                }
                else if (slotType == SlotType.Bag && slotUI.slotType == SlotType.Bag)
                {
                    InventoryManager.Instance.ExchangeBagItem(index, slotUI.index);
                }
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (button.interactable)
                backgroundImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            backgroundImage.color = Color.white;
        }
    }
}