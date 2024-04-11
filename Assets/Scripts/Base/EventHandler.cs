using System;
using UnityEngine;

public class EventHandler
{
    public static event Action<bool> PlayerInetractive;
    public static void CallPlayerInetractive(bool value)
    {
        PlayerInetractive?.Invoke(value);
    }
    public static event Action<Vector2> MoveInput;
    public static void CallMoveInput(Vector2 value)
    {
        MoveInput?.Invoke(value);
    }
}
