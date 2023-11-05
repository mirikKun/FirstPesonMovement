namespace Movement.StateMachine
{
    public interface IPlayerState : IState
    {
        public bool ConditionToEnter();
        public void Construct(PlayerStateMaсhine stateMachine,  PlayerMover mover,IInput input);
    }
}