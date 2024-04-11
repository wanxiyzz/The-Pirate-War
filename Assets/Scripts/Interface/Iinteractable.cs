using UnityEngine;
public interface Iinteractable
{
    public void EnterInteract();
    public void ExitInteract();
    public void EnterWaitInteract();
    public void ExitrWaitInteract();
    public void InputInteract(Vector2 input);
}