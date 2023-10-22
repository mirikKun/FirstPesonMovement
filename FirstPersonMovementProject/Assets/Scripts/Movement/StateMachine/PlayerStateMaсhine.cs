using System;
using System.Collections.Generic;
using System.Linq;

namespace Movement.StateMachine
{
    public class PlayerStateMaсhine 
    {
        IPlayerState[] _states;
        public IPlayerState _activePlayerState;


        public void CheckStatesTransitions()
        {
            foreach (var state in _states)
            {
                if (_activePlayerState != state && state.ConditionToEnter())
                {
                    Enter(state);
                }
            }
        }
        public PlayerStateMaсhine(IPlayerState[] states)
        {
            _states = states;
            _activePlayerState = _states[0];
        }
    
        public void Enter(IPlayerState state) 
        {
            IPlayerState playerState = ChangeState(state);
            playerState.Enter();
        }

        private IPlayerState ChangeState(IPlayerState playerState) 
        {
            _activePlayerState?.Exit();
            _activePlayerState = playerState;
            return playerState;
        }


    }
}
