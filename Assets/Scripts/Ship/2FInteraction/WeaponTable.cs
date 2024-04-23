using System.Collections;
using System.Collections.Generic;
using MyGame.UISystem;
using UnityEngine;

public class WeaponTable : MonoBehaviour, Iinteractable
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform interactTran;
    public string Feature => "查看武器桌";
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void EnterInteract()
    {
        GameManager.Instance.player.PlayerEnterInteract(interactTran);
        UIManager.Instance.OpenWeaponTableUI(true);
    }

    public void EnterWaitInteract()
    {
        spriteRenderer.color = Color.green;
    }

    public void ExitInteract()
    {
        UIManager.Instance.OpenWeaponTableUI(false);
    }

    public void ExitWaitInteract()
    {
        spriteRenderer.color = Color.white;
    }

    public void InputInteract(Vector2 input)
    {
    }

}
