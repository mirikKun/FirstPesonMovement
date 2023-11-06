using UnityEngine;

namespace Movement.StateMachine.States
{
    public abstract class PlayerRechargeableStateBase:PlayerStateBase
    {
        [SerializeField] protected float _cooldown=0.2f;
        protected float _stateTime;
        protected bool _readyToState;

        private void Update()
        {
            StateUpdate();
        }
        protected virtual void  StateUpdate()
        {
            JumpCooldown();
        }
        private void JumpCooldown()
        {
            if (!_readyToState)
            {
                _stateTime += Time.deltaTime;
                if (_stateTime > _cooldown)
                {
                    _readyToState = true;
                    _mover.CantBeGrounded = false;

                }
            }
        }

        protected void StartCooldown()
        {
            _stateTime = 0;
            _readyToState = false;
        }

      
    }
}