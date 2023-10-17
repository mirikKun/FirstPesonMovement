using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private float _onGroundGravity = -2;
    private float _curSpeed;
    private float _desiredSpeed;


    private bool _grounded;

    private Rigidbody _rb;

    private Vector3 _moveDirection;
    private Vector3 _gravityDirection = new (0, -1, 0);
    private float _curGravity;

    private float _transitionTime;
    private float _curTransitionTime;

    private void Awake()
    {
        ResetGravity();
    }

    private void Update()
    {
        SpeedChanging();
    }

    private void FixedUpdate()
    {
        MoveForce();
        GravityForce();
    }

    public void ChangeGravity(float gravity)
    {
        _curGravity = _gravity;
    }

    public void ChangeSpeed(float speed, float transitionTime)
    {
        _curTransitionTime = 0;
        _transitionTime = transitionTime;
        _desiredSpeed = speed;
    }

    private void MoveForce()
    {
        _rb.AddForce(_moveDirection.normalized * _curSpeed, ForceMode.Force);
    }

    private void GravityForce()
    {
        if (_grounded)
            _rb.AddForce(_onGroundGravity * _gravityDirection, ForceMode.Force);
        else
            _rb.AddForce(_curGravity * _gravityDirection, ForceMode.Force);
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

    private void SmoothSpeedChanging()
    {
        _curSpeed = Mathf.Lerp(_curSpeed, _desiredSpeed, _curTransitionTime / _transitionTime);
    }
}