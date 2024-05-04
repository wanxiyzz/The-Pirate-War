using UnityEngine;
using System.Collections;
using MyGame.PlayerSystem;
namespace MyGame.HandheldableSystem.WeaponSystem
{
    public class Bullet : MonoBehaviour
    {
        private int damage;
        [SerializeField] string attackTag;
        [SerializeField] bool is2F;
        private void Awake()
        {
            if (is2F)
            {

            }
        }
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
                if (other.TryGetComponent<PlayerController>(out PlayerController player))
                {
                    if (is2F && player.playerPos == PlayerPos.Ship2F)
                        player.ChangeHealth(-damage);
                    else if (!is2F && player.playerPos == PlayerPos.Ship1F)
                        player.ChangeHealth(-damage);
                }
                gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
        }
        private IEnumerator Eliminate()
        {
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false);
        }
    }
}