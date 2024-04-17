using System.Collections.Generic;
using MyGame.InputSystem;
using UnityEngine;
namespace MyGame.PlayerSystem
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private List<Iinteractable> interactables = new List<Iinteractable>();
        [SerializeField] private Iinteractable currentInteractable;
        private bool isInteractive;
        private void Start()
        {
            GameInput.Instance.InteractAction += Interact;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Iinteractable>(out currentInteractable))
            {
                interactables.Add(currentInteractable);
                currentInteractable.EnterWaitInteract();
            }
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
        }
        private void Interact()
        {
            isInteractive = !isInteractive;
            if (isInteractive)
            {
                if (currentInteractable != null)
                {
                    currentInteractable.EnterInteract();
                    EventHandler.CallPlayerInetractive(true);
                }
            }
            else
            {
                if (currentInteractable != null)
                    currentInteractable.ExitInteract();
                EventHandler.CallPlayerInetractive(false);
            }
        }

    }
}
