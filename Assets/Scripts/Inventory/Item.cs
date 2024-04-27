using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace MyGame.Inventory
{
    [Serializable]
    public struct Item
    {
        public int itemID;
        public int itemAmount;
    }
    [Serializable]
    public class ItemDetail
    {
        public int itemID;
        public string itemName;
        public Sprite itemIcon;
        public int recoveryNum;
    }
}