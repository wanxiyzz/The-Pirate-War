using UnityEngine;
using MyGame.InputSystem;
using MyGame.UISystem;

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

        public string Feature => "使用船舵";
        public bool IsSimple => false;
        private void Start()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        public void EnterInteract()
        {
            GameInput.Instance.MovementAction += InputInteract;
            UIManager.Instance.EnterHelm(helmRotate);
            GameManager.Instance.player.PlayerEnterInteract(takeHelmPos);
            spriteRenderer.color = Color.white;
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