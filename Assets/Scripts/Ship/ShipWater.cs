using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.ShipSystem
{
    public class ShipWater : MonoBehaviour
    {
        private SpriteRenderer waterRenderer;
        private ShipTakeWater shipTakeWater;
        private Vector3 bigWater = new Vector3(2, 2, 1);
        private void Awake()
        {
            waterRenderer = GetComponent<SpriteRenderer>();
            shipTakeWater = GetComponentInParent<ShipTakeWater>();
        }
        void Start()
        {
            waterRenderer.color = new Color(1, 1, 1, 0);
        }
        void Update()
        {

            if (shipTakeWater.waterValue < 0.4f)
            {
                waterRenderer.sortingLayerName = "middleItem";
                waterRenderer.transform.localScale = Vector3.one;
                waterRenderer.sortingOrder = 0;
                waterRenderer.color = new Color(1, 1, 1, shipTakeWater.waterValue);
            }
            else if (shipTakeWater.waterValue < 0.8f)
            {
                waterRenderer.sortingLayerName = "middleItem";
                waterRenderer.sortingOrder = 1;
                waterRenderer.transform.localScale = Vector3.one;
                waterRenderer.color = new Color(1, 1, 1, shipTakeWater.waterValue);
            }
            else
            {
                waterRenderer.color = new Color(1, 1, 1, (shipTakeWater.waterValue - 0.8f) / 0.2f);
                waterRenderer.sortingLayerName = "topItem";
                waterRenderer.transform.localScale = bigWater;
            }
        }
    }
}