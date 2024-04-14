using SquareDino.Player;
using SquareDino.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public class FinalReachedState : IState
    {
        private static readonly int IsFinalReached = Animator.StringToHash("isFinalReached");

        public StateMachine.States State => StateMachine.States.FinalReached;

        private PlayerController _player;
        private Animator _animator;

        public FinalReachedState(PlayerController player)
        {
            _player = player;
            _animator = player.Animator;
        }

        public void OnEnter()
        {
            _animator.SetBool(IsFinalReached, true);
            _player.IsActive = false;
            _player.IsGameStarted = false;
            _player.IsGameEnded = true;
            _player.OnGameEnded?.Invoke();
        }

        public void OnExit()
        {
            _animator.SetBool(IsFinalReached, false);
            _player.IsGameEnded = false;
        }

        public void FixedTick() { }
        public void Tick() { }
    }
}
