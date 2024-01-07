using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(Rigidbody))]
public class FPSController : Singleton<FPSController>
{
    [Header("Velocity Controll")]
    [SerializeField]
    private float _jumpAcceleration = 25f;
    [SerializeField]
    private float _walkSpeed = 5f, _sprintSpeed = 10f;
    [SerializeField, Header("in second, how many time can player still jump after leaving a platform")]
    private float _kyoteTime = 0.5f;
    [SerializeField, Header("in second, how early can player input jump before touching ground for that instruction to activated")]
    private float _preJumpMaxTime = 0.5f;
    [SerializeField, Header("in second, how long can player hold jump to jump higher")]
    private float _jumpWindow = 0.3f;
    [SerializeField, Range(0f,0.99f), Header("in precentage, how fast will the character react to horizontal input")]
    private float _accThreshold = 0.7f;
    [SerializeField]
    private Transform _orientation;

    private Vector3 _horizontalInput;
    private bool _jumpInput;
    private CharacterController _cc;
    private float _verticalVelocity;
    private Vector3 _movement;
    private float _kyoteTimer, _preJumpTimer, _jumpWindowTimer;


    public float JumpAcc => _jumpAcceleration;
    public float HorizontalMaxSpeed => _walkSpeed;


    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _horizontalInput = new Vector3(0f, 0f, 0f);
        _jumpInput = false;

        _verticalVelocity = 0f;

        _kyoteTimer = _kyoteTime;
        _preJumpTimer = _preJumpMaxTime;
        _movement = Vector3.zero;
    }

    void Update()
    {
        JumpCountDown();
        HandleInput();
        CalculateMovement();
        _cc.Move(_movement * Time.deltaTime);
    }

    void JumpCountDown()
    {
        if(!_cc.isGrounded)
        {
            _kyoteTimer -= Time.deltaTime;
        }
        else
        {
            _kyoteTimer = _kyoteTime;
        }
        _preJumpTimer -= Time.deltaTime;
        _jumpWindowTimer -= Time.deltaTime;

        if(_preJumpTimer < 0)
        {
            _preJumpTimer = 0;
        }
        if(_jumpWindowTimer < 0)
        {
            _jumpWindowTimer = 0;
        }
    }

    Vector2 VerticalInputThreashHold()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if(Mathf.Abs(x) <= _accThreshold)
            x = 0;
        
        if(Mathf.Abs(y) <= _accThreshold)
            y = 0;

        return new Vector2(x, y);
    }
    void HandleInput()
    {
        Vector2 input = VerticalInputThreashHold();
        _horizontalInput = input.x * _orientation.right +  input.y * _orientation.forward;

        _jumpInput = Input.GetButtonDown("Jump");

        if(_jumpInput)
        {
            _preJumpTimer = _preJumpMaxTime;
            if(_kyoteTimer > 0)
            {
                OpenJumpWindow();
            }
        }

        if(_cc.isGrounded && _preJumpTimer > 0)
        {
            OpenJumpWindow();
        }
    }

    void OpenJumpWindow()
    {
        _jumpWindowTimer = _jumpWindow;
    }

    bool SprintCheck()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    void CalculateMovement()
    {
        if(!_cc.isGrounded)
        {
            if(_verticalVelocity > -42)
            {
                _verticalVelocity -= 9.81f * Time.deltaTime;
            }
        }
        else
        {
            _verticalVelocity = 0f;
        }

        if (Input.GetButton("Jump"))
        {
            if(_jumpWindowTimer > 0)
            {
                _verticalVelocity += _jumpAcceleration * Time.deltaTime;
            }
        }

        bool isSprinting = SprintCheck();

        _movement = new Vector3(
            _horizontalInput.x*(isSprinting?_sprintSpeed:_walkSpeed), 
            _verticalVelocity, 
            _horizontalInput.z*(isSprinting?_sprintSpeed:_walkSpeed));

    }
}
