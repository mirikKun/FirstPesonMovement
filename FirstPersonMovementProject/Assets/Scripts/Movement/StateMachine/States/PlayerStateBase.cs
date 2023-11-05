using UnityEngine;

namespace Movement.StateMachine.States
{
    public abstract class PlayerStateBase : MonoBehaviour, IPlayerState
    {
        protected IInput _input;
        protected PlayerMover _mover;
        protected PlayerStateMaсhine _stateMachine;

        public abstract bool ConditionToEnter();

        public void Construct(PlayerStateMaсhine stateMachine, PlayerMover mover, IInput input)
        {
            _stateMachine = stateMachine;
            _mover = mover;
            _input = input;
        }

        public abstract void Enter();

        public abstract void Exit();
    }
}