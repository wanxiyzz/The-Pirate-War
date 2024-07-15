using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.ShipSystem.Cannon
{
    public class Cannonball : MonoBehaviour
    {
        // [SerializeField] float existenceTime = 4f;
        // Start is called before the first frame update
        void Start()
        {
            //dasdgasdjkasgdkjsa
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.gameObject.name);
            if (other.gameObject.CompareTag("Ship"))
            {
                other.GetComponent<ShipController>().TakeCannonball(transform.position);
            }
            gameObject.SetActive(false);
        }
    }
}