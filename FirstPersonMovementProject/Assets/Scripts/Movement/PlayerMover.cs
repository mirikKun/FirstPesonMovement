using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tex111;
    [SerializeField] private TextMeshProUGUI tex222;
    [SerializeField] private float _gravity = -30f;
    [SerializeField] private float _onGroundGravity = 2;
    [SerializeField] private float groundDrag = 4;

    private float _curSpeed;
    private float _desiredSpeed;


    public bool Grounded { get; set; }


    public Rigidbody Rb { get; private set; }

    public Vector3 MoveDirection { get; private set; }
    private Vector3 _gravityDirection = new(0, -1, 0);
    private float _curGravity;

    private float _transitionTime;
    private float _curTransitionTime;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float playerHeight = 2;
    private float _speedAccelarator=10;

    private void Awake()
    {
        ResetGravity();
        Rb = GetComponent<Rigidbody>();
        Rb.drag = groundDrag;
    }

    private void Update()
    {
        //SpeedChanging();
        GroundedCheck();
        SetGroundDrag();
        SpeedControl();
    }

    private void GroundedCheck()
    {
        bool touchGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        Grounded = touchGround;
    }

    private void FixedUpdate()
    {
        MoveForce();

        GravityForce();
    }


    public void AddImpuls(Vector3 force)
    {
        Rb.AddForce(force, ForceMode.Impulse);
    }

    public void ChangeSpeed(float speed, float transitionTime)
    {
        _curTransitionTime = 0;
        _transitionTime = transitionTime;
        _desiredSpeed = speed;
        _curSpeed = speed;
    }

    public void ChangeDirection(Vector3 direction)
    {
        MoveDirection = direction;
    }


    public void ChangeGravity(float gravity)
    {
        _curGravity = _gravity;
    }

    private void MoveForce()
    {
        Rb.AddForce(MoveDirection.normalized * _curSpeed*_speedAccelarator, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 rbXYVelocity = new Vector3(Rb.velocity.x, 0f, Rb.velocity.z);
        if (rbXYVelocity.magnitude > _curSpeed)
        {
            Vector3 limitedVel = rbXYVelocity.normalized * _curSpeed;
            Rb.velocity = new Vector3(limitedVel.x, Rb.velocity.y, limitedVel.z);
        }

        tex111.text = rbXYVelocity.magnitude.ToString();
        tex222.text = Rb.velocity.magnitude.ToString();
    }

    private void GravityForce()
    {
        Rb.AddForce(_curGravity * _gravityDirection, ForceMode.Force);
    }


    public void ResetGravity()
    {
        _curGravity = _gravity;
    }

    private void SpeedChanging()
    {
        if (_curTransitionTime >= _transitionTime)
        {
            _curTransitionTime += Time.deltaTime;
            SmoothSpeedChanging();
        }
    }

    private void SetGroundDrag()
    {
        Rb.drag = Grounded ? groundDrag : 0;
    }

    private void SmoothSpeedChanging()
    {
        _curSpeed = Mathf.Lerp(_curSpeed, _desiredSpeed, _curTransitionTime / _transitionTime);
    }
}