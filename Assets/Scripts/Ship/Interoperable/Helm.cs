using UnityEngine;
using MyGame.PlayerSystem;
namespace MyGame.ShipSystem
{
    public class Helm : MonoBehaviour, Iinteractable
    {
        [SerializeField] Transform takeHelmPos;
        [SerializeField] ShipController shipController;
        [SerializeField] private float helmRotate = 0;
        [SerializeField] private float helmRotateSpeed = 0.5f;
        private float helmRotateSpeedMax = 1;
        public void EnterInteract()
        {
            GameInput.Instance.MovementAction += InputInteract;
            UIManager.Instance.EnterHelm(helmRotate);
            GameManager.Instance.player.PlayerEnterInteract(takeHelmPos);
        }

        public void EnterWaitInteract()
        {
        }

        public void ExitInteract()
        {
            UIManager.Instance.ExitHelm();
            GameInput.Instance.MovementAction -= InputInteract;
        }

        public void ExitWaitInteract()
        {
        }

        public void InputInteract(Vector2 input)
        {
            helmRotate -= input.x * helmRotateSpeed * Time.deltaTime;
            helmRotate = Mathf.Clamp(helmRotate, -helmRotateSpeedMax, helmRotateSpeedMax);
            shipController.AdjustAngular(helmRotate);
            UIManager.Instance.UpdateHeml(helmRotate);

        }
    }
}