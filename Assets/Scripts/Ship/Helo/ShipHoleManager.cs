using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.ShipSystem
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
            Debug.Log("nearestHole" + nearestHole.name);
        }
    }
}