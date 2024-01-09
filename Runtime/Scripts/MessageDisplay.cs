using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class MessageDisplay : MessageListener
{
    protected override void RecieveString (string message)
    {
        GetComponent<TMP_Text>().text = message;
    }
}
