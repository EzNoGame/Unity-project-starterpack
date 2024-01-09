using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class MessageDisplay : MessageListener
{
    protected override void OnEnable() {
        base.OnEnable();
        BroadcastSystem.ObjectDropped += EnableText;
        BroadcastSystem.ObjectPickedUp += DisableText;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        BroadcastSystem.ObjectDropped -= EnableText;
        BroadcastSystem.ObjectPickedUp -= DisableText;
    }

    private void DisableText(GameObject obj)
    {
        GetComponent<TMP_Text>().enabled = false;
    }

    private void EnableText(GameObject obj)
    {
        GetComponent<TMP_Text>().enabled = true;
    }

    protected override void RecieveString (string message)
    {
        GetComponent<TMP_Text>().text = message;
    }
}
