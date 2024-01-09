using System;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class FPSCamera : Singleton<FPSCamera> {
    [SerializeField]
    private float _sensitivity = 100f;

    [SerializeField, Header("transform in player to sync the rotation with camera")]
    private Transform _orientation;
    
    [SerializeField, Header("transform in player to keep track the pos of camera")]
    private Transform _cameraHolder;

    [SerializeField, Header("simulate of players hand")]
    private Transform _hand;

    private float _xRotation, _yRotation;

    private GameObject _objLookingAt;

    private void Start() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void FixedUpdate() {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f, 1<<3))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if(_objLookingAt == hit.rigidbody.gameObject)
                return;
            
            _objLookingAt = hit.rigidbody.gameObject;
            _objLookingAt.GetComponent<Interactable>().BeenSeen();

        }
        else
        {
            if(_objLookingAt != null)
            {
                _objLookingAt.GetComponent<Interactable>().BeenUnSeen();
                _objLookingAt = null;
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 5, Color.white);
        }
    }

    private void LateUpdate() {

        float mosueX = Input.GetAxisRaw("Mouse X") * _sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * _sensitivity;

        _xRotation -= mouseY;
        _xRotation = Math.Clamp(_xRotation, -90f, 90f);
        _yRotation += mosueX;

        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);

        transform.position = _cameraHolder.transform.position;

        float distance = 3f;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3f, 1<<6))
        {
            distance = hit.distance;
        }
        
        _hand.position = transform.position + transform.TransformDirection(Vector3.forward) * distance;

    }

    public GameObject GetObjLookedAt() => _objLookingAt;
}