using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _gravity = -30f;
    [SerializeField] private float _onGroundGravity = 2;
    [SerializeField] private float _groundDrag = 4;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float _playerHeight = 2;

    private float _curSpeed;
    private float _desiredSpeed;


    private Vector3 _gravityDirection = new(0, -1, 0);
    private float _curGravity;

    private float _transitionTime;
    private float _curTransitionTime;
    private float _speedAccelarator = 10;
    private RaycastHit _slopeHit;
    private float _maxSlopeAngle = 30;
    public Vector3 MoveDirection {  get; private set; }
    public bool DefaultMovementVector { get; set; } = true;
    public Vector3 CustomMoveDirection { get; set; }
    public bool CantBeGrounded { get; set; }
    public Rigidbody Rb { get; private set; }
    public bool Grounded { get; private set; }


    private void Awake()
    {
        ResetGravity();
        Rb = GetComponent<Rigidbody>();
        Rb.drag = _groundDrag;
    }

    private void Update()
    {
        //SpeedChanging();
        GroundedCheck();
        SetGroundDrag();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovingForce();
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

    public void StopMoving()
    {
        _desiredSpeed = 0;
        _curSpeed = 0;
        Rb.velocity = Vector3.zero;
    }
    public void StopVerticalMovement()
    {
        Vector3 velocity = Rb.velocity;
        Rb.velocity = new Vector3(velocity.x,0,velocity.z);
    }
    public void ChangeDirection(Vector3 direction) => MoveDirection = direction;

    public void SetGravity(float gravity) => _curGravity = gravity;

    public void ResetGravity() => _curGravity = _gravity;

    public bool NotMovingUp() => Rb.velocity.y - GetCurrentMovementVector().y < 1;
    public float GetMoveScrMagnitude() => MoveDirection.sqrMagnitude;


    public bool NotMovingVertically()
    {
        float verticalSpeed = Rb.velocity.y - GetCurrentMovementVector().y;
        return verticalSpeed is > -1 and < 1;
    }

    public bool OnSlope(Vector3 upDirection)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(upDirection, _slopeHit.normal);
            return angle < _maxSlopeAngle;
        }

        return false;
    }

    private void MovingForce()
    {
        if (CanMove())
        {
            Rb.AddForce(GetCurrentMovementVector() * _speedAccelarator, ForceMode.Force);
        }
    }

    private bool CanMove()
    {
        return OnSlope(Vector3.up)||!Grounded;
    }

    private Vector3 GetCurrentMovementVector()
    {
        Vector3 direction;
        if (DefaultMovementVector)
        {
            direction = MoveDirection;
        }
        else
        {
            direction = CustomMoveDirection;
        }
        return GetSlopeMoveDirection(direction, _slopeHit.normal) * _curSpeed;
    }


    private void SpeedControl()
    {
        Vector3 velocity = Rb.velocity;
        Vector3 rbXYVelocity = new Vector3(velocity.x, 0f, velocity.z);
        if (rbXYVelocity.magnitude > _curSpeed)
        {
            Vector3 limitedVel = rbXYVelocity.normalized * _curSpeed;
            Rb.velocity = new Vector3(limitedVel.x, Rb.velocity.y, limitedVel.z);
        }
    }

    private void GroundedCheck()
    {
        bool touchGround = Physics.SphereCast(transform.position, 0.5f, Vector3.down, out RaycastHit ray,
            _playerHeight * 0.5f + 0.2f, whatIsGround);
        bool notMovingUp = NotMovingUp();
        Grounded = touchGround && notMovingUp && !CantBeGrounded;
    }

    private void SpeedChanging()
    {
        if (_curTransitionTime >= _transitionTime)
        {
            _curTransitionTime += Time.deltaTime;
            SmoothSpeedChanging();
        }
    }


    private Vector3 GetSlopeMoveDirection(Vector3 moveDirection, Vector3 slopeNormal)
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeNormal).normalized;
    }

    private void GravityForce() => Rb.AddForce(_curGravity * _gravityDirection, ForceMode.Force);

    private void SetGroundDrag() => Rb.drag = Grounded ? _groundDrag : 0;

    private void SmoothSpeedChanging() =>
        _curSpeed = Mathf.Lerp(_curSpeed, _desiredSpeed, _curTransitionTime / _transitionTime);
}