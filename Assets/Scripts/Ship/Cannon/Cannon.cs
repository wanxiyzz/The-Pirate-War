using System.Collections;
using MyGame.InputSystem;
using UnityEngine;
namespace MyGame.ShipSystem.Cannon
{
    public class Cannon : MonoBehaviour, Iinteractable
    {
        private Vector3 defaultRotaton;
        [SerializeField] float rotateSpeed;
        [SerializeField] float steerAngle;
        [SerializeField] float maxSteerAngle;

        [SerializeField] Transform cannonBody;
        [SerializeField] Transform interactPoint;
        [SerializeField] Transform firePoint;
        [SerializeField] float cannonballSpeed;


        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] bool haveBall = true;
        [SerializeField] ParticleSystem grey;


        private Animator animator;

        public string Name => "船炮";

        private void Awake()
        {
            defaultRotaton = cannonBody.localRotation.eulerAngles;
            animator = GetComponent<Animator>();
        }
        public void EnterInteract()
        {
            GameInput.Instance.MovementAction += InputInteract;
            GameInput.Instance.UseItemAction += Fire;
            GameManager.Instance.player.PlayerEnterInteract(interactPoint);
            spriteRenderer.sprite = ItemManager.Instance.defaultCannonSprite;
            CameraManager.Instance.ChangeCameraOffset(0.5f, 0.8f);
        }

        public void EnterWaitInteract()
        {
            spriteRenderer.sprite = ItemManager.Instance.selectCannonSprite;
        }

        public void ExitInteract()
        {
            GameInput.Instance.MovementAction -= InputInteract;
            GameInput.Instance.UseItemAction -= Fire;
            CameraManager.Instance.ResetCamera();
            CameraManager.Instance.ChangeCameraOffset(0.5f, 0.5f);
            spriteRenderer.sprite = ItemManager.Instance.selectCannonSprite;
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
            cannonBody.localRotation = Quaternion.Euler(defaultRotaton - new Vector3(0f, 0f, steerAngle));
        }
        public void Fire()
        {
            if (haveBall)
            {
                CameraManager.Instance.CameraShake(0.2f, 2f);
                animator.Play("Fire");
                grey.Play();
                Vector2 velocity = (firePoint.position - cannonBody.position).normalized * cannonballSpeed;
                CannonballPool.Instance.GetCannonball(velocity, firePoint.position);
                // haveBall = false;
            }
        }
    }
}
