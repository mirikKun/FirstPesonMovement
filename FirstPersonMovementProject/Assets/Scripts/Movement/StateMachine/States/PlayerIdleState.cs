namespace Movement.StateMachine.States
{
    public class PlayerIdleState:PlayerStateBase{
        public override bool ConditionToEnter()
        {
            return _mover.Grounded && _mover.MoveDirection.sqrMagnitude < 0.1f;
        }

        public override void Enter()
        {
            _mover.ChangeSpeed(0, 0);
        }

        public override void Exit()
        {
            
        }
    }
}