using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.PlayerSystem
{
    public class PlayerInSeaEffect : MonoBehaviour
    {
        public Transform player;

        private void LateUpdate()
        {
            transform.position = player.position;
        }
    }
}