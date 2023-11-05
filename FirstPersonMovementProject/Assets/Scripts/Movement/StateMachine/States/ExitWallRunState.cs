using UnityEngine;

namespace Movement.StateMachine.States
{
    public class ExitWallRunState:PlayerStateBase
    {
        [SerializeField] private float _moveAngleToExitWallRunning=45;
        public override bool ConditionToEnter()
        {
            return _stateMachine.CurStateIs<WallRunState>()&&
                   Vector3.Angle(_mover.CustomMoveDirection,_mover.MoveDirection)>_moveAngleToExitWallRunning;
        }

        public override void Enter()
        {

        }

        public override void Exit()
        {
            
        }
    }
}