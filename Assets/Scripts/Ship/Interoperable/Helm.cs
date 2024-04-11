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
            Debug.Log("交互了");
            UIManager.Instance.EnterHelm(helmRotate);
            GameManager.Instance.player.PlayerEnterInteract(takeHelmPos);
        }

        public void EnterWaitInteract()
        {
            Debug.Log("进入了");
        }

        public void ExitInteract()
        {
            Debug.Log("退出交互了");
            GameInput.Instance.MovementAction -= InputInteract;
        }

        public void ExitrWaitInteract()
        {
            Debug.Log("退出了");
        }

        public void InputInteract(Vector2 input)
        {
            helmRotate -= input.x * helmRotateSpeed * Time.deltaTime;
            helmRotate = Mathf.Clamp(helmRotate, -helmRotateSpeedMax, helmRotateSpeedMax);
            shipController.AdjustTorque(helmRotate);
            UIManager.Instance.UpdateHeml(helmRotate);

        }
    }
}