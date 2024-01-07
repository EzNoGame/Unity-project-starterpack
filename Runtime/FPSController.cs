using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class FPSController : Singleton<FPSController>
{
    [Header("Velocity Controll")]
    [SerializeField]
    private float _jumpAcc = 15f;

    [SerializeField]
    private float _horizontalSpeed = 5f;
    
    [SerializeField]
    private float _kyoteMaxTime = 0.5f;
    [SerializeField]
    private float _preJumpMaxTime = 0.5f;
    [SerializeField]
    private float _jumpWindowMax = 0.3f;
    private float _kyoteTimer, _preJumpTimer, _jumpWindow;

    [SerializeField, Range(0.0f,1.0f)]
    private float _accThreshold = 0.1f;

    [SerializeField, Header("orientation")]
    private Transform _orientation;

    private Vector3 _horizontalInput;
    private bool _jumpInput;

    public float JumpAcc => _jumpAcc;
    public float HorizontalMaxSpeed => _horizontalSpeed;

    private CharacterController cc;
    private float _verticalVelocity;
    private Vector2 _horizontalVelocity;
    private Vector3 _movement;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        _horizontalInput = new Vector3(0f, 0f, 0f);
        _jumpInput = false;

        _verticalVelocity = 0f;
        _horizontalVelocity = Vector2.zero;

        _kyoteTimer = _kyoteMaxTime;
        _preJumpTimer = _preJumpMaxTime;
    }

    void Update()
    {
        JumpCountDown();
        HandleInput();
        CalculateMovement();
        cc.Move(_movement * Time.deltaTime);
    }

    void JumpCountDown()
    {
        if(!cc.isGrounded)
        {
            _kyoteTimer -= Time.deltaTime;
        }
        else
        {
            _kyoteTimer = _kyoteMaxTime;
        }
        _preJumpTimer -= Time.deltaTime;
        _jumpWindow -= Time.deltaTime;

        if(_preJumpTimer < 0)
        {
            _preJumpTimer = 0;
        }
        if(_jumpWindow < 0)
        {
            _jumpWindow = 0;
        }
    }

    void HandleInput()
    {
        _horizontalInput = (Input.GetAxis("Horizontal") * _orientation.right +  Input.GetAxis("Vertical") * _orientation.forward).normalized;
        if(_horizontalInput.magnitude < _accThreshold)
            _horizontalInput = Vector3.zero;
        
        _jumpInput = Input.GetButtonDown("Jump");

        if(_jumpInput)
        {
            _preJumpTimer = _preJumpMaxTime;
            if(_kyoteTimer > 0)
            {
                OpenJumpWindow();
            }
        }

        if(cc.isGrounded && _preJumpTimer > 0)//pre-jump
        {
            OpenJumpWindow();
        }
    }

    void OpenJumpWindow()
    {
        _jumpWindow = _jumpWindowMax;
    }

    void CalculateMovement()
    {
        _horizontalVelocity = new Vector2(_horizontalInput.x, _horizontalInput.z) * _horizontalSpeed;
        if(!cc.isGrounded)
        {
            if(_verticalVelocity > -20)
            {
                _verticalVelocity -= 9.81f * Time.deltaTime;
            }
        }
        else
        {
            _verticalVelocity = 0f;
        }
        
        if (Input.GetButton("Jump")&&_jumpWindow > 0f)
        {
            _verticalVelocity += _jumpAcc * Time.deltaTime;
        }
        else
        {
            _verticalVelocity -= 5f * Time.deltaTime;
        }

        _movement = new Vector3(_horizontalVelocity.x, _verticalVelocity, _horizontalVelocity.y);

    }
}
