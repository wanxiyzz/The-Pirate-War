using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MyGame.HandheldableSystem
{
    public class Lantern : Handheldable
    {
        [SerializeField] private Light2D lantern;
        private SpriteRenderer spriteRenderer;
        void Start()
        {
            lantern = GetComponent<Light2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        public void TackOutLantern()
        {
            Debug.Log("TackOutLantern");
            lantern.intensity = 1f;
            spriteRenderer.enabled = true;
        }
        public void PackUpLantern()
        {
            lantern.intensity = 0f;
            spriteRenderer.enabled = false;
        }
        public override void ItemUsed()
        {

        }
    }
}

