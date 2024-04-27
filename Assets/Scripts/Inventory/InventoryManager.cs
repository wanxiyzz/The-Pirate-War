using System.Collections;
using System.Collections.Generic;
using MyGame.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyGame.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        public ItemDetailsData itemDetailsData;
        public Item[] bagItems = new Item[5];
        public Item[] currenBarrelItems;
        public BarrelType barrelType;
        public bool HaveCannonBall
        {
            get
            {
                foreach (var item in bagItems)
                {
                    if (item.itemID == 1001)
                    {
                        if (item.itemAmount > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        public bool HaveBoard
        {
            get
            {
                foreach (var item in bagItems)
                {
                    if (item.itemID == 1000)
                    {
                        if (item.itemAmount > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        public bool CurrentOpenBox => currenBarrelItems != null;
        public bool isOpenBag;
        private void OnEnable()
        {
            GameInput.Instance.playerInputActions.Player.OpenBag.performed += OnOpenBag;
            EventHandler.OpenBarrel += OnOpenBarrel;
        }
        private void OnDisable()
        {
            EventHandler.OpenBarrel -= OnOpenBarrel;
        }
        private void Start()
        {
            isOpenBag = true;
            UpdateBagUI();
        }
        private void OnOpenBag(InputAction.CallbackContext context)
        {
            isOpenBag = !isOpenBag;
            if (isOpenBag)
            {
                EventHandler.CallOpenBagUI(bagItems, true);
            }
            else
            {
                EventHandler.CallOpenBagUI(bagItems, false);
            }
        }
        public void UpdateBarrelUI()
        {
            EventHandler.CallOpenBarrelUI(currenBarrelItems, true, barrelType);
        }
        public void UpdateBagUI()
        {
            EventHandler.CallOpenBagUI(bagItems, true);
        }
        private void OnOpenBarrel(Item[] items, bool arg2, BarrelType type)
        {
            if (arg2)
            {
                currenBarrelItems = items;
                EventHandler.CallOpenBarrelUI(currenBarrelItems, true, type);
            }
            else
            {
                currenBarrelItems = null;
                EventHandler.CallOpenBarrelUI(null, false, type);
            }
            barrelType = type;
        }

        public void UseBoard()
        {
            for (int i = 0; i < bagItems.Length; i++)
            {
                if (bagItems[i].itemID == 1000)
                {
                    if (bagItems[i].itemAmount > 0)
                    {
                        bagItems[i].itemAmount--;
                        if (isOpenBag)
                        {
                            EventHandler.CallOpenBagUI(bagItems, true);
                        }
                        return;
                    }
                }
            }
        }
        public void UseCannonball()
        {
            for (int i = 0; i < bagItems.Length; i++)
            {
                if (bagItems[i].itemID == 1001)
                {
                    if (bagItems[i].itemAmount > 0)
                    {
                        bagItems[i].itemAmount--;
                        if (isOpenBag)
                        {
                            EventHandler.CallOpenBagUI(bagItems, true);
                        }
                        return;
                    }
                }
            }
        }
        public Item UseItemInBag(int index)
        {
            bagItems[index].itemAmount--;
            if (isOpenBag)
            {
                EventHandler.CallOpenBagUI(bagItems, true);
            }
            return bagItems[index];
        }
        public ItemDetail GetItemDetails(int id)
        {
            return itemDetailsData.itemDetailList[id - 1000];
        }
        public Item RemoveToBag(Item item, int barralIndex, int bagIndex)
        {
            if (bagItems[bagIndex].itemAmount == 0)
            {
                bagItems[bagIndex] = item;
                currenBarrelItems[barralIndex] = new Item();
                return currenBarrelItems[barralIndex];
            }
            else if (bagItems[bagIndex].itemID == item.itemID)
            {
                bagItems[bagIndex].itemAmount += item.itemAmount;
                if (bagItems[bagIndex].itemAmount > 10)
                {
                    int num = bagItems[bagIndex].itemAmount - 10;
                    bagItems[bagIndex].itemAmount = 10;
                    currenBarrelItems[barralIndex] = new Item()
                    {
                        itemAmount = num,
                        itemID = bagItems[bagIndex].itemID
                    };
                    return currenBarrelItems[barralIndex];
                }
            }
            else
            {
                ExchangeBagAndBarreItem(bagIndex, barralIndex);
            }
            return currenBarrelItems[barralIndex];
        }
        public Item RemoveToBarral(Item item, int bagIndex, int barralIndex)
        {
            if (barrelType == BarrelType.Board)
            {
                if (item.itemID != 1000) return item;
            }
            else if (barrelType == BarrelType.CannonBall)
            {
                if (item.itemID != 1001) return item;
            }
            else if (barrelType == BarrelType.Fruit)
            {
                if (item.itemID < 1002) return item;
            }
            if (currenBarrelItems[barralIndex].itemAmount == 0)
            {
                currenBarrelItems[barralIndex] = item;
                bagItems[bagIndex] = new Item();
                return bagItems[bagIndex];
            }
            else if (currenBarrelItems[barralIndex].itemID == item.itemID)
            {
                currenBarrelItems[barralIndex].itemAmount += item.itemAmount;
                if (currenBarrelItems[barralIndex].itemAmount > 10)
                {
                    int num = currenBarrelItems[barralIndex].itemAmount - 10;
                    currenBarrelItems[barralIndex].itemAmount = 10;
                    bagItems[bagIndex] = new Item()
                    {
                        itemAmount = num,
                        itemID = currenBarrelItems[barralIndex].itemID
                    };
                    return bagItems[bagIndex];
                }
            }
            else
            {
                ExchangeBagAndBarreItem(bagIndex, barralIndex);
            }
            return bagItems[bagIndex]; ;
        }
        public Item RemoveToBag(Item item, int currentIndex)
        {
            for (int i = 0; i < bagItems.Length; i++)
            {
                if (bagItems[i].itemAmount == 0)
                {
                    bagItems[i] = item;
                    currenBarrelItems[currentIndex] = new Item();
                    return currenBarrelItems[currentIndex];
                }
                else if (bagItems[i].itemID == item.itemID)
                {
                    bagItems[i].itemAmount += item.itemAmount;
                    if (bagItems[i].itemAmount > 10)
                    {
                        int num = bagItems[i].itemAmount - 10;
                        bagItems[i].itemAmount = 10;
                        currenBarrelItems[currentIndex] = new Item()
                        {
                            itemAmount = num,
                            itemID = bagItems[i].itemID
                        };
                        return currenBarrelItems[currentIndex];
                    }
                    currenBarrelItems[currentIndex] = new Item();
                    return currenBarrelItems[currentIndex];
                }
            }
            return currenBarrelItems[currentIndex];
        }
        public Item AddItemToBarral(Item item)
        {
            for (int i = 0; i < currenBarrelItems.Length; i++)
            {
                if (currenBarrelItems[i].itemAmount == 0)
                {
                    currenBarrelItems[i] = item;
                    return new Item();
                }
                else if (currenBarrelItems[i].itemID == item.itemID)
                {
                    currenBarrelItems[i].itemAmount += item.itemAmount;
                    if (currenBarrelItems[i].itemAmount > 10)
                    {
                        int num = currenBarrelItems[i].itemAmount - 10;
                        currenBarrelItems[i].itemAmount = 10;
                        item = new Item()
                        {
                            itemAmount = num,
                            itemID = currenBarrelItems[i].itemID
                        };
                    }
                }
            }
            return item;
        }
        public Item RemoveToBarral(Item item, int currentIndex)
        {
            if (currenBarrelItems == null) return item;
            if (barrelType == BarrelType.Board)
            {
                if (item.itemID == 1000)
                {
                    bagItems[currentIndex] = AddItemToBarral(item);
                    return bagItems[currentIndex];
                }
                return item;
            }
            else if (barrelType == BarrelType.CannonBall)
            {
                if (item.itemID == 1001)
                {
                    bagItems[currentIndex] = AddItemToBarral(item);
                    return bagItems[currentIndex];
                }
                return item;
            }
            else if (barrelType == BarrelType.Fruit)
            {
                if (item.itemID > 1001)
                {
                    bagItems[currentIndex] = AddItemToBarral(item);
                    return bagItems[currentIndex];
                }
                return item;
            }
            else
            {
                bagItems[currentIndex] = AddItemToBarral(item);
                return bagItems[currentIndex];
            }
        }
        public void ExchangeBarreItem(int index1, int index2)
        {
            var temp = currenBarrelItems[index1];
            currenBarrelItems[index1] = currenBarrelItems[index2];
            currenBarrelItems[index2] = temp;
            EventHandler.CallOpenBarrelUI(currenBarrelItems, true, barrelType);
        }
        public void ExchangeBagItem(int index1, int index2)
        {
            var temp = bagItems[index1];
            bagItems[index1] = bagItems[index2];
            bagItems[index2] = temp;
            EventHandler.CallOpenBagUI(bagItems, true);
        }
        public void ExchangeBagAndBarreItem(int bagIndex, int barralIndex)
        {
            var temp = currenBarrelItems[barralIndex];
            currenBarrelItems[barralIndex] = bagItems[bagIndex];
            bagItems[bagIndex] = temp;
        }
    }
}
