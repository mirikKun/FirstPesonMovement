namespace Movement.StateMachine.States
{
    public class PlayerIdleState:PlayerStateBase{
        public override bool ConditionToEnter() => 
            _mover.Grounded && _mover.GetMoveScrMagnitude() < 0.1f && _mover.NotMovingVertically()&&_mover.DefaultMovementVector;

        public override void Enter()
        {
            _mover.StopMoving();
            _mover.SetGravity(0);
        }

        public override void Exit()
        {
            _mover.ResetGravity();

        }
    }
}