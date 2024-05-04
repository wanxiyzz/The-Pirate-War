using UnityEngine;
using MyGame.InputSystem;
using MyGame.UISystem;
using MyGame.PlayerSystem;
using Photon.Pun;
namespace MyGame.ShipSystem
{
    public class Helm : MonoBehaviourPun, Iinteractable
    {
        [SerializeField] Transform takeHelmPos;
        [SerializeField] ShipController shipController;
        public float helmRotate = 0;
        [SerializeField] private float helmRotateSpeed = 0.5f;
        private SpriteRenderer spriteRenderer;
        private float helmRotateSpeedMax = 1;
        public bool isInteractable;

        public string Feature => "使用船舵";
        public bool IsSimple => isInteractable;

        public bool IsInteractable => isInteractable;

        public bool IsBoard => true;

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
            photonView.RPC("IsInteractableTrue", RpcTarget.All);
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
            photonView.RPC("IsInteractableFalse", RpcTarget.All);
        }

        public void ExitWaitInteract()
        {
            spriteRenderer.color = Color.white;
        }

        public void InputInteract(Vector2 input)
        {
            helmRotate -= input.x * helmRotateSpeed * Time.deltaTime;
            helmRotate = Mathf.Clamp(helmRotate, -helmRotateSpeedMax, helmRotateSpeedMax);
            photonView.RPC("PunHelmRotate", RpcTarget.All, helmRotate);
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
        [PunRPC]
        public void IsInteractableTrue()
        {
            this.isInteractable = true;
        }
        [PunRPC]
        public void IsInteractableFalse()
        {
            this.isInteractable = false;
        }
        [PunRPC]
        public void PunHelmRotate(float helmRotate)
        {
            this.helmRotate = helmRotate;
        }

    }
}