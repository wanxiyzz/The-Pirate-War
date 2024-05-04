using MyGame.ShipSystem.Hole;
using Photon.Pun;
using UnityEngine;
namespace MyGame.ShipSystem
{
    public class ShipTakeWater : MonoBehaviour, IPunObservable
    {
        public float waterValue;
        [SerializeField] ShipHoleManager shipHoleManager;
        [SerializeField] ShipController shipController;
        void Update()
        {
            ShipFlooded();
        }
        private void ShipFlooded()
        {
            waterValue += shipHoleManager.ShipFlooded() * Time.deltaTime * Setting.oneHoleWaterSpeed;
            waterValue = Mathf.Clamp(waterValue, 0f, 1f);
            if (waterValue > 0.8f)
            {
                shipController.Shipwreck();
            }
            if (waterValue >= 1)
            {
                GameManager.Instance.DestroyServerGameObject(shipController.gameObject);
            }
            //Debug.Log(waterValue);
        }
        public void TakeWater(float water)
        {
            if (waterValue > 0.8f)
            {
                return;
            }
            waterValue -= water;
            Debug.Log(waterValue);
            waterValue = Mathf.Clamp(waterValue, 0f, 1f);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(waterValue);
            }
            else
            {
                waterValue = (float)stream.ReceiveNext();
            }
        }
    }
}