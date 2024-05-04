using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.PlayerSystem;
using Photon.Pun;
using MyGame.ShipSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public string shipName;
    public GameObject playerPrefab;
    public GameObject shipPrefab;
    public GameObject playerInSeaEffect;
    public PlayerController player;
    public GameObject backPlayerCannon;
    public Dictionary<string, Transform> ships = new Dictionary<string, Transform>();
    public Transform[] shipResetPoint;
    [SerializeField] Transform shiptranform;
    [SerializeField] Transform[] shipResetPoints;
    [SerializeField] LayerMask layerMask;
    protected override void Awake()
    {
        base.Awake();
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
    public void GetAllShip()
    {
        ShipController[] allObjects = UnityEngine.Object.FindObjectsOfType<ShipController>();
        foreach (var item in allObjects)
        {
            if (!ships.ContainsKey(item.shipName))
            {
                ships.Add(item.shipName, item.transform);
            }
        }
    }
    public void EnterGame(string shipName)
    {
        Debug.Log("进入游戏");
        StartCoroutine(EnterGameCoroutine(shipName));
    }
    IEnumerator EnterGameCoroutine(string shipName)
    {
        Debug.Log("要进入的船" + shipName);
        while (true)
        {
            Debug.Log("有没有这个船" + ships.ContainsKey(shipName));
            foreach (var item in ships)
            {
                Debug.Log("船的名字" + item.Key);
            }
            if (ships.ContainsKey(shipName))
            {
                Debug.Log("进入游戏成功");
                var playerGameObject = PhotonNetwork.Instantiate("Prefabs/Player/Player", ships[shipName].position, Quaternion.identity);
                PlayerController playerContro = playerGameObject.GetComponent<PlayerController>();
                playerContro.Init(ships[shipName]);
                player = playerContro;
                CameraManager.Instance.SetFollow(playerGameObject.transform);
                break;
            }
            else
            {
                GetAllShip();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    public void CreateShip(string shipName)
    {
        if (ships.ContainsKey(shipName))
        {
            Debug.Log("已经存在");
        }
        else
        {
            CheckNullAndCreateShip(shipName);
        }
    }
    public void CheckNullAndCreateShip(string shipName)
    {
        while (true)
        {
            int index = Random.Range(0, shipResetPoints.Length);
            var pos = shipResetPoints[index].position;
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down, Mathf.Infinity, layerMask);
            if (hit.collider == null)
            {

                var shipGameObject = PhotonNetwork.Instantiate("Prefabs/Ship/Ship", pos, Quaternion.identity);
                if (!ships.ContainsKey(shipName))
                {
                    ships.Add(shipName, shipGameObject.transform);
                    ShipController shipController = shipGameObject.GetComponent<ShipController>();
                    shipController.shipName = shipName;
                    Debug.Log(shipController.shipName);
                    Debug.Log("创建成功");
                }
                break;
            }
        }

    }
    public Transform Getship(string shipName)
    {
        if (ships.ContainsKey(shipName))
        {
            return ships[shipName];
        }
        else
        {
            GetAllShip();
            if (ships.ContainsKey(shipName))
            {
                return ships[shipName];
            }
            else
            {
                return null;
            }
        }
    }
    public void DestroyServerGameObject(GameObject obj)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (obj != null && obj.GetPhotonView() != null)
            {
                PhotonNetwork.Destroy(obj);
            }
            else
            {
                Debug.LogWarning("传入的物体为空或不是 PhotonView.");
            }
        }
    }
}
