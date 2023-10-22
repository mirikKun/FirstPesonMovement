using UnityEngine;

namespace Movement.StateMachine.States
{
    public class PlayerWalker : PlayerStateBase
    {
        private const float MinMagnitude = 0.1f;
        [SerializeField] private float _speed;


        public override bool ConditionToEnter()
        {
            return !_input.Sprint && _mover.Grounded && _mover.MoveDirection.sqrMagnitude > MinMagnitude;
        }

        public override void Enter()
        {
            _mover.ChangeSpeed(_speed, 0);
        }

        public override void Exit()
        {
        }
    }
}