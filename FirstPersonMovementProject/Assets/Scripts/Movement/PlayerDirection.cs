using UnityEngine;

namespace Movement
{
    public class PlayerDirection : MonoBehaviour
    {

        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private PlayerInputPC _playerInputPC;
        [SerializeField] private Transform _orientation;
        private float _xInput;
        private float _yInput;
    
        private void Update()
        {
            GetMovementInput();
           
            _playerMover.ChangeDirection(MoveDirection);
        }

        private void GetMovementInput()
        {
            _xInput = _playerInputPC.XInput;
            _yInput = _playerInputPC.YInput;
        }

        public Vector3 MoveDirection =>_orientation.forward*_yInput+_orientation.right* _xInput;
    }
}
