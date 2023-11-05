using System;
using Movement.StateMachine;
using TMPro;
using UnityEngine;

namespace Movement
{
    public class Player:MonoBehaviour
    {
        [SerializeField] private PlayerMover mover;
        [SerializeField] private PlayerInputPC input;
        [SerializeField] private TextMeshProUGUI tex111;

        private PlayerStateMaсhine _stateMaсhine;


        private void Start()
        {
            IPlayerState[] states = GetComponents<IPlayerState>();

            _stateMaсhine = new PlayerStateMaсhine(states);
            foreach (IPlayerState state in states)
            {
                state.Construct(_stateMaсhine,mover,input);
            }
        }

        private void Update()
        {
            tex111.text = _stateMaсhine.GetStateName();

            _stateMaсhine.CheckStatesTransitions();
        }
    }
}