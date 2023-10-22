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
            _stateMaсhine.CheckStatesTransitions();
        }
    }
}