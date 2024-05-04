using UnityEngine;
using MyGame.InputSystem;
using MyGame.UISystem;
using MyGame.PlayerSystem;

namespace MyGame.ShipSystem
{
    public class Helm : MonoBehaviour, Iinteractable
    {
        [SerializeField] Transform takeHelmPos;
        [SerializeField] ShipController shipController;
        [SerializeField] private float helmRotate = 0;
        [SerializeField] private float helmRotateSpeed = 0.5f;
        private SpriteRenderer spriteRenderer;
        private float helmRotateSpeedMax = 1;
        private bool isInteractable;

        public string Feature => "使用船舵";
        public bool IsSimple => isInteractable;

        public bool IsInteractable => isInteractable;

        private void Start()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        public void EnterInteract(PlayerController playerController)
        {
            GameInput.Instance.MovementAction += InputInteract;
            UIManager.Instance.EnterHelm(helmRotate);
            GameManager.Instance.player.PlayerEnterInteract(takeHelmPos);
            spriteRenderer.color = Color.white;
            isInteractable = true;
        }

        public void EnterWaitInteract()
        {
            spriteRenderer.color = Color.green;
        }

        public void ExitInteract()
        {
            UIManager.Instance.ExitHelm();
            GameInput.Instance.MovementAction -= InputInteract;
            spriteRenderer.color = Color.green;
            isInteractable = false;
        }

        public void ExitWaitInteract()
        {
            spriteRenderer.color = Color.white;
        }

        public void InputInteract(Vector2 input)
        {
            helmRotate -= input.x * helmRotateSpeed * Time.deltaTime;
            helmRotate = Mathf.Clamp(helmRotate, -helmRotateSpeedMax, helmRotateSpeedMax);
            if (helmRotate < 0.05f && helmRotate > -0.05f)
            {
                UIManager.Instance.UpdateHeml(0);
                shipController.AdjustAngular(0);
            }
            else
            {
                shipController.AdjustAngular(helmRotate);
                UIManager.Instance.UpdateHeml(helmRotate);
            }
        }
    }
}