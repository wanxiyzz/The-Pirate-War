using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.PlayerSystem
{
    public class PlayerAnimation : MonoBehaviour
    {
        public PlayerController playerController;
        private Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerController = GetComponent<PlayerController>();
        }
        void Update()
        {
            animator.SetBool("isMoving", playerController.isMoveing);
            animator.SetBool("isSea", playerController.playerPos == PlayerPos.Sea);
        }
    }
}