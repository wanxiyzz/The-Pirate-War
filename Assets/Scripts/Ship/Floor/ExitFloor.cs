using MyGame.PlayerSystem;
using UnityEngine;
namespace MyGame.ShipSystem.Floor
{
    public class ExitFloor : MonoBehaviour
    {
        ShipFloor shipFloor;
        private void Start()
        {
            shipFloor = GetComponentInParent<ShipFloor>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && shipFloor.isEnterFloor)
            {
                shipFloor.Exit2F();
            }
        }
    }
}