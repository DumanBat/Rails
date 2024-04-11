using SquareDino.Player;
using SquareDino.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public class AttackState : IState
    {
        private static readonly int AttackHash = Animator.StringToHash("isAttacking");
        public StateMachine.States State => StateMachine.States.Attack;
        
        private PlayerController _player;
        private Animator _animator;

        public AttackState(PlayerController player, Animator animator)
        {
            _player = player;
            _animator = animator;
        }

        public void OnEnter()
        {
            _player.IsAttackEnded = false;
            _animator.SetBool(AttackHash, true);
        }

        public void OnExit()
        {
            _animator.SetBool(AttackHash, false);
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _player.IsAttackEnded = true;
            }
        }

        public void FixedTick() { }
    }
}
