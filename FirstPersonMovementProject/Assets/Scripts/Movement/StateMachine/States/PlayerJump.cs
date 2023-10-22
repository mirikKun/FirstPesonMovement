using System;
using UnityEngine;

namespace Movement.StateMachine.States
{
    public class PlayerJump : PlayerStateBase
    {
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpCooldown=0.2f;
        private float jumpTime;
        private bool readyToJump;

        private void Update()
        {
            JumpCooldown();
        }

        private void JumpCooldown()
        {
            if (!readyToJump)
            {
                jumpTime += Time.deltaTime;
                if (jumpTime > jumpCooldown)
                {
                    readyToJump = true;
                }
            }
        }

        public override bool ConditionToEnter()
        {
            return _mover.Rb.velocity.y < 1 && _mover.Grounded && _input.Jump;
        }

        public override void Enter()
        {
            _mover.AddImpuls(new Vector3(0, jumpForce, 0));
            jumpTime = 0;
            readyToJump = false;
        }

        public override void Exit()
        {
        }
    }
}