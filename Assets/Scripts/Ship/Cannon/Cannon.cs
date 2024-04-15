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


        [SerializeField] Transform interactPoint;
        [SerializeField] Transform firePoint;
        [SerializeField] float cannonballSpeed;


        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] bool haveBall = true;
        private void Awake()
        {
            defaultRotaton = transform.localRotation.eulerAngles;
        }
        public void EnterInteract()
        {
            GameInput.Instance.MovementAction += InputInteract;
            GameInput.Instance.AttackAction += Fire;
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
            GameInput.Instance.AttackAction -= Fire;
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
            CameraManager.Instance.RotateCamera(interactPoint.rotation);
            steerAngle += input.x * Time.deltaTime * rotateSpeed;
            steerAngle = Mathf.Clamp(steerAngle, -maxSteerAngle, maxSteerAngle);
            transform.localRotation = Quaternion.Euler(defaultRotaton - new Vector3(0, 0, steerAngle));
        }
        public void Fire()
        {
            if (haveBall)
            {
                Vector2 velocity = (firePoint.position - transform.parent.position).normalized * cannonballSpeed;
                CannonballPool.Instance.GetCannonball(velocity, firePoint.position);
                // haveBall = false;
            }
        }
    }
}
