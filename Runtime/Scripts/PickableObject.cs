using System;
using System.Collections;
using UnityEngine;

enum PickableObjectState
{
    released,
    picked,
}

public class PickableObject : Interactable
{
    [SerializeField]
    private string InteractText = "press E to pickup";
    private GameObject holder;
    private PickableObjectState state = PickableObjectState.released;

    public override void BeenSeen()
    {
        BroadcastSystem.BroadcastMessage?.Invoke(InteractText);
    }

    public override void BeenUnSeen()
    {
        BroadcastSystem.BroadcastMessage?.Invoke("");
    }
    public override void BeenIteracted(GameObject obj)
    {
        holder = obj;
        state = PickableObjectState.picked;
        BeenUnSeen();
    }
    public override void BeenUndone()
    {
        state = PickableObjectState.released;
        BeenSeen();
    }

    private void LateUpdate() {
        if(state == PickableObjectState.picked)
        {
            transform.position = holder.transform.position;
        }
    }
}