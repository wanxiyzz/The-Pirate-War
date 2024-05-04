using System.Collections;
using System.Collections.Generic;
using System.Net;
using MyGame.InputSystem;
using MyGame.PlayerSystem;
using Photon.Pun;
using UnityEngine;

public class Harpoon : MonoBehaviourPun, Iinteractable, IPunObservable
{

    public bool IsInteractable => isInteractable;
    public bool isInteractable;

    public string Feature => "使用鱼叉";

    public bool IsSimple => false;

    public bool IsBoard => true;

    public LineRenderer lineRenderer;
    public float hookSpeed = 50f; // 线的延长速度

    private Vector3 startPoint;
    private Vector3 direction;
    private float currentDistance = 0f;
    private bool isFire;
    private bool tackBack;
    private Vector3 defaultRotaton;
    [SerializeField] float rotateSpeed;
    [SerializeField] float steerAngle;
    [SerializeField] float maxSteerAngle;


    [SerializeField] Transform firePoint;
    private Transform facehootTrans;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Transform interactPoint;
    [SerializeField] Transform itemTrans;
    private Facehoot facehoot;

    private void Awake()
    {
        facehoot = GetComponentInChildren<Facehoot>();
        facehootTrans = facehoot.transform;
        isFire = false;
        tackBack = false;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        defaultRotaton = transform.rotation.eulerAngles;
        lineRenderer.SetPosition(1, firePoint.position);
        lineRenderer.SetPosition(0, firePoint.position);
        facehootTrans.position = firePoint.position;
        facehoot.HookSomething += OnHookSomething;
    }
    [PunRPC]
    private void OnHookSomething(Transform target)
    {
        tackBack = true;
        itemTrans = target;
    }

    private void Update()
    {
        ExtendLine();
    }

    private void ExtendLine()
    {
        if (isFire)
        {

            if (tackBack)
            {
                currentDistance -= Time.deltaTime * hookSpeed;
                lineRenderer.SetPosition(0, startPoint);
                lineRenderer.SetPosition(1, startPoint + direction * currentDistance);
                if (currentDistance <= 0)
                {
                    isFire = false;
                    tackBack = false;
                    lineRenderer.SetPosition(0, startPoint);
                    lineRenderer.SetPosition(1, startPoint);
                }
                if (itemTrans != null)
                {
                    itemTrans.position = facehootTrans.position;
                }
            }
            else
            {
                if (currentDistance < Setting.maxHarpoonLenght)
                {
                    currentDistance += Time.deltaTime * hookSpeed;
                    lineRenderer.SetPosition(0, startPoint);
                    lineRenderer.SetPosition(1, startPoint + direction * currentDistance);
                }
                else
                {
                    tackBack = true;
                }
            }
            facehoot.transform.position = startPoint + direction * currentDistance;
        }
    }
    private void FireHook(Vector3 start, Vector3 dir)
    {
        Debug.Log("发射钩子");
        startPoint = start;
        direction = dir.normalized;
        currentDistance = 0;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, startPoint);
    }
    public Vector3 GetEndPoint()
    {
        return lineRenderer.GetPosition(1);
    }

    public void EnterInteract(PlayerController playerController)
    {
        GameInput.Instance.MovementAction += InputInteract;
        GameInput.Instance.UseItemAction += HarpoonFirePun;
        GameManager.Instance.player.PlayerEnterInteract(interactPoint);
        spriteRenderer.sprite = ItemManager.Instance.defaultCannonSprite; CameraManager.Instance.ChangeCameraOffset(0.5f, 0.8f);
        photonView.RPC("IsInteractableTrue", RpcTarget.All);
    }

    public void ExitInteract()
    {
        CameraManager.Instance.ResetCamera();
        GameInput.Instance.MovementAction -= InputInteract;
        GameInput.Instance.UseItemAction -= HarpoonFirePun;
        spriteRenderer.sprite = ItemManager.Instance.defaultCannonSprite; CameraManager.Instance.ChangeCameraOffset(0.5f, 0.5f);
        spriteRenderer.sprite = ItemManager.Instance.selectCannonSprite;
        photonView.RPC("IsInteractableFalse", RpcTarget.All);
    }

    public void EnterWaitInteract()
    {
        spriteRenderer.sprite = ItemManager.Instance.selectCannonSprite;
    }

    public void ExitWaitInteract()
    {
        spriteRenderer.sprite = ItemManager.Instance.defaultCannonSprite;
    }

    public void InputInteract(Vector2 input)
    {
        if (isFire) return;
        CameraManager.Instance.RotateCamera(transform.rotation);
        steerAngle += input.x * Time.deltaTime * rotateSpeed;
        steerAngle = Mathf.Clamp(steerAngle, -maxSteerAngle, maxSteerAngle);
        photonView.RPC("PunSteerAngle", RpcTarget.All, steerAngle);
        transform.localRotation = Quaternion.Euler(defaultRotaton - new Vector3(0f, 0f, steerAngle));
    }

    private void HarpoonFirePun()
    {
        photonView.RPC("HarpoonFire", RpcTarget.All);
    }
    [PunRPC]
    private void HarpoonFire()
    {
        if (isFire) return;
        if (itemTrans != null)
        {
            Debug.Log("丢出物品");
            Vector2 randomDirection = Quaternion.AngleAxis(Random.Range(-50, 50), transform.up) * transform.up;
            float randomForce = Random.Range(3, 5);
            Vector2 force = randomDirection * 5;
            itemTrans.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            itemTrans = null;
            return;
        }
        FireHook(firePoint.position, transform.up);
        isFire = true;
    }
    [PunRPC]
    public void PunSteerAngle(float angle)
    {
        steerAngle = angle;
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
    }
}

