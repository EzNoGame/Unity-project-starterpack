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
    private float _maxDistance = 5f;

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
        BroadcastSystem.ObjectPickedUp?.Invoke(gameObject);
        GetComponent<Rigidbody>().useGravity = false;
    }
    public override void BeenUndone()
    {
        state = PickableObjectState.released;
        BroadcastSystem.ObjectDropped?.Invoke(gameObject);
        GetComponent<Rigidbody>().useGravity = true;
    }

    private void FixedUpdate() {
        if(state == PickableObjectState.picked)
        {
            transform.position = Vector3.MoveTowards(transform.position, holder.transform.position, 7*Time.deltaTime);
        }
        if(holder!=null && Vector3.Distance(transform.position, holder.transform.position) > _maxDistance)
        {
            BeenUndone();
        }
    }
}