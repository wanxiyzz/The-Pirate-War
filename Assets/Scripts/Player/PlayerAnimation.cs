using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.PlayerSystem
{
    public class PlayerAnimation : MonoBehaviour
    {
        public PlayerController playerController;
        // [SerializeField] private float sprintTime = 1f;
        [SerializeField] private Vector2 spriteSpeed;
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
        public void Sprint()
        {
            StartCoroutine(SprintIE());
        }
        IEnumerator SprintIE()
        {
            yield return null;
        }
        public void BoardOrLeaveShip()
        {
            animator.Play("LeaveOrBoardShip");
        }
        public void FirePlayerBackShip()
        {
            animator.Play("BackShip");
        }
    }
}