using System.Collections;
using System.Collections.Generic;
using MyGame.InputSystem;
using Photon.Pun;
using UnityEngine;
namespace MyGame.HandheldableSystem
{
    public abstract class Handheldable : MonoBehaviourPun
    {
        protected Vector2 mousePos;
        protected bool canUsed;
        public float bufferTime = 0;
        public float currentBufferTime = 0;
        protected virtual void OnEnable()
        {
            if (photonView.IsMine)
                GameInput.Instance.UseItemAction += ItemUsedPun;
        }
        protected virtual void OnDisable()
        {
            if (photonView.IsMine)
            {
                try
                {
                    GameInput.Instance.UseItemAction -= ItemUsedPun;
                }
                catch { }
            }
        }
        protected void Update()
        {
            if (photonView.IsMine)
                Aim();
            else
            {
                canUsed = true;
            }
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
        public virtual void ItemUsedPun()
        {
            photonView.RPC("ItemUsed", RpcTarget.All);
        }
        [PunRPC]
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