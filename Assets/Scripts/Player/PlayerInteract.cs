using System.Collections.Generic;
using MyGame.InputSystem;
using MyGame.UISystem;
using UnityEngine;
namespace MyGame.PlayerSystem
{
    public class PlayerInteract : MonoBehaviour
    {
        private List<Iinteractable> interactables = new List<Iinteractable>();
        private Iinteractable currentInteractable;
        private bool isInteractive;

        private void OnEnable()
        {
            GameInput.Instance.InteractAction += Interact;
            EventHandler.EnterPlayerInteract += OnEnterPlayerInteract;
        }
        private void OnDisable()
        {
            GameInput.Instance.InteractAction -= Interact;
            EventHandler.EnterPlayerInteract -= OnEnterPlayerInteract;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Iinteractable>(out currentInteractable))
            {
                interactables.Add(currentInteractable);
                currentInteractable.EnterWaitInteract();
            }
            UIManager.Instance.InteractTips(currentInteractable);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            Iinteractable interactable;
            if (other.TryGetComponent<Iinteractable>(out interactable))
            {
                interactables.Remove(interactable);
                if (interactable == currentInteractable)
                {
                    currentInteractable.ExitWaitInteract();
                    if (interactables.Count > 0)
                    {
                        currentInteractable = interactables[interactables.Count - 1];
                        currentInteractable.EnterWaitInteract();
                    }
                    else currentInteractable = null;
                }
            }
            UIManager.Instance.InteractTips(currentInteractable);
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
            if (currentInteractable != null)
            {
                currentInteractable.EnterInteract();
                EventHandler.CallPlayerInetractive(true);
                UIManager.Instance.InteractTips(null);
                isInteractive = true;
            }
        }
        public void ExitInteract()
        {
            if (currentInteractable != null)
                currentInteractable.ExitInteract();
            UIManager.Instance.InteractTips(currentInteractable);
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
