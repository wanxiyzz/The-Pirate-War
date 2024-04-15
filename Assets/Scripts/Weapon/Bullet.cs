using UnityEngine;
using System.Collections;
using MyGame.PlayerSystem;
namespace MyGame.WeaponSystem
{
    public class Bullet : MonoBehaviour
    {
        private int damage;
        [SerializeField] string attackTag;
        public void Init(int damage, Vector2 dir)
        {
            this.damage = damage;
            GetComponent<Rigidbody2D>().velocity = dir * 120;
            StartCoroutine(Eliminate());
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(attackTag))
            {
                if (other.TryGetComponent<PlayerState>(out PlayerState player))
                {
                    player.ChangeHealth(-damage);
                }
                gameObject.SetActive(false);
            }
        }
        private IEnumerator Eliminate()
        {
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false);
        }

    }
}