using System.Collections;
using System.Collections.Generic;
using MyGame.UISystem;
using Photon.Pun;
using UnityEngine;
namespace MyGame.ShipSystem.Sail
{
    public class ShipSail : MonoBehaviourPun
    {
        public float sailValue = 1f;
        private SpriteRenderer sailSprite;
        public float sailSpeed = 0.2f;

        void Awake()
        {
            sailSprite = GetComponentInChildren<SpriteRenderer>();
        }
        private void Update()
        {
        }
        public void HiddenSail()
        {
            SetSailTransparency(0.4f);
        }
        public void RevealSail()
        {
            SetSailTransparency(1);
        }
        public void SetSailTransparency(float alpha)
        {
            Material material = sailSprite.material;
            Color color = material.color;
            color.a = alpha;
            material.color = color;
        }
        public void AddSailValue(float value)
        {
            sailValue += value;
            photonView.RPC("PunSailValue", RpcTarget.All, sailValue);
            sailValue = Mathf.Clamp(sailValue, 0f, 1f);
        }
        [PunRPC]
        public void PunSailValue(float value)
        {
            sailValue = value;
        }
    }
}