using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.ShipSystem
{
    public class ShipFloor : MonoBehaviour
    {
        [SerializeField] SpriteRenderer[] shipTopSprites;
        [SerializeField] SpriteRenderer[] shipButtomSprites;
        [SerializeField] Collider2D[] shipTopInteract;
        [SerializeField] Collider2D[] shipButtomInteract;
        public bool isEnterFloor = false;
        public void Enter2F()
        {
            StartCoroutine(EnterFloorShip());
            CameraManager.Instance.FieldVision(5);
            isEnterFloor = true;
        }
        public void Exit2F()
        {
            StartCoroutine(ExitFloorShip());
            CameraManager.Instance.FieldVision(15);
            isEnterFloor = false;
        }
        IEnumerator EnterFloorShip()
        {
            Debug.Log("进入");
            foreach (var topInteract in shipTopInteract)
            {
                topInteract.enabled = false;
            }
            foreach (var buttomInteract in shipButtomInteract)
            {
                buttomInteract.enabled = true;
            }
            for (int i = 0; i < 26; i++)
            {
                foreach (var shipTopSprite in shipTopSprites)
                {
                    shipTopSprite.color = Color.Lerp(shipTopSprite.color, new Color(1, 1, 1, 0), i / 25f);
                }
                foreach (var shipButtomSprite in shipButtomSprites)
                {
                    shipButtomSprite.color = Color.Lerp(shipButtomSprite.color, new Color(1, 1, 1, 1), i / 25f);
                }
                yield return Setting.waitForFixedUpdate;
            }
        }
        IEnumerator ExitFloorShip()
        {
            Debug.Log("离开");
            foreach (var topInteract in shipTopInteract)
            {
                topInteract.enabled = true;
            }
            foreach (var buttomInteract in shipButtomInteract)
            {
                buttomInteract.enabled = false;
            }
            for (int i = 0; i < 26; i++)
            {
                foreach (var shipTopSprite in shipTopSprites)
                {
                    shipTopSprite.color = Color.Lerp(shipTopSprite.color, new Color(1, 1, 1, 1), i / 25f);
                }
                foreach (var shipButtomSprite in shipButtomSprites)
                {
                    shipButtomSprite.color = Color.Lerp(shipButtomSprite.color, new Color(1, 1, 1, 0), i / 25f);
                }
                yield return Setting.waitForFixedUpdate;
            }
        }
    }
}