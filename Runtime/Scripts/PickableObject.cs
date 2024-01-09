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
    private float _maxDistance = 5f, _minDistance = 0.01f;

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
        // GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
    }
    public override void BeenUndone()
    {
        state = PickableObjectState.released;
        BroadcastSystem.ObjectDropped?.Invoke(gameObject);
        // GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
    }

    private void FixedUpdate() {
        if(state == PickableObjectState.picked && Vector3.Distance(transform.position, holder.transform.position) > _minDistance)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce((holder.transform.position - transform.position)*0.5f, ForceMode.Impulse);
        }
        if(holder!=null && Vector3.Distance(transform.position, holder.transform.position) > _maxDistance)
        {
            BeenUndone();
        }
    }
}