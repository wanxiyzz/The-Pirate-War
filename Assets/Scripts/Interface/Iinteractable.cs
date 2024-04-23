using UnityEngine;
public interface Iinteractable
{
    public string Feature { get; }
    public void EnterInteract();
    public void ExitInteract();
    public void EnterWaitInteract();
    public void ExitWaitInteract();
    public void InputInteract(Vector2 input);
}