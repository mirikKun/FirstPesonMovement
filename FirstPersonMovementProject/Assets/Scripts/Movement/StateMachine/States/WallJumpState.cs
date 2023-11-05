using UnityEngine;

namespace Movement.StateMachine.States
{
    public class WallJumpState:PlayerStateBase
    {
        [SerializeField] private float _jumpUpForce=20;
        [SerializeField] private float _jumpFromWallForce=10;

        public override bool ConditionToEnter()
        {
            return _stateMachine.CurStateIs<WallRunState>() && _input.Jump;
        }

        public override void Enter()
        {
            Vector3 jumpDirection = new Vector3(0, _jumpUpForce, 0);
            jumpDirection += _mover.CustomMoveDirection.normalized * _jumpFromWallForce;
            _mover.AddImpuls(jumpDirection);

        }

        public override void Exit()
        {
        }
    }
}