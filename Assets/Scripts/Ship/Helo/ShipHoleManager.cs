using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.ShipSystem.Hole
{
    public class ShipHoleManager : MonoBehaviour
    {
        [SerializeField] Transform[] holeTrans;
        [SerializeField] ShipHole[] shipHoles;
        void Start()
        {
        }
        public int ShipFlooded()
        {
            int holesNum = 0;
            for (int i = 0; i < shipHoles.Length; i++)
            {
                holesNum += shipHoles[i].brokenLevel;
            }
            return holesNum;
        }
        public void To1F()
        {
            for (int i = 0; i < shipHoles.Length; i++)
            {
                shipHoles[i].boxCollider2D.enabled = false;
            }
        }
        public void To2F()
        {
            for (int i = 0; i < shipHoles.Length; i++)
            {
                if (shipHoles[i].brokenLevel > 0)
                    shipHoles[i].boxCollider2D.enabled = true;
            }

        }
        public void BrokenHole(Vector2 pos)
        {
            float minDistance = 10000;
            Transform nearestHole = null;
            foreach (Transform holeTran in holeTrans)
            {
                float distance = Vector2.Distance(holeTran.position, pos);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestHole = holeTran;
                }
            }
            nearestHole.GetComponent<ShipHole>().Broken();
        }
    }
}