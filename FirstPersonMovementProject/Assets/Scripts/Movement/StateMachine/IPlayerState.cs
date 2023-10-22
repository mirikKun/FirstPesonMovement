namespace Movement.StateMachine
{
    public interface IPlayerState : IState
    {
        public bool ConditionToEnter();
        public void Construct(PlayerStateMaсhine stateMaсhine, PlayerMover mover,IInput input);
    }
}