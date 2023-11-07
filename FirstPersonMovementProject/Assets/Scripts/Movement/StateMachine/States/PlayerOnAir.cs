namespace Movement.StateMachine.States
{
    public class PlayerOnAir : PlayerStateBase
    {
        public override bool ConditionToEnter() =>
            !_mover.Grounded && _mover.Rb.velocity.y < 0&&_mover.DefaultMovementVector;

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }
    }
}