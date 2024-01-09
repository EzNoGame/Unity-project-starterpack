using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private GameObject _holdingObj;
    private void OnEnable() {
        BroadcastSystem.ObjectDropped += Drop;    
    }

    private void OnDisable() {
        BroadcastSystem.ObjectDropped -= Drop;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(FPSCamera.Instance.GetObjLookedAt()!=null && _holdingObj==null)
            {
                _holdingObj = FPSCamera.Instance.GetObjLookedAt();
                _holdingObj.GetComponent<Interactable>().BeenIteracted(gameObject);
            }
            else if(_holdingObj!=null)
            {
                _holdingObj.GetComponent<Interactable>().BeenUndone();
                _holdingObj = null;
            }
        }
    }

    private void Drop(GameObject obj)
    {
        _holdingObj = null;
    }
}
