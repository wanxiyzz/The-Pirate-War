using System;
using System.Collections;
using System.Collections.Generic;
using MyGame.InputSystem;
using MyGame.PlayerSystem;
using UnityEngine;

public class BackPlayerCannon : MonoBehaviour, Iinteractable
{
    public bool IsInteractable => false;

    public string Feature => "回到船上";

    public bool IsSimple => true;

    public bool IsBoard => false;

    private PlayerController player;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void EnterInteract(PlayerController playerController)
    {
        Debug.Log("进入交互");
        GameManager.Instance.player.PlayerEnterInteract(transform);
        playerController.EnterCannon(transform, out player);
        Fire();
    }

    private void Fire()
    {
        player.FirePlayerBackShip();
        ExitInteract();
    }

    public void EnterWaitInteract()
    {
        spriteRenderer.color = Color.green;
    }

    public void ExitInteract()
    {
        spriteRenderer.color = Color.green;
    }

    public void ExitWaitInteract()
    {
        spriteRenderer.color = Color.white;
    }

    public void InputInteract(Vector2 input)
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
