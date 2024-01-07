using System;
using UnityEngine;

public class FPSCamera : MonoBehaviour {
    [SerializeField]
    private float _sensitivity = 100f;

    [SerializeField]
    private Transform _orientation;
    [SerializeField, Header("transform in play to keep track the pos of camera")]
    private Transform _cameraPosition;

    private float _xRotation, _yRotation;

    private void Start() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {

        transform.position = _cameraPosition.position;

        float mosueX = Input.GetAxisRaw("Mouse X") * _sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * _sensitivity;

        _xRotation -= mouseY;
        _xRotation = Math.Clamp(_xRotation, -90f, 90f);
        _yRotation += mosueX;

        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}