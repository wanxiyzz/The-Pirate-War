using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.ShipSystem.Floor
{
    public class ShipFloor : MonoBehaviour
    {
        [SerializeField] Transform topParent;
        [SerializeField] Transform ButtomParent;
        private SpriteRenderer[] shipTopSprites;
        private SpriteRenderer[] shipButtomSprites;
        private Collider2D[] shipTopInteract;
        private Collider2D[] shipButtomInteract;
        [SerializeField] PolygonCollider2D enterFloorCollider;
        [SerializeField] PolygonCollider2D exitFloorCollider;
        public bool isEnterFloor = false;
        private void Awake()
        {
            shipTopSprites = topParent.GetComponentsInChildren<SpriteRenderer>();
            shipButtomSprites = ButtomParent.GetComponentsInChildren<SpriteRenderer>();
            shipTopInteract = topParent.GetComponentsInChildren<Collider2D>();
            shipButtomInteract = ButtomParent.GetComponentsInChildren<Collider2D>();
            for (int i = 0; i < shipButtomInteract.Length; i++)
            {
                shipButtomInteract[i].enabled = false;
            }
            for (int i = 0; i < shipButtomSprites.Length; i++)
            {
                shipButtomSprites[i].color = new Color(1f, 1f, 1f, 0f);
            }
            exitFloorCollider.enabled = false;
        }
        public void Enter2F()
        {
            StartCoroutine(EnterFloorShip());
            GlobalLightManager.Instance.ChangeLightIntensity(0f);
            enterFloorCollider.enabled = false;
            exitFloorCollider.enabled = true;
            CameraManager.Instance.FieldVision(5);
            isEnterFloor = true;
        }
        public void Exit2F()
        {
            StartCoroutine(ExitFloorShip());
            GlobalLightManager.Instance.ChangeLightIntensity(1f);
            enterFloorCollider.enabled = true;
            exitFloorCollider.enabled = false;
            CameraManager.Instance.FieldVision(15);
            isEnterFloor = false;
        }
        IEnumerator EnterFloorShip()
        {
            GameManager.Instance.player.To2Floor();
            foreach (var topInteract in shipTopInteract)
            {
                topInteract.enabled = false;
            }
            foreach (var buttomInteract in shipButtomInteract)
            {
                buttomInteract.enabled = true;
            }
            for (int i = 0; i < 51; i++)
            {
                foreach (var shipTopSprite in shipTopSprites)
                {
                    shipTopSprite.color = Color.Lerp(shipTopSprite.color, new Color(1, 1, 1, 0), i / 50f);
                }
                foreach (var shipButtomSprite in shipButtomSprites)
                {
                    shipButtomSprite.color = Color.Lerp(shipButtomSprite.color, new Color(1, 1, 1, 1), i / 50f);
                }
                yield return Setting.waitForFixedUpdate;
            }

        }
        IEnumerator ExitFloorShip()
        {
            GameManager.Instance.player.Exit2Floor();
            foreach (var topInteract in shipTopInteract)
            {
                topInteract.enabled = true;
            }
            foreach (var buttomInteract in shipButtomInteract)
            {
                buttomInteract.enabled = false;
            }
            for (int i = 0; i < 51; i++)
            {
                foreach (var shipTopSprite in shipTopSprites)
                {
                    shipTopSprite.color = Color.Lerp(shipTopSprite.color, new Color(1, 1, 1, 1), i / 50f);
                }
                foreach (var shipButtomSprite in shipButtomSprites)
                {
                    shipButtomSprite.color = Color.Lerp(shipButtomSprite.color, new Color(1, 1, 1, 0), i / 50f);
                }
                yield return Setting.waitForFixedUpdate;
            }
        }
    }
}