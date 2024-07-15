using System.Collections;
using System.Collections.Generic;
using MyGame.ShipSystem;
using Photon.Pun;
using UnityEngine;
namespace MyGame.HandheldableSystem
{
    public class Bucket : Handheldable
    {
        [SerializeField] bool haveWater;
        void Start()
        {

        }
        public override void ItemUsedPun()
        {
            ItemUsed();
        }
        [PunRPC]
        public override void ItemUsed()
        {
            base.ItemUsed();
            if (!haveWater)
            {
                if (GameManager.Instance.player.playerPos == PlayerPos.Ship2F)
                {
                    Debug.Log("Take Water");
                    GameManager.Instance.player.shipRigi.GetComponent<ShipTakeWater>().TakeWater(Setting.barrelage);
                }
                haveWater = true;
            }
            else
            {

            }
        }

    }
}