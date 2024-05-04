using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ExitGames.Client.Photon;
using MyGame.Inventory;
using Photon.Pun;
public class MyNetworkManager : MonoBehaviourPunCallbacks
{
    void Awake()
    {
        PhotonPeer.RegisterType(typeof(MyGame.Inventory.Item), (byte)'I', SerializeItem, DeserializeItem);
    }
    static byte[] SerializeItem(object obj)
    {
        Item item = (Item)obj;
        MemoryStream memoryStream = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(memoryStream, item.itemID);
        formatter.Serialize(memoryStream, item.itemAmount);
        return memoryStream.ToArray();
    }
    static object DeserializeItem(byte[] data)
    {
        MemoryStream memoryStream = new MemoryStream(data);
        BinaryFormatter formatter = new BinaryFormatter();
        int itemID = (int)formatter.Deserialize(memoryStream);
        int itemAmount = (int)formatter.Deserialize(memoryStream);
        return new Item { itemID = itemID, itemAmount = itemAmount };
    }
}
