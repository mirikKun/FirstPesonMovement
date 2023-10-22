using UnityEngine;

namespace Movement.StateMachine.States
{
    public abstract class PlayerStateBase : MonoBehaviour, IPlayerState
    {
        protected IInput _input;
        protected PlayerMover _mover;
        protected PlayerStateMaсhine _stateMasine;

        public abstract bool ConditionToEnter();

        public void Construct(PlayerStateMaсhine stateMaсhine, PlayerMover mover, IInput input)
        {
            _stateMasine = stateMaсhine;
            _mover = mover;
            _input = input;
        }

        public abstract void Enter();

        public abstract void Exit();
    }
}