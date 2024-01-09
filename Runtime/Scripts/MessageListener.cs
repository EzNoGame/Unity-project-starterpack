using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageListener : MonoBehaviour
{
    private void OnEnable() {
        BroadcastSystem.BroadcastMessage += RecieveString;
    }

    private void OnDisable() {
        BroadcastSystem.BroadcastMessage -= RecieveString;
    }

    protected virtual void RecieveString(string message)
    {
        Debug.Log(message);
    }
}
