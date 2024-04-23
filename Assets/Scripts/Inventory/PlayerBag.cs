using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.Inventory
{
    public class PlayerBag : Singleton<PlayerBag>
    {
        public int cannonballNum;
        public int boardNum;
        public Item[] bagItems;
        public Item[] currentBoxItems;
        public bool UseBoard()
        {
            if (boardNum > 0)
            {
                boardNum--;
                return true;
            }
            return false;
        }
        public bool UseCannonball()
        {
            if (cannonballNum > 0)
            {
                cannonballNum--;
                return true;
            }
            return false;
        }

    }
}