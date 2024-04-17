using System.Collections;
using System.Collections.Generic;
using MyGame.InputSystem;
using UnityEngine;
namespace MyGame.HandheldableSystem
{
    public abstract class Handheldable : MonoBehaviour
    {
        protected Vector2 mousePos;

        protected Vector2 direction;
        protected bool canUsed;
        public float bufferTime = 0;
        public float currentBufferTime = 0;
        protected void OnEnable()
        {
            GameInput.Instance.UseItemAction += ItemUsed;
        }
        protected void OnDisable()
        {
            GameInput.Instance.UseItemAction -= ItemUsed;
        }
        protected void Update()
        {
            Aim();
        }
        protected virtual void Aim()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePos - (Vector2)transform.position).normalized;
            transform.right = direction;
            if (!canUsed)
            {
                currentBufferTime -= Time.deltaTime;
                if (currentBufferTime < 0)
                {
                    canUsed = true;
                }
            }
        }
        public virtual void ItemUsed()
        {
            if (canUsed)
            {
                currentBufferTime = bufferTime;
                canUsed = false;
            }
        }
    }
}