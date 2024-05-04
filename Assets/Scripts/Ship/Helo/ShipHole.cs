using System.Collections;
using System.Collections.Generic;
using MyGame.Inventory;
using MyGame.PlayerSystem;
using MyGame.UISystem;
using Photon.Pun;
using UnityEngine;

namespace MyGame.ShipSystem.Hole
{
    public class ShipHole : MonoBehaviourPun, Iinteractable
    {
        public int brokenLevel = 0;
        private SpriteRenderer spriteRenderer;

        public string Feature => "修复船洞";
        public bool IsSimple => !InventoryManager.Instance.HaveBoard;

        private bool isInteractable;
        public bool IsInteractable => isInteractable;

        public bool IsBoard => true;

        public float currentFixTime;
        public Coroutine currentFixCoroutine;
        public BoxCollider2D boxCollider2D;

        private void Awake()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            boxCollider2D.enabled = false;
        }
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
        }
        public void EnterInteract(PlayerController playerController)
        {
            if (InventoryManager.Instance.HaveBoard)
            {
                currentFixCoroutine = StartCoroutine(FixHoleIE());
                UIManager.Instance.OpenProgressBar("修理中...");
                //AUDIO:修理
            }
            else
            {
                EventHandler.CallEnterPlayerInteract(false);
            }

        }
        IEnumerator FixHoleIE()
        {
            float fixTime = brokenLevel * Setting.fixShipTime;
            while (currentFixTime < fixTime)
            {
                currentFixTime += Time.deltaTime;
                UIManager.Instance.UpdateProgressBar(currentFixTime / fixTime);
                yield return null;
            }
            FixHole();
            InventoryManager.Instance.UseBoard();
            UIManager.Instance.CloseProgressBar();
            currentFixTime = 0;
            EventHandler.CallEnterPlayerInteract(false);
        }

        public void EnterWaitInteract()
        {
        }

        public void ExitInteract()
        {
            if (currentFixCoroutine != null)
            {
                StopCoroutine(currentFixCoroutine);
                currentFixTime = 0;
                UIManager.Instance.CloseProgressBar();
            }
        }

        public void ExitWaitInteract()
        {
        }

        public void InputInteract(Vector2 input)
        {
        }
        public void BrokenPun()
        {
            if (photonView.IsMine)
                photonView.RPC("Broken", RpcTarget.Others);
        }
        [PunRPC]
        public bool Broken()
        {
            if (brokenLevel < 2)
            {
                brokenLevel++;
                spriteRenderer.enabled = true;
                if (GameManager.Instance.player.playerPos == PlayerPos.Ship2F)
                    boxCollider2D.enabled = true;
                return true;
            }
            return false;
        }
        public void FixHolePun()
        {
            photonView.RPC("FixHole", RpcTarget.Others);
        }
        [PunRPC]
        public void FixHole()
        {
            brokenLevel = 0;
            boxCollider2D.enabled = false;
            spriteRenderer.enabled = false;
        }
    }
}