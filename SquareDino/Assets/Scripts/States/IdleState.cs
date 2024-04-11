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
        private Animator _animator;

        public IdleState(Animator animator)
        {
            _animator = animator;
        }

        public void Tick() { }
        public void FixedTick() { }
        public void OnEnter()
        {
            _animator.SetBool(IdleHash, true);
        }
        public void OnExit()
        {
            _animator.SetBool(IdleHash, false);
        }
    }
}
