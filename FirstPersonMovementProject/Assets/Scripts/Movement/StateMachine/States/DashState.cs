using UnityEngine;

namespace Movement.StateMachine.States
{
    public class DashState:PlayerRechargeableStateBase
    {
        [SerializeField] private Transform _orientation;
        [SerializeField] private float _dashForce;
        [SerializeField] private float _duration;
        [SerializeField] private float _afterDashSpeed;

        private float _dashTime;

        private bool _active;

        public override bool ConditionToEnter()
        {
            return _input.Dash ;
        }

        protected override void StateUpdate()
        {
            base.StateUpdate();
            if (!_active)
                return;
            DashDuration();
        }

        private void DashDuration()
        {
            _dashTime -= Time.deltaTime;
            if (_dashTime <= 0)
            {
                Exit();
            }
        }

        public override void Enter()
        {
            StartCooldown();
            _active = true;
            _dashTime = _duration;
            StartDash();

        }

        public override void Exit()
        {
            _active = false;
            _mover.ResetGravity();
            _mover.CantBeGrounded=false;
            _mover.ChangeSpeed(_afterDashSpeed,0);
            _mover.DefaultMovementVector = true;
            Debug.Log("222"+_mover.DefaultMovementVector);

        }

        private Vector3 GetDashDirection()
        {
            if (_mover.MoveDirection.sqrMagnitude > 0.1f)
            {
                return _mover.MoveDirection;
            }
            else
            {
                return new Vector3(_orientation.forward.x, 0, _orientation.forward.z);
            }
        }

        private void StartDash()
        {
            _mover.CantBeGrounded=true;
            _mover.StopVerticalMovement();
            _mover.SetGravity(0);
            _mover.ChangeSpeed(_dashForce,0);
            
            Vector3 direction = GetDashDirection();
            Vector3 forceToApply = direction * _dashForce ;
            _mover.CustomMoveDirection = direction;
            _mover.DefaultMovementVector = false;
            Debug.Log("111"+_mover.DefaultMovementVector);
            _mover.AddImpuls(forceToApply);
        }
    }
}