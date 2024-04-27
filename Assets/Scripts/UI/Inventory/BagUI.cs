using System.Collections;
using System.Collections.Generic;
using MyGame.Inventory;
using UnityEngine;
namespace MyGame.UISystem.InventoryUI
{
    public class BagUI : MonoBehaviour
    {
        private SlotUI[] slots;
        private RectTransform rectTransform;
        private Coroutine foldCoroutine;
        private bool isOpen;
        [SerializeField] Vector2 flodPos;
        [SerializeField] Vector2 openPos;
        [SerializeField] float flodTime;
        private void Awake()
        {
            slots = GetComponentsInChildren<SlotUI>();
            rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = flodPos;
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].index = i;
                slots[i].slotType = SlotType.Bag;
            }
        }
        public void OpenBagSlots(Item[] items)
        {
            if (isOpen)
            {
                UpdateBagSlots(items);
                return;
            }
            isOpen = true;
            if (foldCoroutine != null)
            {
                StopCoroutine(foldCoroutine);
            }
            UpdateBagSlots(items);
            foldCoroutine = StartCoroutine(OpenBagUIIE());
        }
        public void CloseBagUI()
        {
            isOpen = false;
            if (foldCoroutine != null)
            {
                StopCoroutine(foldCoroutine);
            }
            foldCoroutine = StartCoroutine(CloseBagUIIE());
        }
        public void UpdateBagSlots(Item[] items)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].UpdateSlotUI(items[i]);
            }
        }

        IEnumerator OpenBagUIIE()
        {
            var currentPos = rectTransform.anchoredPosition;
            float t = 0;
            while (t < flodTime)
            {
                t += Time.deltaTime;
                rectTransform.anchoredPosition = Vector2.Lerp(currentPos, openPos, t / flodTime);
                yield return null;
            }
        }
        IEnumerator CloseBagUIIE()
        {
            var currentPos = rectTransform.anchoredPosition;
            float t = 0;
            while (t < flodTime)
            {
                t += Time.deltaTime;
                rectTransform.anchoredPosition = Vector2.Lerp(currentPos, flodPos, t / flodTime);
                yield return null;
            }
        }
    }
}