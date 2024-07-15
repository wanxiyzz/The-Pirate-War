using System.Collections.Generic;
using MyGame.InputSystem;
using MyGame.UISystem;
using Photon.Pun;
using UnityEngine;
namespace MyGame.PlayerSystem
{
    public class PlayerInteract : MonoBehaviourPun
    {
        private List<Iinteractable> interactables = new List<Iinteractable>();
        [SerializeField] private Iinteractable currentInteractable;
        [SerializeField] private Iinteractable currentWaitInteractable;
        private bool isInteractive;
        [SerializeField] PlayerController playerController;
        private void OnEnable()
        {
            if (photonView.IsMine)
            {
                GameInput.Instance.InteractAction += Interact;
                EventHandler.EnterPlayerInteract += OnEnterPlayerInteract;
            }
            else
            {
                GetComponent<CircleCollider2D>().enabled = false;
            }
        }
        private void OnDisable()
        {
            if (photonView.IsMine)
            {
                GameInput.Instance.InteractAction -= Interact;
                EventHandler.EnterPlayerInteract -= OnEnterPlayerInteract;
            }
        }
        private void Update()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                if (interactables.Count > 1)
                {
                    currentWaitInteractable.ExitWaitInteract();
                    Debug.Log(interactables.Count);
                    currentWaitInteractable = interactables[interactables.Count - 2];
                    currentWaitInteractable.EnterWaitInteract();
                    UIManager.Instance.InteractTips(currentWaitInteractable);
                }
            }
        }
        public void SelectLastItem()
        {
            if (interactables.Count > 1)
            {
                currentWaitInteractable.ExitWaitInteract();
                Debug.Log(interactables.Count);
                currentWaitInteractable = interactables[interactables.Count - 2];
                currentWaitInteractable.EnterWaitInteract();
                UIManager.Instance.InteractTips(currentWaitInteractable);
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (currentWaitInteractable != null)
            {
                currentWaitInteractable.ExitWaitInteract();
            }
            if (other.TryGetComponent<Iinteractable>(out currentWaitInteractable))
            {
                interactables.Add(currentWaitInteractable);
                currentWaitInteractable.EnterWaitInteract();
            }
            UIManager.Instance.InteractTips(currentWaitInteractable);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            Iinteractable interactable;
            if (other.TryGetComponent<Iinteractable>(out interactable))
            {
                interactables.Remove(interactable);
                if (interactable == currentWaitInteractable)
                {
                    currentWaitInteractable.ExitWaitInteract();
                    if (interactables.Count > 0)
                    {
                        currentWaitInteractable = interactables[interactables.Count - 1];
                        currentWaitInteractable.EnterWaitInteract();
                    }
                    else currentWaitInteractable = null;
                }
                if (interactable == currentInteractable)
                {
                    currentInteractable.ExitInteract();
                    currentInteractable.ExitWaitInteract();
                    currentInteractable = null;
                }
            }
            UIManager.Instance.InteractTips(currentWaitInteractable);
        }
        private void Interact()
        {
            if (!isInteractive)
            {
                EnterInetract();
            }
            else
            {
                ExitInteract();
            }
        }
        private void EnterInetract()
        {
            Debug.Log("进入交互");
            if (currentWaitInteractable != null)
            {
                if (currentWaitInteractable.IsBoard && !playerController.isBoardShip) return;
                if (currentWaitInteractable.IsInteractable) return;
                if (currentWaitInteractable.IsSimple)
                {
                    currentInteractable = currentWaitInteractable;
                    currentInteractable.EnterInteract(playerController);
                    return;
                }
                currentInteractable = currentWaitInteractable;
                currentInteractable.EnterInteract(playerController);
                EventHandler.CallPlayerInetractive(true);
                UIManager.Instance.InteractTips(null);
                isInteractive = true;
            }
        }
        public void ExitInteract()
        {
            Debug.Log("退出交互");
            if (currentInteractable != null)
                currentInteractable.ExitInteract();
            UIManager.Instance.InteractTips(currentWaitInteractable);
            EventHandler.CallPlayerInetractive(false);
            isInteractive = false;
        }

        private void OnEnterPlayerInteract(bool obj)
        {
            if (obj) EnterInetract();
            else ExitInteract();
        }

    }
}
