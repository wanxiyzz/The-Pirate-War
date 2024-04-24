using System.Collections;
using System.Collections.Generic;
using MyGame.Inventory;
using UnityEngine;

namespace MyGame.ShipSystem.Hole
{
    public class ShipHole : MonoBehaviour, Iinteractable
    {
        public int brokenLevel = 0;
        private SpriteRenderer spriteRenderer;
        public string Feature => "修复船洞";
        public bool IsSimple => false;
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
        }
        public void EnterInteract()
        {
            if (PlayerBag.Instance.UseBoard())
            {

            }
            else
            {
                //TODO: 需要木板
            }

        }

        public void EnterWaitInteract()
        {
        }

        public void ExitInteract()
        {
        }

        public void ExitWaitInteract()
        {
        }

        public void InputInteract(Vector2 input)
        {
        }
        public bool Broken()
        {
            if (brokenLevel < 2)
            {
                brokenLevel++;
                spriteRenderer.enabled = true;
                return true;
            }
            return false;
        }
        public void FixHole()
        {
            brokenLevel = 0;
            spriteRenderer.enabled = false;
        }
    }
}