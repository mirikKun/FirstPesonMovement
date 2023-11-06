using System;
using UnityEngine;

namespace Movement.StateMachine.States
{
    public class WallRunState:PlayerRechargeableStateBase
    {
        [SerializeField] private Transform _orientation;
        [SerializeField] private float _stickToWallForce = 5;
        [SerializeField] private float _wallRunSpeed=7;


        private bool _wallLeft;
        private bool _wallRight;
        private RaycastHit _leftWallHit;
        private RaycastHit _rightWallHit;
        [SerializeField]private float _wallCheckDistance;
        [SerializeField]private LayerMask _whatIsWall;
        private bool _active;


        protected override void StateUpdate()
        {
            base.StateUpdate();
            if (!_active)
                return;
            WallRunning();
        }

        public override void Enter()
        {
            Debug.Log("Enter wall run");

            _mover.StopVerticalMovement();
            _mover.DefaultMovementVector = false;
            _mover.SetGravity(0);
            _mover.ChangeSpeed(_wallRunSpeed,0);
            _active = true;
            _mover.CustomMoveDirection = GetMoveVector().normalized;


        }

        public override void Exit()
        {
            StartCooldown();

            _mover.ResetGravity();
            _active = false;
            
            _mover.DefaultMovementVector = true;
            _mover.CustomMoveDirection = _wallRight ? _rightWallHit.normal : _leftWallHit.normal;
        }



        private void WallRunning()
        {
            CheckForRightWall();
            CheckForLeftWall();
            ExitIfWallEnded();
            Vector3 moveDirection = GetMoveVector();
            _mover.CustomMoveDirection = moveDirection.normalized;
        }

        private void ExitIfWallEnded()
        {
            if (!_wallRight && !_wallLeft)
            {
                Exit();
            }
        }

        private Vector3 GetMoveVector()
        {
            Vector3 wallNormal = _wallRight ? _rightWallHit.normal : _leftWallHit.normal;
            Vector3 wallForward = Vector3.Cross(wallNormal, _orientation.up);
            if (OppositeDirection(wallForward))
                wallForward = -wallForward;

            Vector3 moveDirection = wallForward * _wallRunSpeed + -wallNormal * _stickToWallForce;
            return moveDirection;
        }

        private bool OppositeDirection(Vector3 wallForward)
        {
            Vector3 forward = _orientation.forward;
            return (forward - wallForward).magnitude > (forward - -wallForward).magnitude;
        }

        public override bool ConditionToEnter()
        {
            CheckForRightWall();
            CheckForLeftWall();
            return (_wallRight || _wallLeft) && !_mover.Grounded && _mover.GetMoveScrMagnitude() > 0.1f;

        }

        private void CheckForRightWall()
        {
            _wallRight = Physics.Raycast(transform.position, _orientation.right, out _rightWallHit, _wallCheckDistance, _whatIsWall);
        }
        private void CheckForLeftWall()
        {
            _wallLeft = Physics.Raycast(transform.position, -_orientation.right, out _leftWallHit, _wallCheckDistance, _whatIsWall);
        }
    }
}