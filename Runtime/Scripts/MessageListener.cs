using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageListener : MonoBehaviour
{
    protected virtual void OnEnable() {
        BroadcastSystem.BroadcastMessage += RecieveString;
    }

    protected virtual void OnDisable() {
        BroadcastSystem.BroadcastMessage -= RecieveString;
    }

    protected virtual void RecieveString(string message)
    {
        Debug.Log(message);
    }
}
