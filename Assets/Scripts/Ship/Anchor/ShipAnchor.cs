using System.Collections;
using System.Collections.Generic;
using MyGame.InputSystem;
using MyGame.PlayerSystem;
using UnityEngine;
using Photon.Pun;
namespace MyGame.ShipSystem.Anchor
{
    public class ShipAnchor : MonoBehaviourPun, Iinteractable
    {
        public bool dropAnchor = true;
        [SerializeField] Transform[] anchorPos;
        public bool[] havePeople;
        public string Feature => dropAnchor ? "升起船锚" : "降下船锚";
        public bool IsSimple => !dropAnchor;

        public bool IsInteractable => EmptySpace() < 0;

        public bool IsBoard => true;

        public SpriteRenderer spriteRenderer;
        public int currentNum;//不同步
        public float rotationPercent;
        private float rotationSpeed = 0.3f;
        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            havePeople = new bool[anchorPos.Length];
        }
        private void Update()
        {
            if (dropAnchor)
            {
                if (rotationPercent > 1)
                {
                    photonView.RPC("RPC_DropAnchor", RpcTarget.All, false);
                }
                if (!HavePlayerInetract() && rotationPercent > 0)
                {
                    photonView.RPC("RPC_SetrotationPercent", RpcTarget.All, rotationPercent - rotationSpeed * Time.deltaTime);
                }
                transform.localRotation = Quaternion.Euler(0, 0, rotationPercent * -360);
            }
        }
        [PunRPC]
        public void RPC_SetrotationPercent(float value)
        {
            rotationPercent = value;
        }
        [PunRPC]
        public void RPC_DropAnchor(bool value)
        {
            dropAnchor = value;
        }
        [PunRPC]
        public void RPC_SetHavePlayer(bool[] value)
        {
            havePeople = value;
        }
        public int EmptySpace()
        {
            for (int i = 0; i < havePeople.Length; i++)
            {
                if (!havePeople[i]) return i;
            }
            return -1;
        }
        public bool HavePlayerInetract()
        {
            for (int i = 0; i < havePeople.Length; i++)
            {
                if (havePeople[i]) return true;
            }
            return false;
        }
        public void EnterInteract(PlayerController playerController)
        {
            if (dropAnchor)
            {
                int num = EmptySpace();
                if (num < 0)
                {
                    return;
                }
                spriteRenderer.color = Color.white;
                GameManager.Instance.player.PlayerEnterInteract(anchorPos[num]);
                havePeople[num] = true;
                GameInput.Instance.MovementAction += InputInteract;
                photonView.RPC("RPC_SetHavePlayer", RpcTarget.All, havePeople);
            }
            else
            {
                StartCoroutine(DorpAnchorIE());
                EventHandler.CallEnterPlayerInteract(false);
            }
            spriteRenderer.color = Color.white;
        }
        IEnumerator DorpAnchorIE()
        {
            while (rotationPercent > 0)
            {
                photonView.RPC("RPC_SetrotationPercent", RpcTarget.All, rotationPercent - rotationSpeed * Time.deltaTime);
                transform.localRotation = Quaternion.Euler(0, 0, rotationPercent * -360);
                yield return Setting.waitForFixedUpdate;
            }
            photonView.RPC("RPC_DropAnchor", RpcTarget.All, true);
            //TODO:音效
        }
        public void ExitInteract()
        {
            GameInput.Instance.MovementAction -= InputInteract;
            havePeople[currentNum] = false;
            photonView.RPC("RPC_SetHavePlayer", RpcTarget.All, havePeople);
            spriteRenderer.color = Color.green;
        }

        public void EnterWaitInteract()
        {
            spriteRenderer.color = Color.green;
        }

        public void ExitWaitInteract()
        {
            spriteRenderer.color = Color.white;
        }

        public void InputInteract(Vector2 input)
        {
            if (input.y > 0)
                photonView.RPC("RPC_SetrotationPercent", RpcTarget.All, rotationPercent + rotationSpeed * Time.deltaTime);
            if (rotationPercent > 1) EventHandler.CallEnterPlayerInteract(false);
        }

    }
}