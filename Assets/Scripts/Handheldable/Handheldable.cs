using System.Collections;
using System.Collections.Generic;
using MyGame.InputSystem;
using UnityEngine;
namespace MyGame.HandheldableSystem
{
    public abstract class Handheldable : MonoBehaviour
    {
        protected Vector2 mousePos;
        protected bool canUsed;
        public float bufferTime = 0;
        public float currentBufferTime = 0;
        protected virtual void OnEnable()
        {
            GameInput.Instance.UseItemAction += ItemUsed;
        }
        protected virtual void OnDisable()
        {
            try
            {
                GameInput.Instance.UseItemAction -= ItemUsed;
            }
            catch { }
        }
        protected void Update()
        {
            Aim();
        }
        protected virtual void Aim()
        {
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