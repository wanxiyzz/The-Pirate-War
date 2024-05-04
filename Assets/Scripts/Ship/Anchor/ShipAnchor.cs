using System.Collections;
using System.Collections.Generic;
using MyGame.InputSystem;
using MyGame.PlayerSystem;
using UnityEngine;
namespace MyGame.ShipSystem.Anchor
{
    public class ShipAnchor : MonoBehaviour, Iinteractable
    {
        public bool dropAnchor = true;
        [SerializeField] Transform[] anchorPos;
        public bool[] havePeople;
        public string Feature => dropAnchor ? "升起船锚" : "降下船锚";
        public bool IsSimple => !dropAnchor;

        public bool IsInteractable => EmptySpace() < 0;

        public SpriteRenderer spriteRenderer;
        public int currentNum;//不同步
        [SerializeField] float rotationPercent;
        private float rotationSpeed = 0.3f;
        private float dropSpeed = 0.5f;
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
                    dropAnchor = false;
                }
                if (!HavePlayerInetract() && rotationPercent > 0)
                {
                    rotationPercent -= rotationSpeed * Time.deltaTime;
                }
                transform.localRotation = Quaternion.Euler(0, 0, rotationPercent * -360);
            }
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
                    //TODO:人满了
                    return;
                }
                spriteRenderer.color = Color.white;
                GameManager.Instance.player.PlayerEnterInteract(anchorPos[num]);
                havePeople[num] = true;
                GameInput.Instance.MovementAction += InputInteract;
                currentNum = num;
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
                rotationPercent -= dropSpeed * Time.deltaTime;
                transform.localRotation = Quaternion.Euler(0, 0, rotationPercent * -360);
                yield return null;
            }
            dropAnchor = true;
            //TODO:音效
        }
        public void ExitInteract()
        {
            GameInput.Instance.MovementAction -= InputInteract;
            havePeople[currentNum] = false;
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
                rotationPercent += input.y * rotationSpeed * Time.deltaTime;
            if (rotationPercent > 1) EventHandler.CallEnterPlayerInteract(false);
        }

    }
}