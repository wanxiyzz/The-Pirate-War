using System.Collections;
using System.Collections.Generic;
using MyGame.PlayerSystem;
using MyGame.UISystem;
using UnityEngine;

public class WeaponTable : MonoBehaviour, Iinteractable
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform interactTran;
    public bool isInteractable;

    public string Feature => "查看武器桌";
    public bool IsSimple => isInteractable;

    public bool IsInteractable => isInteractable;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void EnterInteract(PlayerController playerController)
    {
        GameManager.Instance.player.PlayerEnterInteract(interactTran);
        UIManager.Instance.OpenWeaponTableUI(true);
        isInteractable = true;
    }

    public void EnterWaitInteract()
    {
        spriteRenderer.color = Color.green;
    }

    public void ExitInteract()
    {
        UIManager.Instance.OpenWeaponTableUI(false);
        isInteractable = false;
    }

    public void ExitWaitInteract()
    {
        spriteRenderer.color = Color.white;
    }

    public void InputInteract(Vector2 input)
    {
    }

}
