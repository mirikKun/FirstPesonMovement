namespace Movement.StateMachine.States
{
    public class PlayerOnAir : PlayerStateBase
    {
        public override bool ConditionToEnter()
        {
            return !_mover.Grounded && _mover.Rb.velocity.y < 0;
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }
    }
}