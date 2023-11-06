using System;
using UnityEngine;

namespace Movement.StateMachine.States
{
    public class PlayerJump : PlayerRechargeableStateBase
    {
        [SerializeField] private float _jumpForce=15;
        public override bool ConditionToEnter()
        {
            return _mover.NotMovingUp()  && _mover.Grounded && _input.Jump;
        }

        public override void Enter()
        {
            StartCooldown();

            _mover.AddImpuls(new Vector3(0, _jumpForce, 0));
            _mover.CantBeGrounded = true;
        }

        public override void Exit()
        {
        }
    }
}