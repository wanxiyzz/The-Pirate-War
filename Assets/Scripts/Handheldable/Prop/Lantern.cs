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
            lantern.intensity = 1f;
            spriteRenderer.enabled = true;
        }
        public override void ItemUsed()
        {

        }
    }
}

