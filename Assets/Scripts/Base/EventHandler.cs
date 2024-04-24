using System;

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
    public static event Action<int, bool> UpdateWeaponUI;
    public static void CallUpdateWeaponUI(int index, bool value)
    {
        UpdateWeaponUI?.Invoke(index, value);
    }
}
