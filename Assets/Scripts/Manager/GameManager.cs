using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.PlayerSystem;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;
    public GameObject shipPrefab;
    public PlayerController player;
    public Dictionary<string, Transform> ships;
    public Transform[] shipResetPoint;
    void Start()
    {

    }
    void Update()
    {

    }
    public Transform Getship(string shipName)
    {
        return ships[shipName];
    }
    public void AddShip(string shipName, Vector2 pos)
    {

    }
    public void CreatPlayerself(string shipName)
    {
        if (ships.ContainsKey(shipName))
        {
            player = Instantiate(playerPrefab, ships[shipName].position, Quaternion.identity, ships[shipName]).GetComponent<PlayerController>();
        }
        else
        {

        }
    }
}
