using System;
using System.Collections;
using MyGame.InputSystem;
using MyGame.Inventory;
using MyGame.PlayerSystem;
using MyGame.UISystem;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace MyGame.ShipSystem.Cannon
{
    public class Cannon : MonoBehaviourPun, Iinteractable, IPunObservable
    {
        private Vector3 defaultRotaton;
        [SerializeField] float rotateSpeed;
        [SerializeField] float steerAngle;
        [SerializeField] float maxSteerAngle;
        [SerializeField] Transform cannonBody;
        [SerializeField] Transform interactPoint;
        [SerializeField] Transform firePoint;


        [SerializeField] SpriteRenderer spriteRenderer;
        public bool haveBall = false;
        [SerializeField] ParticleSystem grey;


        private Animator animator;
        public bool havePlayer = false;
        public string Feature => "使用船炮";
        public bool IsSimple => false;
        public bool isInteractable;
        public bool IsInteractable => isInteractable;

        public bool IsBoard => true;

        private PlayerController player;
        private float currentLoadTime;//Nop
        private bool isLoading;//Nop
        private Coroutine loadingCoroutine;
        private void Onperformed(InputAction.CallbackContext context)
        {
            if (context.interaction is HoldInteraction)
            {
                if (isLoading) return;
                if (!havePlayer && !haveBall)
                {
                    GameManager.Instance.player.EnterCannon(transform, out player);
                    UIManager.Instance.Tips(true, "长按 F 退出大炮");
                    photonView.RPC("PunHavePlayertrue", RpcTarget.All);
                }
                else
                {
                    UIManager.Instance.TackWarningUI("看看炮筒 小伙子");
                    UIManager.Instance.Tips(true, "长按 F 钻入大炮");
                    GameManager.Instance.player.ExitCannon(interactPoint);
                    photonView.RPC("PunHavePlayerfalse", RpcTarget.All);
                }
            }
        }
        private void OnDisable()
        {

        }
        private void Awake()
        {
            defaultRotaton = cannonBody.localRotation.eulerAngles;
            animator = GetComponent<Animator>();
        }
        public void EnterInteract(PlayerController playerController)
        {
            player = playerController;
            GameInput.Instance.playerInputActions.Player.Interact.performed += Onperformed;
            GameInput.Instance.playerInputActions.Player.Loading.performed += OnLoading;
            GameInput.Instance.MovementAction += InputInteract;
            GameInput.Instance.UseItemAction += PunCannonFire;
            GameManager.Instance.player.PlayerEnterInteract(interactPoint);
            spriteRenderer.sprite = ItemManager.Instance.defaultCannonSprite;
            CameraManager.Instance.ChangeCameraOffset(0.5f, 0.8f);
            UIManager.Instance.Tips(true, "长按 F 钻入大炮");
            photonView.RPC("IsInteractableTrue", RpcTarget.All);
        }

        private void OnLoading(InputAction.CallbackContext context)
        {
            if (!haveBall && InventoryManager.Instance.HaveCannonBall)
            {
                UIManager.Instance.OpenProgressBar("装填中...");
                loadingCoroutine = StartCoroutine(LoadingIE());
            }
        }
        IEnumerator LoadingIE()
        {
            isLoading = true;
            while (currentLoadTime < Setting.laodingTime)
            {
                currentLoadTime += Time.deltaTime;
                UIManager.Instance.UpdateProgressBar(currentLoadTime / Setting.laodingTime);
                yield return null;
            }
            isLoading = false;
            currentLoadTime = 0;
            photonView.RPC("ReloadBall", RpcTarget.All);
            InventoryManager.Instance.UseCannonball();
            UIManager.Instance.CloseProgressBar();
        }

        public void EnterWaitInteract()
        {
            spriteRenderer.sprite = ItemManager.Instance.selectCannonSprite;
        }

        public void ExitInteract()
        {
            GameInput.Instance.playerInputActions.Player.Interact.performed -= Onperformed;
            GameInput.Instance.MovementAction -= InputInteract;
            GameInput.Instance.UseItemAction -= PunCannonFire;
            CameraManager.Instance.ResetCamera();
            CameraManager.Instance.ChangeCameraOffset(0.5f, 0.5f);
            spriteRenderer.sprite = ItemManager.Instance.selectCannonSprite;
            UIManager.Instance.Tips(false, "");
            photonView.RPC("IsInteractableFalse", RpcTarget.All);
            if (loadingCoroutine != null)
            {
                StopCoroutine(loadingCoroutine);
                currentLoadTime = 0;
                UIManager.Instance.CloseProgressBar();
                isLoading = false;
            }
            player = null;
        }

        public void ExitWaitInteract()
        {
            spriteRenderer.sprite = ItemManager.Instance.defaultCannonSprite;
        }

        public void InputInteract(Vector2 input)
        {
            CameraManager.Instance.RotateCamera(transform.rotation);
            steerAngle += input.x * Time.deltaTime * rotateSpeed;
            steerAngle = Mathf.Clamp(steerAngle, -maxSteerAngle, maxSteerAngle);
            photonView.RPC("PunSteerAngle", RpcTarget.All, steerAngle);
            cannonBody.localRotation = Quaternion.Euler(defaultRotaton - new Vector3(0f, 0f, steerAngle));
        }
        public void PunCannonFire()
        {
            photonView.RPC("CannonFire", RpcTarget.All);
        }
        [PunRPC]
        public void ReloadBall()
        {
            haveBall = true;
        }
        [PunRPC]
        public void PunHavePlayertrue()
        {
            this.havePlayer = true;
        }
        [PunRPC]
        public void PunHavePlayerfalse()
        {
            this.havePlayer = false;
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
        public void CannonFire()
        {
            Debug.Log("火炮发射");
            if (isLoading) return;
            if (havePlayer)
            {
                if (player == GameManager.Instance.player)
                {
                    player.FirePlayer(firePoint.position, (firePoint.position - cannonBody.position).normalized);
                    UIManager.Instance.Tips(false, string.Empty);
                    havePlayer = false;
                    player = null;
                }
                return;
            }
            if (haveBall)
            {
                CameraManager.Instance.CameraShake(0.2f, 2f);
                animator.Play("Fire");
                grey.Play();
                Vector2 velocity = (firePoint.position - cannonBody.position).normalized * Setting.cannonballSpeed;
                CannonballPool.Instance.GetCannonball(velocity, firePoint.position);
                haveBall = false;
            }
            else
            {
                if (player != null && player == GameManager.Instance.player)
                    UIManager.Instance.TackWarningUI("没有炮弹");
            }
        }
        [PunRPC]
        public void PunSteerAngle(float angle)
        {
            steerAngle = angle;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
        }
    }
}
