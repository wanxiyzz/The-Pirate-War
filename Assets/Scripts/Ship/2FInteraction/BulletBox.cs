using System.Collections;
using System.Collections.Generic;
using MyGame.PlayerSystem;
using UnityEngine;

public class BulletBox : MonoBehaviour, Iinteractable
{
    public string Feature => "补满子弹";

    public bool IsSimple => true;

    public bool IsInteractable => false;

    public bool IsBoard => true;

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void EnterInteract(PlayerController playerController)
    {
        GameManager.Instance.player.playerWeapon.AddBullet();
        EventHandler.CallEnterPlayerInteract(false);
    }

    public void EnterWaitInteract()
    {
        spriteRenderer.color = Color.green;
    }

    public void ExitInteract()
    {
    }

    public void ExitWaitInteract()
    {
        spriteRenderer.color = Color.blue;
    }

    public void InputInteract(Vector2 input)
    {

    }
}
