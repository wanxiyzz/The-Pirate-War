using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mygame.PlayerSystem
{
    public class PlayerBackShip : MonoBehaviour
    {
        public Transform shipTrans;
        public string shipName;
        [SerializeField] private GameObject backShipCannon;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] Transform backShipCannonTrans;
        private void Start()
        {
            StartCoroutine(CheckDistance());
        }
        IEnumerator CheckDistance()
        {
            while (true)
            {
                if (shipTrans != null)
                {
                    if (Vector2.Distance(transform.position, shipTrans.position) > 70f)
                    {
                        if (backShipCannonTrans == null)
                        {
                            CheckNull();
                        }
                        else
                        {
                            if (Vector2.Distance(transform.position, backShipCannonTrans.position) > 50f)
                            {
                                Destroy(backShipCannonTrans.gameObject);
                                CheckNull();
                            }
                        }
                    }
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    shipTrans = GameManager.Instance.Getship(shipName);
                    yield return new WaitForSeconds(1f);
                }
            }
        }
        public void CheckNull()
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector2 randomPoint = (Vector2)transform.position + randomDirection * Random.Range(0f, 5f);
            RaycastHit2D hit = Physics2D.Raycast(randomPoint, Vector2.down, Mathf.Infinity, layerMask);

            if (hit.collider == null)
            {
                backShipCannonTrans = Instantiate(backShipCannon, randomPoint, Quaternion.identity).transform;
            }
        }
    }
}