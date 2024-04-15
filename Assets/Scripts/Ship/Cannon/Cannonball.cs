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

        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //animation
            gameObject.SetActive(false);
        }
    }
}