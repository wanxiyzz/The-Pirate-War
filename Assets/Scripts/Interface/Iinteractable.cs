using MyGame.PlayerSystem;
using UnityEngine;
public interface Iinteractable
{
    public bool IsInteractable { get; }
    public string Feature { get; }
    public bool IsSimple { get; }
    public void EnterInteract(PlayerController playerController);
    public void ExitInteract();
    public void EnterWaitInteract();
    public void ExitWaitInteract();
    public void InputInteract(Vector2 input);
}