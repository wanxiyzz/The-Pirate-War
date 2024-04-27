using System.Collections.Generic;
using UnityEngine;
using MyGame.Inventory;
namespace MyGame.Inventory
{
    [CreateAssetMenu(fileName = "ItemDetailsData", menuName = "Inventory/ItemDetailsData")]
    public class ItemDetailsData : ScriptableObject
    {
        public List<ItemDetail> itemDetailList;
    }
}