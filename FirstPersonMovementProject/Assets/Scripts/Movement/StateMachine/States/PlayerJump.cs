using System;
using UnityEngine;

namespace Movement.StateMachine.States
{
    public class PlayerJump : PlayerStateBase
    {
        [SerializeField] private float _jumpForce=15;
        [SerializeField] private float _jumpCooldown=0.2f;
        private float _jumpTime;
        private bool _readyToJump;

        private void Update()
        {
            JumpCooldown();
        }

        private void JumpCooldown()
        {
            if (!_readyToJump)
            {
                _jumpTime += Time.deltaTime;
                if (_jumpTime > _jumpCooldown)
                {
                    _readyToJump = true;
                    _mover.CantBeGrounded = false;

                }
            }
        }

        public override bool ConditionToEnter()
        {
            return _mover.NotMovingUp()  && _mover.Grounded && _input.Jump;
        }

        public override void Enter()
        {
            _mover.AddImpuls(new Vector3(0, _jumpForce, 0));
            _mover.CantBeGrounded = true;
            _jumpTime = 0;
            _readyToJump = false;
        }

        public override void Exit()
        {
        }
    }
}