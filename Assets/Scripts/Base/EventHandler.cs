using System;
using MyGame.Inventory;

public class EventHandler
{
    public static event Action<bool> PlayerInetractive;
    public static void CallPlayerInetractive(bool value)
    {
        PlayerInetractive?.Invoke(value);
    }
    public static event Action<int, int> ChangeWeaponUI;
    public static void CallChangeWeaponUI(int tableIndex, int bagIndex)
    {
        ChangeWeaponUI?.Invoke(tableIndex, bagIndex);
    }
    // Add other events as needed
    public static event Action<bool> EnterPlayerInteract;
    public static void CallEnterPlayerInteract(bool value)
    {
        EnterPlayerInteract?.Invoke(value);
    }
    public static event Action<Item[], bool, BarrelType> OpenBarrelUI;
    public static void CallOpenBarrelUI(Item[] items, bool value, BarrelType type)
    {
        OpenBarrelUI?.Invoke(items, value, type);
    }
    public static event Action<Item[], bool, BarrelType> OpenBarrel;
    public static void CallOpenBarrel(Item[] items, bool value, BarrelType type)
    {
        OpenBarrel?.Invoke(items, value, type);
    }
    public static event Action<Item[], bool> OpenBagUI;
    public static void CallOpenBagUI(Item[] items, bool value)
    {
        OpenBagUI?.Invoke(items, value);
    }
    public static event Action<bool> PickUpAllItem;
    public static void CallPickUpAllItem(bool value)
    {
        PickUpAllItem?.Invoke(value);
    }
    public static event Action CountShipsAndPlayers;
    public static void CallCountShipsAndPlayers()
    {
        CountShipsAndPlayers?.Invoke();
    }
}
