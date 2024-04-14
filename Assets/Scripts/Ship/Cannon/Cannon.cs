using System.Collections;
using System.Collections.Generic;
using MyGame.PlayerSystem;
using UnityEngine;
namespace MyGame.ShipSystem.Cannon
{
    public class Cannon : MonoBehaviour, Iinteractable
    {
        [SerializeField] float rotateSpeed;
        [SerializeField] float steerAngle;
        [SerializeField] float maxSteerAngle;


        [SerializeField] Transform interactPoint;
        [SerializeField] Transform firePoint;
        [SerializeField] float connonballSpeed;
        public void EnterInteract()
        {
            GameInput.Instance.MovementAction += InputInteract;
            GameManager.Instance.player.PlayerEnterInteract(interactPoint);
            CameraManager.Instance.ChangeCameraOffset(0.5f, 0.8f);
        }

        public void EnterWaitInteract()
        {
        }

        public void ExitInteract()
        {
            GameInput.Instance.MovementAction -= InputInteract;
            CameraManager.Instance.ResetCamera();
            CameraManager.Instance.ChangeCameraOffset(0.5f, 0.5f);
        }

        public void ExitWaitInteract()
        {
        }

        public void InputInteract(Vector2 input)
        {
            Debug.Log(interactPoint.rotation.z);
            CameraManager.Instance.RotateCamera(interactPoint.rotation);
        }
    }
}
