using System.Collections;
using System.Collections.Generic;
using MyGame.PlayerSystem;
using MyGame.UISystem;
using UnityEngine;
using Photon.Pun;
public class WeaponTable : MonoBehaviourPun, Iinteractable, IPunObservable
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform interactTran;
    public bool isInteractable;

    public string Feature => "查看武器桌";
    public bool IsSimple => isInteractable;

    public bool IsInteractable => isInteractable;

    public bool IsBoard => true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void EnterInteract(PlayerController playerController)
    {
        GameManager.Instance.player.PlayerEnterInteract(interactTran);
        UIManager.Instance.OpenWeaponTableUI(true);
        photonView.RPC("IsInteractableTrue", RpcTarget.All);
    }

    public void EnterWaitInteract()
    {
        spriteRenderer.color = Color.green;
    }

    public void ExitInteract()
    {
        UIManager.Instance.OpenWeaponTableUI(false);
        photonView.RPC("IsInteractableFalse", RpcTarget.All);
    }

    public void ExitWaitInteract()
    {
        spriteRenderer.color = Color.white;
    }

    public void InputInteract(Vector2 input)
    {
    }
    [PunRPC]
    public void IsInteractableTrue()
    {
        this.isInteractable = true;
    }
    [PunRPC]
    public void IsInteractableFalse()
    {
        this.isInteractable = false;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isInteractable);
        }
        else
        {
            isInteractable = (bool)stream.ReceiveNext();
        }
    }
}
