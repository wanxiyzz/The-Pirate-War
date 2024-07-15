using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.Inventory;
using MyGame.PlayerSystem;
using Photon.Pun;

namespace MyGame.ItemSystem
{
    public class Barrel : MonoBehaviourPun, Iinteractable, IPunObservable
    {
        public Item[] items;
        public BarrelType barrelType;
        public bool IsInteractable => false;

        public string Feature => "查看木桶";

        public bool IsSimple => isInetractable;

        public bool IsBoard => false;

        private bool isInetractable;
        private SpriteRenderer spriteRenderer;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void EnterInteract(PlayerController playerController)
        {
            spriteRenderer.color = Color.white;
            EventHandler.CallOpenBarrel(items, true, barrelType);
            isInetractable = true;
        }

        public void EnterWaitInteract()
        {
            spriteRenderer.color = Color.green;
        }
        public void ExitInteract()
        {
            isInetractable = false;
            EventHandler.CallOpenBarrel(items, false, barrelType);
            spriteRenderer.color = Color.green;
        }
        public void ExitWaitInteract()
        {
            spriteRenderer.color = Color.white;
        }
        public void InputInteract(Vector2 input)
        {
        }
        [PunRPC]
        public void OnChange()
        {
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
        }
    }
}