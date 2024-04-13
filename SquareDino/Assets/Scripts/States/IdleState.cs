using SquareDino.Player;
using SquareDino.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public class IdleState : IState
    {
        private static readonly int IdleHash = Animator.StringToHash("isIdle");
        public StateMachine.States State => StateMachine.States.Idle;

        private PlayerController _player;
        private Animator _animator;

        public IdleState(PlayerController player, Animator animator)
        {
            _player = player;
            _animator = animator;
        }

        public void Tick()
        {
            if (_player.IsActive)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                _player.IsActive = true;
                _player.IsGameStarted = true;
            }
        }

        public void OnEnter()
        {
            _animator.SetBool(IdleHash, true);
        }

        public void OnExit()
        {
            _animator.SetBool(IdleHash, false);
        }

        public void FixedTick() { }
    }
}
