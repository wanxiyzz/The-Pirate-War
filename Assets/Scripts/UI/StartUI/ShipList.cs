using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
namespace MyGame.UISystem
{
    public class ShipList : MonoBehaviour
    {
        public GameObject shipState;
        public void Init(Dictionary<string, List<Player>> ships)
        {
            foreach (var ship in ships)
            {
                GameObject newShipState = Instantiate(shipState, transform);
                newShipState.GetComponent<ShipState>().Init(ship.Key, ship.Value.Count);
            }
        }
    }
}