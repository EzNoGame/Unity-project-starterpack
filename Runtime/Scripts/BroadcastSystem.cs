using System;
using UnityEngine;

public static class BroadcastSystem
{
    public static Action<string> BroadcastMessage;
    public static Action<GameObject> ObjectPickedUp;
    public static Action<GameObject> ObjectDropped;
}